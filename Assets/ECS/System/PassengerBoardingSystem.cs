using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class PassengerBoardingSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<SendRequesGetDataInPassengerBoardingEvent> _sendRequestFilter;

    private List<Passenger> _passengers;

    private List<Vehicle> _allCars;
    private List<Vehicle> _carsInParking;
    private List<Vehicle> _carsToAddParking;

    public PassengerBoardingSystem(List<Vehicle> allCars)
    {
        _allCars = allCars;
        _carsInParking = new List<Vehicle>();
        _carsToAddParking = new List<Vehicle>();
    }

    public void Init()
    {
        for (int i = 0; i < _allCars.Count; i++)
        {
            _allCars[i].CarEnterParking += AddCar;
        }
    }

    public void Destroy()
    {
        for (int i = 0; i < _allCars.Count; i++)
        {
            _allCars[i].CarEnterParking -= AddCar;
        }
    }

    private void AddCar(Vehicle car)
    {
        _carsToAddParking.Add(car);
        TeleportCarToReservedParkingSlot(car);
    }

    private void TeleportCarToReservedParkingSlot(Vehicle car)
    {
        car.Entity.Get<CarParkingEvent>();
    }


    public void Run()
    {
        SendDataToPassengerSortingSystem();
        MovePassengerToCar();
    }

    private void SendDataToPassengerSortingSystem()
    {
        foreach (var sendRequestEntity in _sendRequestFilter)
        {
            var passengerSortingNewEntity = _ecsWorld.NewEntity();
            ref var passengerSortingDataEvent = ref passengerSortingNewEntity.Get<GetUnitsDataEvent>();
            passengerSortingDataEvent.carsOnlyParkingZoneList = _carsInParking;
            passengerSortingDataEvent.allPassengersInLevel = _passengers;

            passengerSortingNewEntity.Get<VerifyCarsToPassengerSortingEvent>();

            _sendRequestFilter.GetEntity(sendRequestEntity).Del<SendRequesGetDataInPassengerBoardingEvent>();
        }
    }

    private void MovePassengerToCar()
    {
        if (_carsToAddParking.Count > 0)
        {
            _carsInParking.AddRange(_carsToAddParking);
            _carsToAddParking.Clear();
        }

        if (_carsInParking.Count == 0 || _passengers == null || _passengers.Count == 0)
            return;

        var carsArray = _carsInParking.ToArray();
        var passengersArray = _passengers.ToArray();

        for (int i = 0; i < carsArray.Length; i++)
        {
            ref var firstPassengerComponent = ref passengersArray[0].Entity.Get<PassengerComponent>();
            ref var carComponent = ref carsArray[i].Entity.Get<CarComponent>();

            if (carComponent.isParked == false)
                continue;

            if (carComponent.isNotEmptySeats == false)
            {
                if (carComponent.renderer.material.color == firstPassengerComponent.renderer.material.color)
                {
                    ref var firstPassengerMovable = ref passengersArray[0].Entity.Get<PassengerMovableComponent>();
                    ref var carMovable = ref carsArray[i].Entity.Get<CarMovableComponent>();

                    if (firstPassengerMovable.isPositionStartQueuePosition)
                    {
                        firstPassengerComponent.carComponent = carComponent;
                        firstPassengerMovable.isMoving = true;
                        firstPassengerMovable.targetCarPosition = carMovable.rigidbody.position;
                        carComponent.reservedSeats.Add(firstPassengerComponent);
                        _passengers.RemoveAt(0);

                        _ecsWorld.NewEntity().Get<ChangePassengersCountToShowerEvent>() = new ChangePassengersCountToShowerEvent
                        {
                            newCurrentCount = _passengers.Count
                        };

                        return;
                    }
                }
            }
            else
            {
                _carsInParking.Remove(carComponent.car);
            }
        }
    }
}
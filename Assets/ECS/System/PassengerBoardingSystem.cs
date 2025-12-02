using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class PassengerBoardingSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<SendRequesPassengersAndCarsDataEvent> _sendRequestFilter;

    private List<Passenger> _passengers;
    private CarToParkingTriggerHandler _carToParkingTriggerHandler;

    private List<Vehicle> _cars;
    private List<Vehicle> _carsToAdd;

    public PassengerBoardingSystem(List<Passenger> passengers, CarToParkingTriggerHandler parkingTriggerHandler)
    {
        _passengers = passengers;
        _carToParkingTriggerHandler = parkingTriggerHandler;

        _cars = new List<Vehicle>();
        _carsToAdd = new List<Vehicle>();
    }

    public void Init()
    {
        _carToParkingTriggerHandler.CarEnterParking += AddCar;
    }

    private void AddCar(Vehicle car)
    {
        _carsToAdd.Add(car);
        TeleportCarToReservedParkingSlot(car);
    }

    private void TeleportCarToReservedParkingSlot(Vehicle car)
    {
        car.Entity.Get<CarParkingEvent>();
    }

    public void Destroy()
    {
        _carToParkingTriggerHandler.CarEnterParking -= AddCar;
    }

    public void Run()
    {
        foreach (var sendRequestEntity  in _sendRequestFilter)
        {
            var requestEntity = _sendRequestFilter.GetEntity(sendRequestEntity);

            _ecsWorld.NewEntity().Get<GetPassengersAndCarsDataEvent>() = new GetPassengersAndCarsDataEvent
            {
                cars = _cars,
                passengers = _passengers,
            };

            requestEntity.Del<SendRequesPassengersAndCarsDataEvent>();
        }

        MovePassengerToCar();
    }

    private void MovePassengerToCar()
    {
        if (_carsToAdd.Count > 0)
        {
            _cars.AddRange(_carsToAdd);
            _carsToAdd.Clear();
        }

        if (_cars.Count == 0 || _passengers == null || _passengers.Count == 0)
            return;

        var carsArray = _cars.ToArray();
        var passengersArray = _passengers.ToArray();

        for (int i = 0; i < carsArray.Length; i++)
        {
            ref var firstPassengerComponent = ref passengersArray[0].Entity.Get<PassengerComponent>();
            ref var carComponent = ref carsArray[i].Entity.Get<CarComponent>();

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
                        firstPassengerMovable.targetCarPosition = carMovable.currentTransform.position;
                        carComponent.reservedSeats.Add(firstPassengerComponent);
                        _passengers.RemoveAt(0);
                        return;
                    }
                }
            }
            else
            {
                _cars.Remove(carComponent.car);
            }
        }
    }
}
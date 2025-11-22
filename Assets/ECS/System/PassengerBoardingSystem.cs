using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class PassengerBoardingSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private Quaternion _rotationCarInParking = Quaternion.Euler(0, -30, 0);

    private List<Passenger> _passengers;
    private List<ParkingSlot> _parkingSlots;
    private CarToParkingTriggerHandler _carToParkingTriggerHandler;

    private List<Vehicle> _cars;
    private List<Vehicle> _carsToAdd;

    public PassengerBoardingSystem(List<Passenger> passengers, CarToParkingTriggerHandler parkingTriggerHandler, List<ParkingSlot> parkingSlots)
    {
        _passengers = passengers;
        _carToParkingTriggerHandler = parkingTriggerHandler;
        _parkingSlots = parkingSlots;

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
        ref var carComponent = ref car.Entity.Get<CarComponent>();
        ref var carMovable = ref car.Entity.Get<CarMovableComponent>();

        carMovable.isMoving = false;
        carMovable.targetPoint = Vector3.zero;
        carMovable.currentTransform.position = carComponent.parkingReservedSlot.transform.position;
        carMovable.currentTransform.rotation = _rotationCarInParking;

        Debug.Log($"PassengerBoardingSystem - TeleportCarToReservedParkingSlot(Vehicle car): Телепортируем машину на ее зарезервированное место: {car.name}");
    }

    public void Destroy()
    {
        _carToParkingTriggerHandler.CarEnterParking -= AddCar;
    }

    public void Run()
    {
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

                        Debug.Log($"PassengerBoardingSystem - MovePassengerToCar(): садим {firstPassengerComponent.passenger.name} в машину");
                        return;
                    }
                }
            }
            else
            {
                //if (_parkingSlots.Contains(carComponent.parkingReservedSlot))
                //{
                //    ref var parkingComponent = ref carComponent.parkingReservedSlot.Entity.Get<ParkingComponent>();
                //    parkingComponent.isReserved = false;

                //    _cars.Remove(carComponent.car);
                //}

                StartCancelParkingReserverEvent(carComponent.parkingReservedSlot);

                _cars.Remove(carComponent.car);
            }
        }
    }

    private void StartCancelParkingReserverEvent(ParkingSlot slot)
    {
        _ecsWorld.NewEntity().Get<ParkingCancelReservationEvent>() = new ParkingCancelReservationEvent
        {
            parkingSlot = slot
        };
    }
}
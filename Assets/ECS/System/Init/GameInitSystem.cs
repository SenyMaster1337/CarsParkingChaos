using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class GameInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private StaticData _staticData;

    private List<Vehicle> _cars;
    private List<Passenger> _passengers;
    private List<ParkingSlot> _parkingSlots;
    private StartQueuePoint _startQueuePoint;

    public GameInitSystem(StaticData staticData, List<Vehicle> defaultCars, List<Passenger> passengers, List<ParkingSlot> parkingSlots, StartQueuePoint startQueuePoint)
    {
        _staticData = staticData;
        _cars = defaultCars;
        _parkingSlots = parkingSlots;
        _passengers = passengers;
        _startQueuePoint = startQueuePoint; 
    }

    public void Init()
    {
        InitCars();
        InitPassengers();
        InitParkingSlots();
        InitParkingReservationComponent();
    }

    private void InitCars()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            var carNewEntity = _ecsWorld.NewEntity();

            ref var carComponent = ref carNewEntity.Get<CarComponent>();
            carComponent.car = _cars[i];
            carComponent.renderer = _cars[i].gameObject.GetComponentInChildren<Renderer>();
            carComponent.crashHandler = _cars[i].GetComponentInChildren<CrashHandler>();

            carComponent.passengers = new List<PassengerComponent>();
            carComponent.reservedSeats = new List<PassengerComponent>();

            carComponent.isNotEmptySeats = false;
            carComponent.isAllPassengersBoarded = false;
            carComponent.isCrashHandlerEnabled = true;
            carComponent.canClickable = true; 
            carComponent.canCrashed = true;

            carComponent.rorationCarInParking = _staticData.RotationCarInParking;
            carComponent.distanceToDisableCrashHandler = _staticData.DistanceToDisableCrashHandler;

            if (_cars[i].TryGetComponent(out Minivan minivan))
                carComponent.maxPassengersSlots = _staticData.MinivanCarSlots;
            else
                carComponent.maxPassengersSlots = _staticData.DefaultCarSlots;

            ref var carMovable = ref carNewEntity.Get<CarMovableComponent>();
            carMovable.currentTransform = _cars[i].gameObject.transform;
            carMovable.spawnPosition = _cars[i].gameObject.transform.position;
            carMovable.targetPoint = Vector3.zero;

            carMovable.moveSpeed = _staticData.CarSpeed;

            carMovable.isMoving = false;
            carMovable.isReverseEnable = false;

            ref var carAnimationComponent = ref carNewEntity.Get<CarAnimationComponent>();
            carAnimationComponent.animator = _cars[i].GetComponentInChildren<Animator>();

            _cars[i].Entity = carNewEntity;
        }
    }

    private void InitPassengers()
    {
        for (int i = 0; i < _passengers.Count; i++)
        {
            var passengerNewEntity = _ecsWorld.NewEntity();

            ref var passengerComponent = ref passengerNewEntity.Get<PassengerComponent>();
            passengerComponent.passenger = _passengers[i];
            passengerComponent.renderer = _passengers[i].gameObject.GetComponentInChildren<Renderer>();
            passengerComponent.startQueuePosition = _startQueuePoint.transform.position;

            ref var passengerMovable = ref passengerNewEntity.Get<PassengerMovableComponent>();
            passengerMovable.currentTransform = _passengers[i].gameObject.transform;
            passengerMovable.moveSpeed = _staticData.PassengerSpeed;
            passengerMovable.currentQueuePointPosition = _passengers[i].gameObject.transform.position;
            passengerMovable.targetCarPosition = Vector3.zero;

            passengerMovable.isMoving = false;
            passengerMovable.isNeedShiftQueue = false;

            if (i == 0)
                passengerMovable.isPositionStartQueuePosition = true;
            else
                passengerMovable.isPositionStartQueuePosition = false;

            ref var passengerAnimationComponent = ref passengerNewEntity.Get<PassengerAnimationComponent>();
            passengerAnimationComponent.animator = _passengers[i].GetComponentInChildren<Animator>();

            _passengers[i].Entity = passengerNewEntity;
        }
    }

    private void InitParkingSlots()
    {
        for (int i = 0; i < _parkingSlots.Count; i++)
        {
            var parkingSlotNewEntity = _ecsWorld.NewEntity();

            ref var parkingComponent = ref parkingSlotNewEntity.Get<ParkingComponent>();
            parkingComponent.car = null;
            parkingComponent.isReserved = false;

            _parkingSlots[i].Entity = parkingSlotNewEntity;
        }
    }

    private void InitParkingReservationComponent()
    {
        var parkingReservationEntity = _ecsWorld.NewEntity();

        ref var parkingReservationComponent = ref parkingReservationEntity.Get<ParkingReservationComponent>();
        parkingReservationComponent.parkingSlots = _parkingSlots;
    }
}

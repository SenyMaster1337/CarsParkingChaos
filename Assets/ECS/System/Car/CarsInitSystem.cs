using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private StaticData _staticData;
    private List<Vehicle> _cars;

    public CarsInitSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        InitCars();
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
            carComponent.isCrashed = false;
            carComponent.isParked = true;

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
            carMovable.isReverseDirectionEnable = false;

            ref var carAnimationComponent = ref carNewEntity.Get<CarAnimationComponent>();
            carAnimationComponent.animator = _cars[i].GetComponentInChildren<Animator>();

            ref var carAudioComponent = ref carNewEntity.Get<CarAudioComponent>();
            carAudioComponent.driveSound = _cars[i].GetComponentInChildren<CarDriveSound>();
            carAudioComponent.crashSound = _cars[i].GetComponentInChildren<CarCrashSound>();
            carAudioComponent.leavingSound = _cars[i].GetComponentInChildren<CarLeavingSound>();

            carAudioComponent.isDriveSoundEnable = false;
            carAudioComponent.isCrashSoundEnable = false;
            carAudioComponent.isLeavingCarSoundEnable = false;

            _cars[i].Entity = carNewEntity;
        }
    }
}

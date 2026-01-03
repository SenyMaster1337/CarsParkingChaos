using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class CarsInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private StaticData _staticData;
    private SceneData _sceneData;
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
            carComponent.renderer = _cars[i].GetComponentInChildren<CarRenderer>().Renderer;

            carComponent.passengers = new List<PassengerComponent>();
            carComponent.reservedSeats = new List<PassengerComponent>();

            carComponent.crashHandler = _cars[i].GetComponentInChildren<CrashHandler>();
            carComponent.isCrashHandlerEnabled = true;
            carComponent.canClickable = true;
            carComponent.canCrashed = true;
            carComponent.isCrashed = false;
            carComponent.isParked = true;

            carComponent.isNotEmptySeats = false;
            carComponent.isAllPassengersBoarded = false;

            carComponent.rorationCarInParking = _staticData.RotationCarInParking;
            carComponent.distanceToDisableCrashHandler = _staticData.DistanceToDisableCrashHandler;

            if (_cars[i].TryGetComponent(out CarMinivan minivan))
                carComponent.maxPassengersSlots = _staticData.MinivanCarSlots;
            else if (_cars[i].TryGetComponent(out CarCoope coope))
                carComponent.maxPassengersSlots = _staticData.CoopeCarSlots;
            else
                carComponent.maxPassengersSlots = _staticData.DefaultCarSlots;

            ref var carAnimationComponent = ref carNewEntity.Get<CarAnimationComponent>();
            carAnimationComponent.animator = _cars[i].GetComponentInChildren<Animator>();

            ref var carAudioComponent = ref carNewEntity.Get<CarAudioComponent>();
            carAudioComponent.driveSound = _cars[i].GetComponentInChildren<CarDriveSound>();
            carAudioComponent.crashSound = _cars[i].GetComponentInChildren<CarCrashSound>();
            carAudioComponent.leavingSound = _cars[i].GetComponentInChildren<CarLeavingSound>();

            carAudioComponent.isDriveSoundEnable = false;
            carAudioComponent.isCrashSoundEnable = false;
            carAudioComponent.isLeavingCarSoundEnable = false;

            ref var carEffectComponent = ref carNewEntity.Get<CarEffectsComponent>();
            carEffectComponent.carEffectFilledPassengers = _cars[i].GetComponentInChildren<CarFilledPassengersEffect>();
            carEffectComponent.carCrashEffect = _cars[i].GetComponentInChildren<CarCrashEffect>();
            carEffectComponent.carDriveEffect = _cars[i].GetComponentInChildren<CarDriveEffect>();

            carEffectComponent.isFilledPassengersEffectActive = false;
            carEffectComponent.isCrashEffectActive = false;
            carEffectComponent.isDriveEffectActive = false;

            _cars[i].Entity = carNewEntity;
        }

        if (_sceneData.LevelCarsMaterial != null && _sceneData.LevelCarsMaterial.CarsMaterial != null && _sceneData.LevelCarsMaterial.CarsMaterial.Count > 0)
        {
            for (int i = 0; i < _cars.Count; i++)
            {
                ref var carComponent = ref _cars[i].Entity.Get<CarComponent>();
                carComponent.renderer.material = _sceneData.LevelCarsMaterial.CarsMaterial[i];
            }
        }
    }
}

using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.Handler;
using CarParkingChaos.Effects;
using CarParkingChaos.Markers;
using CarParkingChaos.Sounds;

namespace CarParkingChaos.ECS.Systems
{
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
                carComponent.Car = _cars[i];
                carComponent.Renderer = _cars[i].GetComponentInChildren<CarRenderer>().Renderer;

                carComponent.Passengers = new List<PassengerComponent>();
                carComponent.ReservedSeats = new List<PassengerComponent>();

                carComponent.CrashHandler = _cars[i].GetComponentInChildren<CrashHandler>();
                carComponent.IsCrashHandlerEnabled = true;
                carComponent.CanClickable = true;
                carComponent.CanCrashed = true;
                carComponent.IsCrashed = false;
                carComponent.IsParked = true;

                carComponent.IsNotEmptySeats = false;
                carComponent.IsAllPassengersBoarded = false;

                carComponent.RorationCarInParking = _staticData.RotationCarInParking;
                carComponent.DistanceToDisableCrashHandler = _staticData.DistanceToDisableCrashHandler;

                if (_cars[i].TryGetComponent(out CarMinivan minivan))
                    carComponent.MaxPassengersSlots = _staticData.MinivanCarSlots;
                else if (_cars[i].TryGetComponent(out CarCoope coope))
                    carComponent.MaxPassengersSlots = _staticData.CoopeCarSlots;
                else
                    carComponent.MaxPassengersSlots = _staticData.DefaultCarSlots;

                ref var carAnimationComponent = ref carNewEntity.Get<CarAnimationComponent>();
                carAnimationComponent.Animator = _cars[i].GetComponentInChildren<Animator>();

                ref var carAudioComponent = ref carNewEntity.Get<CarAudioComponent>();
                carAudioComponent.DriveSound = _cars[i].GetComponentInChildren<CarDriveSound>();
                carAudioComponent.CrashSound = _cars[i].GetComponentInChildren<CarCrashSound>();
                carAudioComponent.LeavingSound = _cars[i].GetComponentInChildren<CarLeavingSound>();

                carAudioComponent.IsDriveSoundEnable = false;
                carAudioComponent.IsCrashSoundEnable = false;
                carAudioComponent.IsLeavingCarSoundEnable = false;

                ref var carEffectComponent = ref carNewEntity.Get<CarEffectsComponent>();
                carEffectComponent.CarEffectFilledPassengers = _cars[i].GetComponentInChildren<CarFilledPassengersEffect>();
                carEffectComponent.CarCrashEffect = _cars[i].GetComponentInChildren<CarCrashEffect>();
                carEffectComponent.CarDriveEffect = _cars[i].GetComponentInChildren<CarDriveEffect>();

                carEffectComponent.IsFilledPassengersEffectActive = false;
                carEffectComponent.IsCrashEffectActive = false;
                carEffectComponent.IsDriveEffectActive = false;

                _cars[i].Entity = carNewEntity;
            }

            if (_sceneData.LevelCarsMaterial != null && _sceneData.LevelCarsMaterial.CarsMaterial != null && _sceneData.LevelCarsMaterial.CarsMaterial.Count > 0)
            {
                for (int i = 0; i < _cars.Count; i++)
                {
                    ref var carComponent = ref _cars[i].Entity.Get<CarComponent>();
                    carComponent.Renderer.material = _sceneData.LevelCarsMaterial.CarsMaterial[i];
                }
            }
        }
    }
}

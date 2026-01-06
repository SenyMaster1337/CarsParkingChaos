using Leopotam.Ecs;
using UnityEngine;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class RaycastReaderSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<InputEvent> _input;
        private EcsFilter<EnableRaycastReaderEvent> _raycastEnable;
        private EcsFilter<RaycastReaderDisableEvent> _raycastDisable;
        private EcsFilter<CooldownEvent> _cooldown;

        private StaticData _staticData;
        private bool _isRaycastSystemActive;

        public RaycastReaderSystem()
        {
            _isRaycastSystemActive = true;
        }

        public void Run()
        {
            foreach (var entityInput in _input)
            {
                var entityInputEvent = _input.GetEntity(entityInput);
                ReadRaycast(entityInputEvent);
                entityInputEvent.Del<InputEvent>();
            }

            foreach (var entityDisable in _raycastEnable)
            {
                _isRaycastSystemActive = true;
                _raycastEnable.GetEntity(entityDisable).Del<EnableRaycastReaderEvent>();
            }

            foreach (var entityDisable in _raycastDisable)
            {
                _isRaycastSystemActive = false;
                _raycastDisable.GetEntity(entityDisable).Del<RaycastReaderDisableEvent>();
            }
        }

        public void ReadRaycast(EcsEntity ecsEntity)
        {
            if (_isRaycastSystemActive == false)
                return;

            if (_cooldown.GetEntitiesCount() > 0)
                return;

            ref var inputEvent = ref ecsEntity.Get<InputEvent>();

            if (Physics.Raycast(inputEvent.Ray, out RaycastHit hit))
            {
                var carHit = hit.collider.GetComponent<Vehicle>();
                var advParkingSlotHit = hit.collider.GetComponent<OpenADVParkingSlotUnlock>();

                if (carHit != null)
                {
                    ref var carComponent = ref carHit.Entity.Get<CarComponent>();

                    if (carComponent.CanClickable == false)
                        return;

                    StartParkingReservedEvent(carComponent);
                    StartCooldownEvent();
                    TryStartHandTutorialHideEvent(carComponent);
                }

                if (advParkingSlotHit != null)
                {
                    _ecsWorld.NewEntity().Get<SaveParkingSlotEvent>() = new SaveParkingSlotEvent
                    {
                        ParkingSlot = advParkingSlotHit.ParkingSlot,
                        OpenADVParkingSlotUnlock = advParkingSlotHit
                    };

                    _ecsWorld.NewEntity().Get<OpenADVUnlockParkingSlotEvent>();
                    _ecsWorld.NewEntity().Get<DisableButtonsEvent>();
                    _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
                }
            }
        }


        private void StartParkingReservedEvent(CarComponent component)
        {
            _ecsWorld.NewEntity().Get<ReservedParkingSlotEvent>() = new ReservedParkingSlotEvent { CarEntity = component.Car.Entity };
        }

        private void TryStartHandTutorialHideEvent(CarComponent component)
        {
            _ecsWorld.NewEntity().Get<TutorialHideHandEvent>() = new TutorialHideHandEvent { EcsEntity = component.Car.Entity };
        }

        private void StartCooldownEvent()
        {
            _ecsWorld.NewEntity().Get<CooldownEvent>() = new CooldownEvent { RemainingTime = _staticData.CooldownInputReaderToCar };
        }
    }
}

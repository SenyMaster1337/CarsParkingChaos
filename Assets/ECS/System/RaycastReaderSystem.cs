using Leopotam.Ecs;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RaycastReaderSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<InputEvent> _input;
    private EcsFilter<RaycastReaderEnableEvent> _raycastEnable;
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
            var entityDisableEvent = _raycastEnable.GetEntity(entityDisable);
            _isRaycastSystemActive = true;
            entityDisableEvent.Del<RaycastReaderEnableEvent>();
        }

        foreach (var entityDisable in _raycastDisable)
        {
            var entityDisableEvent = _raycastDisable.GetEntity(entityDisable);
            _isRaycastSystemActive = false;
            entityDisableEvent.Del<RaycastReaderDisableEvent>();
        }
    }

    public void ReadRaycast(EcsEntity ecsEntity)
    {
        if (_isRaycastSystemActive == false)
            return;

        if (_cooldown.GetEntitiesCount() > 0)
            return;

        ref var inputEvent = ref ecsEntity.Get<InputEvent>();

        if (Physics.Raycast(inputEvent.ray, out RaycastHit hit))
        {
            var hitEntity = hit.collider.GetComponent<Vehicle>();

            if (hitEntity == null)
                return;

            ref var carComponent = ref hitEntity.Entity.Get<CarComponent>();

            if (carComponent.canClickable == false)
                return;

            StartParkingReservedEvent(carComponent);

            StartCooldownEvent();

            TryStartHandTutorialHideEvent(carComponent);
        }
    }


    private void StartParkingReservedEvent(CarComponent component)
    {
        _ecsWorld.NewEntity().Get<ReservedParkingSlotEvent>() = new ReservedParkingSlotEvent
        {
            carEntity = component.car.Entity
        };
    }

    private void TryStartHandTutorialHideEvent(CarComponent component)
    {
        _ecsWorld.NewEntity().Get<HandTutorialHideEvent>() = new HandTutorialHideEvent
        {
            ecsEntity = component.car.Entity
        };
    }

    private void StartCooldownEvent()
    {
        _ecsWorld.NewEntity().Get<CooldownEvent>() = new CooldownEvent { remainingTime = _staticData.CooldownInputReaderToCar };
    }
}

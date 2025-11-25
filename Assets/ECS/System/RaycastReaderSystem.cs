using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RaycastReaderSystem : IEcsRunSystem
{
    private EcsFilter<InputEvent> _input;
    private EcsFilter<RaycastReaderEnableEvent> _raycastEnable;
    private EcsFilter<RaycastReaderDisableEvent> _raycastDisable;

    private bool _isActiveSystem;

    public RaycastReaderSystem()
    {
        _isActiveSystem = true;
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
            _isActiveSystem = true;
            entityDisableEvent.Del<RaycastReaderEnableEvent>();
        }

        foreach (var entityDisable in _raycastDisable)
        {
            var entityDisableEvent = _raycastDisable.GetEntity(entityDisable);
            _isActiveSystem = false;
            entityDisableEvent.Del<RaycastReaderDisableEvent>();
        }
    }

    public void ReadRaycast(EcsEntity ecsEntity)
    {
        ref var inputEvent = ref ecsEntity.Get<InputEvent>();

        if (_isActiveSystem == false)
            return;

        if (Physics.Raycast(inputEvent.ray, out RaycastHit hit))
        {
            var hitEntity = hit.collider.GetComponent<Vehicle>();

            if (hitEntity == null)
                return;

            hitEntity.Entity.Get<CarActivatedMovableEvent>();
        }
    }
}

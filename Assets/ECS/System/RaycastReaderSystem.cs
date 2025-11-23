using Leopotam.Ecs;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RaycastReaderSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsFilter<RaycastReaderEnableEvent> _raycastEnable;
    private EcsFilter<RaycastReaderDisableEvent> _raycastDisable;

    private bool _isActiveSystem;
    private InputSystem _inputSystem;

    public RaycastReaderSystem(InputSystem inputSystem)
    {
        _isActiveSystem = true;
        _inputSystem = inputSystem;
    }

    public void Init()
    {
        _inputSystem.RayShooting += ReadRaycast;
    }

    public void Destroy()
    {
        _inputSystem.RayShooting -= ReadRaycast;
    }

    public void Run()
    {
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

    public void ReadRaycast(Ray screenPoint)
    {
        if (_isActiveSystem == false)
            return;

        if (Physics.Raycast(screenPoint, out RaycastHit hit))
        {
            var hitEntity = hit.collider.GetComponent<Vehicle>();

            if (hitEntity == null)
                return;

            hitEntity.Entity.Get<CarActivatedMovableEvent>();
        }
    }
}

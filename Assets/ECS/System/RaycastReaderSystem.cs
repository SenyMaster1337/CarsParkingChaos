using Leopotam.Ecs;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RaycastReaderSystem : IEcsInitSystem, IEcsDestroySystem
{
    private InputSystem _inputSystem;

    public event Action<EcsEntity> EntityDetected;

    public RaycastReaderSystem(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
    }

    public void Init()
    {
        _inputSystem.RayShooting += ReadRaycast;
    }

    public void ReadRaycast(Ray screenPoint)
    {
        if (Physics.Raycast(screenPoint, out RaycastHit hit))
        {
            var hitEntity = hit.collider.GetComponent<Vehicle>();

            if (hitEntity == null)
                return;

            EntityDetected?.Invoke(hitEntity.Entity);
        }
    }

    public void Destroy()
    {
        _inputSystem.RayShooting -= ReadRaycast;
    }
}

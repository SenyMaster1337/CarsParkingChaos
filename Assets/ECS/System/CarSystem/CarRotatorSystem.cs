using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class CarRotatorSystem : IEcsInitSystem, IEcsDestroySystem
{
    private List<RotateTriggerHandler> _rotateTriggerHandler;

    public CarRotatorSystem(List<RotateTriggerHandler> triggerHandler)
    {
        _rotateTriggerHandler = triggerHandler;
    }

    public void Init()
    {
        for (int i = 0; i < _rotateTriggerHandler.Count; i++)
        {
            _rotateTriggerHandler[i].OnTriggerCar += RotateCar;
        }
    }

    private void RotateCar(Vehicle car, Quaternion quaternion)
    {
        ref var movable = ref car.Entity.Get<CarMovableComponent>();
        movable.currentTransform.rotation = quaternion;
    }

    public void Destroy()
    {
        for (int i = 0; i < _rotateTriggerHandler.Count; i++)
        {
            _rotateTriggerHandler[i].OnTriggerCar -= RotateCar;
        }
    }
}

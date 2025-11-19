using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class CarCrashHandlerSystem : IEcsInitSystem, IEcsDestroySystem
{
    private List<Vehicle> _crashHandler;

    public CarCrashHandlerSystem(List<Vehicle> collisionHandler)
    {
        _crashHandler = collisionHandler;
    }

    public void Init()
    {
        for (int i = 0; i < _crashHandler.Count; i++)
        {
            _crashHandler[i].GetComponentInChildren<CrashHandler>().OnCollisionCar += ComeBack;
        }
    }

    private void ComeBack(Vehicle crashHandlerCar, Vehicle carCrashed)
    {
        ref var componentCarCrashed = ref carCrashed.Entity.Get<CarComponent>();

        if (componentCarCrashed.canCrashed == false)
        {
            Debug.Log("Обработка столкновений не обратаывает эту машину, тк она уже едет");
            ref var movableCrashHandlerCar = ref crashHandlerCar.Entity.Get<CarMovableComponent>();
            movableCrashHandlerCar.isReverseEnable = true;
        }
    }

    public void Destroy()
    {
        for (int i = 0; i < _crashHandler.Count; i++)
        {
            if (_crashHandler[i] != null)
                _crashHandler[i].GetComponentInChildren<CrashHandler>().OnCollisionCar -= ComeBack;
        }
    }
}

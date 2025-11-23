using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CarParkingSystem : IEcsInitSystem, IEcsDestroySystem
{
    private CarEnterHandler _carHandler;
    private CarToParkingTriggerHandler _parkingTriggerHandler;

    public CarParkingSystem(CarEnterHandler carHandler, CarToParkingTriggerHandler parkingTriggerHandler)
    {
        _carHandler = carHandler;
        _parkingTriggerHandler = parkingTriggerHandler;
    }

    public void Init()
    {
        _carHandler.OnCollisionCar += ParkCar;
    }

    private void ParkCar(Vehicle car)
    {
        ref var movable = ref car.Entity.Get<CarMovableComponent>();
        movable.targetPoint = _parkingTriggerHandler.transform.position;
    }

    public void Destroy()
    {
        _carHandler.OnCollisionCar -= ParkCar;
    }
}

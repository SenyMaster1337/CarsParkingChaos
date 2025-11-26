using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CarParkingSystem : IEcsInitSystem, IEcsDestroySystem
{
    private CarEnterHandler _carHandler;

    public CarParkingSystem(CarEnterHandler carHandler)
    {
        _carHandler = carHandler;
    }

    public void Init()
    {
        _carHandler.OnCollisionCar += ParkCar;
    }

    private void ParkCar(Vehicle car)
    {
        ref var movable = ref car.Entity.Get<CarMovableComponent>();
        ref var component = ref car.Entity.Get<CarComponent>();
        movable.targetPoint = component.parkingReservedSlot.transform.position;
    }

    public void Destroy()
    {
        _carHandler.OnCollisionCar -= ParkCar;
    }
}

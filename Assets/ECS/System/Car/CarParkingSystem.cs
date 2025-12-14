using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CarParkingSystem : IEcsInitSystem, IEcsDestroySystem
{
    private List<Vehicle> _cars;

    public CarParkingSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            _cars[i].OnCollisionCar += ParkCar;
        }
    }

    public void Destroy()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            _cars[i].OnCollisionCar -= ParkCar;
        }
    }

    private void ParkCar(CarParkingDirection caeEnter, Vehicle car)
    {
        ref var movable = ref car.Entity.Get<CarMovableComponent>();
        ref var component = ref car.Entity.Get<CarComponent>();
        movable.targetPoint = component.parkingReservedSlot.transform.position;
    }
}
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class CarRotatorSystem : IEcsInitSystem, IEcsDestroySystem
{
    private List<Vehicle> _cars;

    public CarRotatorSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            _cars[i].OnTriggerCar += RotateCar;
        }
    }

    public void Destroy()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            _cars[i].OnTriggerCar -= RotateCar;
        }
    }

    private void RotateCar(Quaternion quaternion, Vehicle car)
    {
        ref var movable = ref car.Entity.Get<CarMovableComponent>();
        movable.currentTransform.rotation = quaternion;
        movable.isSpeedUpEnable = true;
    }
}
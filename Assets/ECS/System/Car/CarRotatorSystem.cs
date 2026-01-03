using Leopotam.Ecs;
using System.Collections.Generic;

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

    private void RotateCar(CarRotate carRotate, Vehicle car)
    {
        ref var movable = ref car.Entity.Get<CarMovableComponent>();

        if (movable.carRotates.Contains(carRotate) == false)
        {
            movable.carRotates.Add(carRotate);
            movable.rigidbody.MoveRotation(carRotate.transform.rotation);
        }
    }
}
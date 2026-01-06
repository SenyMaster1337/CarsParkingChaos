using System.Collections.Generic;
using Leopotam.Ecs;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Systems
{
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

            if (movable.CarRotates.Contains(carRotate) == false)
            {
                movable.CarRotates.Add(carRotate);
                movable.Rigidbody.MoveRotation(carRotate.transform.rotation);
            }
        }
    }
}

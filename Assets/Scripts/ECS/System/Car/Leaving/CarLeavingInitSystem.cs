using System.Collections.Generic;
using Leopotam.Ecs;
using CarParkingChaos.Markers;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class CarLeavingInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private List<Vehicle> _cars;

        public CarLeavingInitSystem(List<Vehicle> cars)
        {
            _cars = cars;
        }

        public void Init()
        {
            var carLeavingNewEntity = _ecsWorld.NewEntity();

            ref var carLeavingComponent = ref carLeavingNewEntity.Get<CarLeavingComponent>();
            carLeavingComponent.Cars = _cars;
        }
    }
}

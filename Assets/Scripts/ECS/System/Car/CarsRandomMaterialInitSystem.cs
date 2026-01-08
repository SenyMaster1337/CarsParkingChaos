using System.Collections.Generic;
using Leopotam.Ecs;
using CarParkingChaos.Markers;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class CarsRandomMaterialInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private List<Vehicle> _cars;

        public CarsRandomMaterialInitSystem(List<Vehicle> cars)
        {
            _cars = cars;
        }

        public void Init()
        {
            var carsRandomColorNewEntity = _ecsWorld.NewEntity();

            ref var carsRandomColorComponent = ref carsRandomColorNewEntity.Get<CarsRandomColorComponent>();
            carsRandomColorComponent.Cars = _cars;
        }
    }
}

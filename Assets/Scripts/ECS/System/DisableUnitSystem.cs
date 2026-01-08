using Leopotam.Ecs;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class DisableUnitSystem : IEcsRunSystem
    {
        private EcsFilter<DisableUnitsEvent> _filter;

        public void Run()
        {
            foreach (var entity in _filter)
            {
                var entityDisableComponent = _filter.GetEntity(entity);

                if (entityDisableComponent.IsAlive() && entityDisableComponent.Has<CarComponent>())
                {
                    ref var carComponent = ref entityDisableComponent.Get<CarComponent>();

                    entityDisableComponent.Del<DisableUnitsEvent>();
                    carComponent.Car.gameObject.SetActive(false);
                    carComponent.Car.Entity.Destroy();
                }

                if (entityDisableComponent.IsAlive() && entityDisableComponent.Has<PassengerComponent>())
                {
                    ref var passengerComponent = ref entityDisableComponent.Get<PassengerComponent>();

                    entityDisableComponent.Del<DisableUnitsEvent>();
                    passengerComponent.Passenger.gameObject.SetActive(false);
                    passengerComponent.Passenger.Entity.Destroy();
                }
            }
        }
    }
}

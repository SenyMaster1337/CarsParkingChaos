using Leopotam.Ecs;

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
                carComponent.car.gameObject.SetActive(false);
                carComponent.car.Entity.Destroy();
            }

            if (entityDisableComponent.IsAlive() && entityDisableComponent.Has<PassengerComponent>())
            {
                ref var passengerComponent = ref entityDisableComponent.Get<PassengerComponent>();

                entityDisableComponent.Del<DisableUnitsEvent>();
                passengerComponent.passenger.gameObject.SetActive(false);
                passengerComponent.passenger.Entity.Destroy();
            }
        }
    }
}

using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUnitSystem : IEcsRunSystem
{
    private EcsFilter<DisableComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            var entityDisableComponent = _filter.GetEntity(entity);

            Vehicle car = null;
            Passenger passenger = null;

            if (entityDisableComponent.Has<DisableComponent>())
            {
                if (entityDisableComponent.Has<CarComponent>())
                {
                    ref var carComponent = ref entityDisableComponent.Get<CarComponent>();
                    car = carComponent.car;

                    entityDisableComponent.Del<CarComponent>();
                    entityDisableComponent.Del<CarMovableComponent>();
                    entityDisableComponent.Del<CarAnimationComponent>();
                }

                if (entityDisableComponent.Has<PassengerComponent>())
                {
                    ref var passengerComponent = ref entityDisableComponent.Get<PassengerComponent>();
                    passenger = passengerComponent.passenger;

                    entityDisableComponent.Del<PassengerMovableComponent>();
                    entityDisableComponent.Del<PassengerComponent>();
                    entityDisableComponent.Del<PassengerAnimationComponent>();
                }

                entityDisableComponent.Del<DisableComponent>();

                if (car != null)
                {
                    Debug.Log($"DisableUnitSystem: Удаляем {car.name}");
                    car.gameObject.SetActive(false);
                    car = null;
                }

                if (passenger != null)
                {
                    Debug.Log($"DisableUnitSystem: {passenger.name}");
                    passenger.gameObject.SetActive(false);
                    passenger = null;
                }
            }
        }
    }
}

using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<ShuffleComponent> _shuffleComponentFilter;
    private EcsFilter<ShuffleEvent> _shuffleEventfilter;

    public void Run()
    {
        foreach (var shuffleComponentEntity in _shuffleComponentFilter)
        {
            ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

            foreach (var shuffleEventEntity in _shuffleEventfilter)
            {
                if (shuffleComponent.cars.Count <= 1)
                    return;

                StartConfirmBuyingEvent();

                System.Random random = new();

                for (int i = 0; i < shuffleComponent.cars.Count; i++)
                {
                    int randomIndex = random.Next(i + 1);

                    ref var firstCarComponent = ref shuffleComponent.cars[i].Entity.Get<CarComponent>();
                    ref var secondCarComponent = ref shuffleComponent.cars[randomIndex].Entity.Get<CarComponent>();

                    Color tempFirstPassengerColor = firstCarComponent.renderer.material.color;
                    Color templastPassengerColor = secondCarComponent.renderer.material.color;

                    firstCarComponent.renderer.material.color = templastPassengerColor;
                    secondCarComponent.renderer.material.color = tempFirstPassengerColor;
                }

                int passengerIndex = 0;

                for (int i = 0; i < shuffleComponent.cars.Count && passengerIndex < shuffleComponent.passengers.Count; i++)
                {
                    ref var carComponent = ref shuffleComponent.cars[i].Entity.Get<CarComponent>();

                    for (int j = 0; j < carComponent.maxPassengersSlots && passengerIndex < shuffleComponent.passengers.Count; j++)
                    {
                        Debug.Log(passengerIndex);
                        ref var passengerComponent = ref shuffleComponent.passengers[passengerIndex].Entity.Get<PassengerComponent>();
                        passengerComponent.renderer.material.color = carComponent.renderer.material.color;
                        passengerIndex++;
                    }
                }

                _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
                _shuffleEventfilter.GetEntity(shuffleEventEntity).Del<ShuffleEvent>();
            }
        }
    }

    private void StartConfirmBuyingEvent()
    {
        var confirmEventNewEntity = _ecsWorld.NewEntity();
        confirmEventNewEntity.Get<ConfirmBuyingEvent>();
        confirmEventNewEntity.Get<PassengerShuffleConfirmBuyingEvent>();
    }
}

using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShuffleSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<ShuffleComponent> _shuffleComponentFilter;
    private EcsFilter<ShuffleStartInitPassengersEvent> _shuffleStartInitPassengerFilter;
    private EcsFilter<ShuffleEvent> _shuffleEventfilter;
    private EcsFilter<ShowAdvToShuffleEvent> _showAdvToShuffleFilter;

    public string rewardID;
    private System.Random _random;

    public ShuffleSystem()
    {
        _random = new System.Random();
    }

    public void Run()
    {
        foreach (var shuffleComponentEntity in _shuffleComponentFilter)
        {
            ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

            foreach (var shuffleStartEntity in _shuffleStartInitPassengerFilter)
            {
                SortPassengerInColorCars(shuffleComponentEntity);
                _shuffleStartInitPassengerFilter.GetEntity(shuffleStartEntity).Del<ShuffleStartInitPassengersEvent>();
            }

            foreach (var shuffleEventEntity in _shuffleEventfilter)
            {
                if (shuffleComponent.cars.Count <= 1)
                    return;

                StartConfirmPayment();
                ShuffleCars(shuffleComponentEntity);
                SortPassengerInColorCars(shuffleComponentEntity);
                _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();

                _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
                _shuffleEventfilter.GetEntity(shuffleEventEntity).Del<ShuffleEvent>();
            }

            foreach (var showAdvShuffleEntity in _showAdvToShuffleFilter)
            {
                if (shuffleComponent.cars.Count <= 1)
                    return;

                YG2.RewardedAdvShow(rewardID, () =>
                {
                    ShuffleCars(shuffleComponentEntity);
                    SortPassengerInColorCars(shuffleComponentEntity);
                    _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();

                    _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
                    _ecsWorld.NewEntity().Get<CloseShuffleInfoShowerEvent>();
                });

                _showAdvToShuffleFilter.GetEntity(showAdvShuffleEntity).Del<ShowAdvToShuffleEvent>();
            }
        }
    }

    private void SortPassengerInColorCars(int shuffleComponentEntity)
    {
        ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

        int passengerIndex = 0;

        for (int i = 0; i < shuffleComponent.cars.Count && passengerIndex < shuffleComponent.passengers.Count; i++)
        {
            ref var carComponent = ref shuffleComponent.cars[i].Entity.Get<CarComponent>();

            for (int j = 0; j < carComponent.maxPassengersSlots && passengerIndex < shuffleComponent.passengers.Count; j++)
            {
                ref var passengerComponent = ref shuffleComponent.passengers[passengerIndex].Entity.Get<PassengerComponent>();
                passengerComponent.renderer.material.color = carComponent.renderer.material.color;
                passengerIndex++;
            }
        }
    }

    private void ShuffleCars(int shuffleComponentEntity)
    {
        ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

        for (int i = 0; i < shuffleComponent.cars.Count; i++)
        {
            int randomIndex = _random.Next(i + 1);

            ref var firstCarComponent = ref shuffleComponent.cars[i].Entity.Get<CarComponent>();
            ref var secondCarComponent = ref shuffleComponent.cars[randomIndex].Entity.Get<CarComponent>();

            Color tempFirstPassengerColor = firstCarComponent.renderer.material.color;
            Color templastPassengerColor = secondCarComponent.renderer.material.color;

            firstCarComponent.renderer.material.color = templastPassengerColor;
            secondCarComponent.renderer.material.color = tempFirstPassengerColor;
        }
    }

    private void StartConfirmPayment()
    {
        var confirmEventNewEntity = _ecsWorld.NewEntity();
        confirmEventNewEntity.Get<ConfirmBuyingEvent>();
        confirmEventNewEntity.Get<PassengerShuffleConfirmBuyingEvent>();
        _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
    }
}

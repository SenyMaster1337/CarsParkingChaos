using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShuffleSystem : IEcsRunSystem
{
    public string RewardID = "ShuffleRewardID";

    private EcsWorld _ecsWorld;
    private EcsFilter<ShuffleComponent> _shuffleComponentFilter;
    private EcsFilter<SortPassengerInColorCarsEvent> _SortPassengerInColorCarsFilter;
    private EcsFilter<ShuffleEvent> _shuffleEventfilter;
    private EcsFilter<ShowAdvToShuffleEvent> _showAdvToShuffleFilter;

    private System.Random _random;
    private int _shuffleCarsIterationCount;
    private int _firstIndexCars;
    private int _minCarsCoint;
    private bool _isPassengerSkipActive;

    public ShuffleSystem()
    {
        _random = new System.Random();
        _shuffleCarsIterationCount = 3;
        _firstIndexCars = 3;
        _minCarsCoint = 5;
        _isPassengerSkipActive = false;
    }

    public void Run()
    {
        foreach (var shuffleComponentEntity in _shuffleComponentFilter)
        {
            ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

            foreach (var shuffleStartEntity in _SortPassengerInColorCarsFilter)
            {
                //_isPassengerSkipActive = false;
                SortPassengerInColorCars(shuffleComponentEntity);
                //_isPassengerSkipActive = true;
                _SortPassengerInColorCarsFilter.GetEntity(shuffleStartEntity).Del<SortPassengerInColorCarsEvent>();
            }

            foreach (var shuffleEventEntity in _shuffleEventfilter)
            {
                if (shuffleComponent.cars.Count < _minCarsCoint)
                {
                    _ecsWorld.NewEntity().Get<ShowNotEnoughCarsToShuffleEvent>();
                    _shuffleEventfilter.GetEntity(shuffleEventEntity).Del<ShuffleEvent>();
                    return;
                }

                StartConfirmPayment();
                ShuffleCars(shuffleComponentEntity);
                SortPassengerInColorCars(shuffleComponentEntity);

                _shuffleEventfilter.GetEntity(shuffleEventEntity).Del<ShuffleEvent>();
            }

            foreach (var showAdvShuffleEntity in _showAdvToShuffleFilter)
            {
                if (shuffleComponent.cars.Count < _minCarsCoint)
                {
                    _ecsWorld.NewEntity().Get<ShowNotEnoughCarsToShuffleEvent>();
                    _showAdvToShuffleFilter.GetEntity(showAdvShuffleEntity).Del<ShowAdvToShuffleEvent>();
                    return;
                }

                YG2.RewardedAdvShow(RewardID, () =>
                {
                    if (RewardID == "ShuffleRewardID")
                    {
                        ShuffleCars(shuffleComponentEntity);
                        SortPassengerInColorCars(shuffleComponentEntity);
                    }
                });

                _ecsWorld.NewEntity().Get<CloseShuffleInfoShowerEvent>();
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
                //if (_isPassengerSkipActive && i <= _firstIndexCars)
                //{
                //    passengerIndex++;
                //    continue;
                //}

                ref var passengerComponent = ref shuffleComponent.passengers[passengerIndex].Entity.Get<PassengerComponent>();
                passengerComponent.renderer.material.color = carComponent.renderer.material.color;
                passengerIndex++;
            }
        }

        StartEnabledEvent();
    }

    private void ShuffleCars(int shuffleComponentEntity)
    {
        ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

        for (int i = 0; i < _shuffleCarsIterationCount; i++)
        {
            for (int j = 0; j < shuffleComponent.cars.Count; j++)
            {
                int randomIndex = _random.Next(j + 1);
                ref var firstCarComponent = ref shuffleComponent.cars[j].Entity.Get<CarComponent>();
                ref var secondCarComponent = ref shuffleComponent.cars[randomIndex].Entity.Get<CarComponent>();

                Color tempFirstPassengerColor = firstCarComponent.renderer.material.color;
                Color templastPassengerColor = secondCarComponent.renderer.material.color;

                firstCarComponent.renderer.material.color = templastPassengerColor;
                secondCarComponent.renderer.material.color = tempFirstPassengerColor;
            }
        }
    }

    private void StartConfirmPayment()
    {
        var confirmEventNewEntity = _ecsWorld.NewEntity();
        confirmEventNewEntity.Get<ConfirmBuyingEvent>();
        confirmEventNewEntity.Get<PassengerShuffleConfirmBuyingEvent>();
    }

    private void StartEnabledEvent()
    {
        _ecsWorld.NewEntity().Get<EnableRaycastReaderEvent>();
        _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
    }
}

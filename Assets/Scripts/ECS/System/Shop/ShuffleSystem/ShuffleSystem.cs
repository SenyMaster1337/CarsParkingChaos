using Leopotam.Ecs;
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
    private int _minCarsCoint;

    public ShuffleSystem()
    {
        _random = new System.Random();
        _shuffleCarsIterationCount = 3;
        _minCarsCoint = 5;
    }

    public void Run()
    {
        foreach (var shuffleComponentEntity in _shuffleComponentFilter)
        {
            ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

            foreach (var shuffleStartEntity in _SortPassengerInColorCarsFilter)
            {
                SortPassengerInColorAllCars(shuffleComponentEntity);
                _SortPassengerInColorCarsFilter.GetEntity(shuffleStartEntity).Del<SortPassengerInColorCarsEvent>();
            }

            foreach (var shuffleEventEntity in _shuffleEventfilter)
            {
                if (shuffleComponent.Cars.Count < _minCarsCoint)
                {
                    _ecsWorld.NewEntity().Get<ShowNotEnoughCarsToShuffleEvent>();
                    _shuffleEventfilter.GetEntity(shuffleEventEntity).Del<ShuffleEvent>();
                    return;
                }

                StartConfirmPayment();
                ReplaceColorCars(shuffleComponentEntity);
                SortPassengerInColorAllCars(shuffleComponentEntity);

                _shuffleEventfilter.GetEntity(shuffleEventEntity).Del<ShuffleEvent>();
            }

            foreach (var showAdvShuffleEntity in _showAdvToShuffleFilter)
            {
                if (shuffleComponent.Cars.Count < _minCarsCoint)
                {
                    _ecsWorld.NewEntity().Get<ShowNotEnoughCarsToShuffleEvent>();
                    _showAdvToShuffleFilter.GetEntity(showAdvShuffleEntity).Del<ShowAdvToShuffleEvent>();
                    return;
                }

                YG2.RewardedAdvShow(RewardID, () =>
                {
                    if (RewardID == "ShuffleRewardID")
                    {
                        ReplaceColorCars(shuffleComponentEntity);
                        SortPassengerInColorAllCars(shuffleComponentEntity);
                    }
                });

                _ecsWorld.NewEntity().Get<CloseShuffleInfoShowerEvent>();
                _showAdvToShuffleFilter.GetEntity(showAdvShuffleEntity).Del<ShowAdvToShuffleEvent>();
            }
        }
    }

    private void SortPassengerInColorAllCars(int shuffleComponentEntity)
    {
        ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

        int passengerIndex = 0;

        for (int i = 0; i < shuffleComponent.Cars.Count && passengerIndex < shuffleComponent.Passengers.Count; i++)
        {
            ref var carComponent = ref shuffleComponent.Cars[i].Entity.Get<CarComponent>();

            if (carComponent.IsNotEmptySeats)
                continue;

            for (int j = 0; j < carComponent.MaxPassengersSlots && passengerIndex < shuffleComponent.Passengers.Count; j++)
            {
                ref var passengerComponent = ref shuffleComponent.Passengers[passengerIndex].Entity.Get<PassengerComponent>();
                passengerComponent.Renderer.material.color = carComponent.Renderer.material.color;
                passengerIndex++;
            }
        }

        StartEnableInteractionGameEvents();
    }

    private void ReplaceColorCars(int shuffleComponentEntity)
    {
        ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

        for (int i = 0; i < _shuffleCarsIterationCount; i++)
        {
            for (int j = 0; j < shuffleComponent.Cars.Count; j++)
            {
                ref var firstCarComponent = ref shuffleComponent.Cars[j].Entity.Get<CarComponent>();
                ref var secondCarComponent = ref shuffleComponent.Cars[_random.Next(j + 1)].Entity.Get<CarComponent>();

                Color tempFirstPassengerColor = firstCarComponent.Renderer.material.color;
                Color templastPassengerColor = secondCarComponent.Renderer.material.color;

                firstCarComponent.Renderer.material.color = templastPassengerColor;
                secondCarComponent.Renderer.material.color = tempFirstPassengerColor;
            }
        }
    }

    private void StartConfirmPayment()
    {
        var confirmEventNewEntity = _ecsWorld.NewEntity();
        confirmEventNewEntity.Get<ConfirmBuyingEvent>();
        confirmEventNewEntity.Get<PassengerShuffleConfirmBuyingEvent>();
    }

    private void StartEnableInteractionGameEvents()
    {
        _ecsWorld.NewEntity().Get<EnableRaycastReaderEvent>();
        _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
    }
}

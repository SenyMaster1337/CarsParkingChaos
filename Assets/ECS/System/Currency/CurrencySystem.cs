using Leopotam.Ecs;

public class CurrencySystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CurrencyComponent> _filter;
    private EcsFilter<AddCoinsWinningEvent> _filterWinning;
    private EcsFilter<TryBuyEvent> _buyPassengerSortingFilter;
    private EcsFilter<ConfirmBuyingEvent> _confirmBuyingPassengerSortingFilter;

    private StaticData _staticData;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var currencyComponent = ref _filter.Get1(entity);

            foreach (var winningEntity in _filterWinning)
            {
                var winningEvent = _filterWinning.GetEntity(winningEntity);
                AddCoinsWinningEvent(ref currencyComponent);
                winningEvent.Del<AddCoinsWinningEvent>();
            }

            foreach (var buySortingEntity in _buyPassengerSortingFilter)
            {
                var sortingEvent = _buyPassengerSortingFilter.GetEntity(buySortingEntity);
                TryToBuy(currencyComponent, sortingEvent);
                sortingEvent.Del<TryBuyEvent>();
            }

            foreach (var confirmSortingBuyingEntity in _confirmBuyingPassengerSortingFilter)
            {
                var confirmSortingEvent = _confirmBuyingPassengerSortingFilter.GetEntity(confirmSortingBuyingEntity);
                ConfirmPurchase(ref currencyComponent, confirmSortingEvent);
                confirmSortingEvent.Del<ConfirmBuyingEvent>();
            }
        }
    }

    private void TryToBuy(CurrencyComponent currencyComponent, EcsEntity confirmSortingEvent)
    {
        if (confirmSortingEvent.Has<PassengerSortingComponent>())
        {
            if (currencyComponent.playerCoins >= _staticData.PriceSortPassengers)
            {
                _ecsWorld.NewEntity().Get<PassengerSortEvent>();
            }
            else
            {
                _ecsWorld.NewEntity().Get<ShowNotEnoughMoneyWindowEvent>();
            }
        }

        if (confirmSortingEvent.Has<ShuffleComponent>())
        {
            if (currencyComponent.playerCoins >= _staticData.PriceShufflePassengers)
            {
                _ecsWorld.NewEntity().Get<ShuffleEvent>();
            }
            else
            {
                _ecsWorld.NewEntity().Get<ShowNotEnoughMoneyWindowEvent>();
            }
        }
    }

    private void ConfirmPurchase(ref CurrencyComponent currencyComponent, EcsEntity confirmSortingEvent)
    {
        if (confirmSortingEvent.Has<PassengerSortingConfirmBuyingEvent>())
        {
            PayPurchase(ref currencyComponent, _staticData.PriceSortPassengers);
            _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
        }

        if (confirmSortingEvent.Has<PassengerShuffleConfirmBuyingEvent>())
        {
            PayPurchase(ref currencyComponent, _staticData.PriceShufflePassengers);
            _ecsWorld.NewEntity().Get<CloseShuffleInfoShowerEvent>();
        }
    }

    private void PayPurchase(ref CurrencyComponent currencyComponent, int value)
    {
        currencyComponent.playerCoins -= value;
        StartChangeCurrentCoinShowerEvent(currencyComponent.playerCoins);
        _ecsWorld.NewEntity().Get<YGSavePlayerCoinsCountEvent>();
        _ecsWorld.NewEntity().Get<YGSaveProgressEvent>();
    }

    private void AddCoinsWinningEvent(ref CurrencyComponent currencyComponent)
    {
        currencyComponent.playerCoins += _staticData.NumberCointAddedPerWin;
        StartChangeCurrentCoinShowerEvent(currencyComponent.playerCoins);
    }


    private void StartChangeCurrentCoinShowerEvent(int newCurrentCoins)
    {
        _ecsWorld.NewEntity().Get<ChangeShowCoinsValueEvent>() = new ChangeShowCoinsValueEvent { currentCoinsValue = newCurrentCoins };
    }
}

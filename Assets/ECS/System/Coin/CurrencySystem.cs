using Leopotam.Ecs;

public class CurrencySystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CurrencyComponent> _filter;
    private EcsFilter<AddCoinsWinningEvent> _filterWinning;
    private EcsFilter<BuyPassengerSortingEvent> _buyPassengerSortingFilter;
    private EcsFilter<ConfirmPassengerSortingBuyingEvent> _confirmBuyingPassengerSortingFilter;

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
                TryToBuyPassengerSorting(currencyComponent);
                sortingEvent.Del<BuyPassengerSortingEvent>();
            }

            foreach (var confirmSortingBuyingEntity in _confirmBuyingPassengerSortingFilter)
            {
                var confirmSortingEvent = _confirmBuyingPassengerSortingFilter.GetEntity(confirmSortingBuyingEntity);
                ConfirmBuyingPassengerSorting(ref currencyComponent);
                confirmSortingEvent.Del<ConfirmPassengerSortingBuyingEvent>();
            }
        }
    }

    private void ConfirmBuyingPassengerSorting(ref CurrencyComponent currencyComponent)
    {
        currencyComponent.playerCoins -= _staticData.PriceSortPassengers;
        StartChangeCurrentCoinShowerEvent(currencyComponent.playerCoins);
        _ecsWorld.NewEntity().Get<YGSaveProgressEvent>();
        _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
    }

    private void TryToBuyPassengerSorting(CurrencyComponent currencyComponent)
    {
        if (currencyComponent.playerCoins >= _staticData.PriceSortPassengers)
        {
            _ecsWorld.NewEntity().Get<SortPassengerEvent>();
        }
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

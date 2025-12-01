using Leopotam.Ecs;

public class CurrencySystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CurrencyComponent> _filter;
    private EcsFilter<AddCoinsWinningEvent> _filterWinning;
    private EcsFilter<BuyPassengerSortingEvent> _buyPassengerSortingFilter;

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
                DeductCoinsBuyingPassengerSorting(ref currencyComponent);
                sortingEvent.Del<BuyPassengerSortingEvent>();
            }
        }
    }

    private void AddCoinsWinningEvent(ref CurrencyComponent currencyComponent)
    {
        currencyComponent.playerCoins += _staticData.NumberCointAddedPerWin;
        StartChangeCurrentCoinShowerEvent(currencyComponent.playerCoins);
    }

    private void DeductCoinsBuyingPassengerSorting(ref CurrencyComponent currencyComponent)
    {
        if (currencyComponent.playerCoins >= _staticData.PriceSortPassengers)
        {
            currencyComponent.playerCoins -= _staticData.PriceSortPassengers;
            StartChangeCurrentCoinShowerEvent(currencyComponent.playerCoins);
        }
    }

    private void StartChangeCurrentCoinShowerEvent(int newCurrentCoins)
    {
        _ecsWorld.NewEntity().Get<ChangeShowCoinsValueEvent>() = new ChangeShowCoinsValueEvent { currentCoinsValue = newCurrentCoins };
    }
}

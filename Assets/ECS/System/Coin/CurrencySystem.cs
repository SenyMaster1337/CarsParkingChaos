using Leopotam.Ecs;

public class CurrencySystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CurrencyComponent> _filter;
    private EcsFilter<AddCoinsWinningEvent> _filterWinning;
    private StaticData _staticData;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var currencyComponent = ref _filter.Get1(entity);
            AddCoinsEvent(ref currencyComponent);
        }
    }

    private void AddCoinsEvent(ref CurrencyComponent currencyComponent)
    {
        foreach (var winningEntity in _filterWinning)
        {
            var winningEvent = _filterWinning.GetEntity(winningEntity);
            currencyComponent.playerCoins += _staticData.NumberCointAddedPerWin;
            _ecsWorld.NewEntity().Get<ChangeShowCoinsValueEvent>() = new ChangeShowCoinsValueEvent { currentCoinsValue = currencyComponent.playerCoins };
            winningEvent.Del<AddCoinsWinningEvent>();
        }
    }
}

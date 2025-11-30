using Leopotam.Ecs;
using YG;

public class CurrencyInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    public void Init()
    {
        var newCurrencyEntity = _ecsWorld.NewEntity();

        ref var currencyComponent = ref newCurrencyEntity.Get<CurrencyComponent>();
        currencyComponent.playerCoins = YG2.saves.coins;
        StartChangeCoinValueEvent(currencyComponent);
    }

    private void StartChangeCoinValueEvent(CurrencyComponent currencyComponent)
    {
        _ecsWorld.NewEntity().Get<ChangeShowCoinsValueEvent>() = new ChangeShowCoinsValueEvent
        {
            currentCoinsValue = currencyComponent.playerCoins
        };
    }
}

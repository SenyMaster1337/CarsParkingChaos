using Leopotam.Ecs;
using YG;

public class CurrencyInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    public void Init()
    {
        var newCurrencyEntity = _ecsWorld.NewEntity();

        ref var currencyComponent = ref newCurrencyEntity.Get<CurrencyComponent>();
        currencyComponent.PlayerCoins = YG2.saves.Coins;
        StartChangeCoinValueEvent(currencyComponent);
    }

    private void StartChangeCoinValueEvent(CurrencyComponent currencyComponent)
    {
        _ecsWorld.NewEntity().Get<ChangeShowCoinsValueEvent>() = new ChangeShowCoinsValueEvent
        {
            CurrentCoinsValue = currencyComponent.PlayerCoins,
        };
    }
}

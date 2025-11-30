using Leopotam.Ecs;

public class CurrencyShowInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private CoinCountText _coinCountText;

    public CurrencyShowInitSystem(CoinCountText coinCountText)
    {
        _coinCountText = coinCountText;
    }

    public void Init()
    {
        var newCointTextEntity = _ecsWorld.NewEntity();

        ref var currencyComponent = ref newCointTextEntity.Get<CurrencyShowComponent>();
        currencyComponent.coinCountText = _coinCountText;
    }
}

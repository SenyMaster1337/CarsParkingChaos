using Leopotam.Ecs;

public class CurrencyShowInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private CurrentCoinCountText _coinCountText;

    public CurrencyShowInitSystem(CurrentCoinCountText coinCountText)
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

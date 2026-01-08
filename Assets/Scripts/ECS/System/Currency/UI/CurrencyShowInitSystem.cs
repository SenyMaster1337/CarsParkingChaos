using Leopotam.Ecs;
using CarParkingChaos.UI.Text;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
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
            currencyComponent.CoinCountText = _coinCountText;
        }
    }
}

using Leopotam.Ecs;

namespace CarParkingChaos.ECS.Systems
{
    public class CurrencyShowSystem : IEcsRunSystem
    {
        private EcsFilter<CurrencyShowComponent> _filter;
        private EcsFilter<ChangeShowCoinsValueEvent> _filterWinning;

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var currencyShowComponent = ref _filter.Get1(entity);
                ChangeCoinsValue(currencyShowComponent);
            }
        }

        private void ChangeCoinsValue(CurrencyShowComponent currencyShowComponent)
        {
            foreach (var changeEntity in _filterWinning)
            {
                ref var changeCoinsEventComponent = ref _filterWinning.Get1(changeEntity);
                currencyShowComponent.CoinCountText.Value.SetText($"{changeCoinsEventComponent.CurrentCoinsValue}");

                var changeEvent = _filterWinning.GetEntity(changeEntity);
                changeEvent.Del<ChangeShowCoinsValueEvent>();
            }
        }
    }
}

using Leopotam.Ecs;
using CarParkingChaos.UI.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class ShopShowerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private ShopShower _shopShower;

        public ShopShowerInitSystem(ShopShower shopShower)
        {
            _shopShower = shopShower;
        }

        public void Init()
        {
            var shopShowerNewEntity = _ecsWorld.NewEntity();

            ref var shopShowerComponent = ref shopShowerNewEntity.Get<ShopShowerComponent>();
            shopShowerComponent.ShopShower = _shopShower;

            shopShowerComponent.ShopShower.WindowGroup.alpha = 0f;
            shopShowerComponent.ShopShower.WindowGroup.interactable = false;
            shopShowerComponent.ShopShower.WindowGroup.blocksRaycasts = false;

            shopShowerComponent.ShopShower.NotCarsToSortingWindow.WindowGroup.alpha = 0f;
            shopShowerComponent.ShopShower.NotCarsToSortingWindow.WindowGroup.interactable = false;
            shopShowerComponent.ShopShower.NotCarsToSortingWindow.WindowGroup.blocksRaycasts = false;

            shopShowerComponent.ShopShower.NotEnoughMoneyWindow.WindowGroup.alpha = 0f;
            shopShowerComponent.ShopShower.NotEnoughMoneyWindow.WindowGroup.interactable = false;
            shopShowerComponent.ShopShower.NotEnoughMoneyWindow.WindowGroup.blocksRaycasts = false;

            shopShowerComponent.ShopShower.NotEnoughCarsToShuffleWindow.WindowGroup.alpha = 0f;
            shopShowerComponent.ShopShower.NotEnoughCarsToShuffleWindow.WindowGroup.interactable = false;
            shopShowerComponent.ShopShower.NotEnoughCarsToShuffleWindow.WindowGroup.blocksRaycasts = false;
        }
    }
}

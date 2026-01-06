using Leopotam.Ecs;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.UI.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class PassengerSortingShowerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;

        private PassengerSortingShower _buyPassengerSortingShower;
        private StaticData _staticData;

        public PassengerSortingShowerInitSystem(PassengerSortingShower buyPassengerSortingShower)
        {
            _buyPassengerSortingShower = buyPassengerSortingShower;
        }

        public void Init()
        {
            var sortingPassengerNewEntity = _ecsWorld.NewEntity();

            ref var sortingPassengerComponent = ref sortingPassengerNewEntity.Get<PassengerSortingShowerComponent>();
            sortingPassengerComponent.BuyPassengerSortingShower = _buyPassengerSortingShower;

            sortingPassengerComponent.BuyPassengerSortingShower.WindowGroup.alpha = 0f;
            sortingPassengerComponent.BuyPassengerSortingShower.WindowGroup.interactable = false;
            sortingPassengerComponent.BuyPassengerSortingShower.WindowGroup.blocksRaycasts = false;

            sortingPassengerComponent.BuyPassengerSortingShower.PriceBuyingPassengerSortingText.Value.SetText($"{_staticData.PriceSortPassengers}");
            sortingPassengerComponent.BuyPassengerSortingShower.PriceBuyingPassengerSortingShopAsssortmentMenuText.Value.SetText($"{_staticData.PriceSortPassengers}");
        }
    }
}

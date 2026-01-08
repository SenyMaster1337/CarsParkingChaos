using Leopotam.Ecs;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.ECS.Components;
using CarParkingChaos.UI.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class CarShuffleShowerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;

        private CarShuffleShower _buyPassengerShuffle;
        private StaticData _staticData;

        public CarShuffleShowerInitSystem(
            CarShuffleShower buyPassengerShuffleShower)
        {
            _buyPassengerShuffle = buyPassengerShuffleShower;
        }

        public void Init()
        {
            var sortingPassengerNewEntity = _ecsWorld.NewEntity();

            ref var sortingPassengerComponent =
                ref sortingPassengerNewEntity.Get<PassengerShuffleShowerComponent>();
            sortingPassengerComponent.BuyPassengerShuffleShower =
                _buyPassengerShuffle;

            sortingPassengerComponent.BuyPassengerShuffleShower.WindowGroup
                .alpha = 0f;
            sortingPassengerComponent.BuyPassengerShuffleShower.WindowGroup
                .interactable = false;
            sortingPassengerComponent.BuyPassengerShuffleShower.WindowGroup
                .blocksRaycasts = false;

            sortingPassengerComponent.BuyPassengerShuffleShower
                .PriceBuyingCarShuffleText.Value
                .SetText($"{_staticData.PriceShufflePassengers}");
            sortingPassengerComponent.BuyPassengerShuffleShower
                .PriceBuyingCarShuffleShopAsssortmentMenuText.Value
                .SetText($"{_staticData.PriceShufflePassengers}");
        }
    }
}
using Leopotam.Ecs;

public class CarShuffleShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private BuyShuffleShower _buyPassengerShuffle;
    private StaticData _staticData;

    public CarShuffleShowerInitSystem(BuyShuffleShower buyPassengerShuffleShower)
    {
        _buyPassengerShuffle = buyPassengerShuffleShower;
    }

    public void Init()
    {
        var sortingPassengerNewEntity = _ecsWorld.NewEntity();

        ref var sortingPassengerComponent = ref sortingPassengerNewEntity.Get<PassengerShuffleShowerComponent>();
        sortingPassengerComponent.buyPassengerShuffleShower = _buyPassengerShuffle;

        sortingPassengerComponent.buyPassengerShuffleShower.WindowGroup.alpha = 0f;
        sortingPassengerComponent.buyPassengerShuffleShower.WindowGroup.interactable = false;
        sortingPassengerComponent.buyPassengerShuffleShower.WindowGroup.blocksRaycasts = false;

        sortingPassengerComponent.buyPassengerShuffleShower.PriceBuyingCarShuffleText.Value.SetText($"{_staticData.PriceShufflePassengers}");
        sortingPassengerComponent.buyPassengerShuffleShower.PriceBuyingCarShuffleShopAsssortmentMenuText.Value.SetText($"{_staticData.PriceShufflePassengers}");
    }
}

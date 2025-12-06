using Leopotam.Ecs;

public class PassengerShuffleShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private BuyPassengerShuffleShower _buyPassengerShuffle;
    private StaticData _staticData;

    public PassengerShuffleShowerInitSystem(BuyPassengerShuffleShower buyPassengerShuffleShower)
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

        sortingPassengerComponent.buyPassengerShuffleShower.PriceBuyingPassengerShuffleText.Value.SetText($"{_staticData.PriceSortPassengers}");
        sortingPassengerComponent.buyPassengerShuffleShower.PriceBuyingPassengerShuffleShopAsssortmentMenuText.Value.SetText($"{_staticData.PriceSortPassengers}");
    }
}

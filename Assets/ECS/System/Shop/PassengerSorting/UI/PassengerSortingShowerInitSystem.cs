using Leopotam.Ecs;

public class PassengerSortingShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private BuyPassengerSortingShower _buyPassengerSortingShower;
    private StaticData _staticData;

    public PassengerSortingShowerInitSystem(BuyPassengerSortingShower buyPassengerSortingShower)
    {
        _buyPassengerSortingShower = buyPassengerSortingShower;
    }

    public void Init()
    {
        var sortingPassengerNewEntity = _ecsWorld.NewEntity();

        ref var sortingPassengerComponent = ref sortingPassengerNewEntity.Get<PassengerSortingShowerComponent>();
        sortingPassengerComponent.buyPassengerSortingShower = _buyPassengerSortingShower;

        sortingPassengerComponent.buyPassengerSortingShower.WindowGroup.alpha = 0f;
        sortingPassengerComponent.buyPassengerSortingShower.WindowGroup.interactable = false;
        sortingPassengerComponent.buyPassengerSortingShower.WindowGroup.blocksRaycasts = false;

        sortingPassengerComponent.buyPassengerSortingShower.PriceBuyingPassengerSortingText.Value.SetText($"{_staticData.PriceSortPassengers}");
        sortingPassengerComponent.buyPassengerSortingShower.PriceBuyingPassengerSortingShopAsssortmentMenuText.Value.SetText($"{_staticData.PriceSortPassengers}");
    }
}

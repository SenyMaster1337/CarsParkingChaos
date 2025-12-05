using Leopotam.Ecs;

public class PassengerSortingShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private BuyPassengerSortingShower _buySortingPassengersShower;
    private StaticData _staticData;

    public PassengerSortingShowerInitSystem(BuyPassengerSortingShower buySortingPassengersShower)
    {
        _buySortingPassengersShower = buySortingPassengersShower;
    }

    public void Init()
    {
        var sortingPassengerNewEntity = _ecsWorld.NewEntity();

        ref var sortingPassengerComponent = ref sortingPassengerNewEntity.Get<PassengerSortingShowerComponent>();
        sortingPassengerComponent.buySortingPassengersShower = _buySortingPassengersShower;

        sortingPassengerComponent.buySortingPassengersShower.WindowGroup.alpha = 0f;
        sortingPassengerComponent.buySortingPassengersShower.WindowGroup.interactable = false;
        sortingPassengerComponent.buySortingPassengersShower.WindowGroup.blocksRaycasts = false;

        sortingPassengerComponent.buySortingPassengersShower.PriceBuyingPassengerSortingText.Value.SetText($"{_staticData.PriceSortPassengers}");
        sortingPassengerComponent.buySortingPassengersShower.PriceBuyingPassengerSortingShopAsssortmentMenuText.Value.SetText($"{_staticData.PriceSortPassengers}");
    }
}

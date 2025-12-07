using Leopotam.Ecs;
using UnityEngine;

public class PassengerSortingUIButtonsReader : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _ecsWorld;

    private BuyPassengerSortingShower _buyPassengerSortingShower;

    public PassengerSortingUIButtonsReader(BuyPassengerSortingShower buyPassengerSortingShower)
    {
        _buyPassengerSortingShower = buyPassengerSortingShower;
    }

    public void Init()
    {
        _buyPassengerSortingShower.OpenBuyingPassengerSortingButtonClickReader.OnButtonClicked += OnButtonClickOpenBuyingPassengerSorting;
        _buyPassengerSortingShower.AcceptBuyingPassengersSortingButtonClickReader.OnButtonClicked += OnButtonClickAcceptBuyingPassengerSorting;
        _buyPassengerSortingShower.DeclineBuyingPassengerSortingButtonClickReader.OnButtonClicked += OnButtonClickDeclineBuyingPassengerSorting;
    }

    public void Destroy()
    {
        _buyPassengerSortingShower.OpenBuyingPassengerSortingButtonClickReader.OnButtonClicked -= OnButtonClickOpenBuyingPassengerSorting;
        _buyPassengerSortingShower.AcceptBuyingPassengersSortingButtonClickReader.OnButtonClicked -= OnButtonClickAcceptBuyingPassengerSorting;
        _buyPassengerSortingShower.DeclineBuyingPassengerSortingButtonClickReader.OnButtonClicked -= OnButtonClickDeclineBuyingPassengerSorting;
    }

    private void OnButtonClickOpenBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<OpenPassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<CloseShopEvent>();
    }

    private void OnButtonClickAcceptBuyingPassengerSorting()
    {
        var TryBuyEventNewEntity = _ecsWorld.NewEntity();
        TryBuyEventNewEntity.Get<TryBuyEvent>();
        TryBuyEventNewEntity.Get<PassengerSortingComponent>();

        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickDeclineBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<OpenShopEvent>();
    }
}

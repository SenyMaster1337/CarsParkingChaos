using Leopotam.Ecs;

public class PassengerSortingUIButtonsReader : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _ecsWorld;

    private PassengerSortingShower _buyPassengerSortingShower;

    public PassengerSortingUIButtonsReader(PassengerSortingShower buyPassengerSortingShower)
    {
        _buyPassengerSortingShower = buyPassengerSortingShower;
    }

    public void Init()
    {
        _buyPassengerSortingShower.OpenBuyingPassengerSortingButton.OnButtonClicked += OnButtonClickOpen;
        _buyPassengerSortingShower.AcceptBuyingPassengersSortingButton.OnButtonClicked += OnButtonClickAccept;
        _buyPassengerSortingShower.DeclineBuyingPassengerSortingButton.OnButtonClicked += OnButtonClickDecline;
        _buyPassengerSortingShower.RewardPassengerSortingButton.OnButtonClicked += OnButtonClickReward;
    }

    public void Destroy()
    {
        _buyPassengerSortingShower.OpenBuyingPassengerSortingButton.OnButtonClicked -= OnButtonClickOpen;
        _buyPassengerSortingShower.AcceptBuyingPassengersSortingButton.OnButtonClicked -= OnButtonClickAccept;
        _buyPassengerSortingShower.DeclineBuyingPassengerSortingButton.OnButtonClicked -= OnButtonClickDecline;
        _buyPassengerSortingShower.RewardPassengerSortingButton.OnButtonClicked -= OnButtonClickReward;
    }

    private void OnButtonClickOpen()
    {
        _ecsWorld.NewEntity().Get<OpenPassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<CloseShopEvent>();
    }

    private void OnButtonClickAccept()
    {
        var TryBuyEventNewEntity = _ecsWorld.NewEntity();
        TryBuyEventNewEntity.Get<TryBuyEvent>();
        TryBuyEventNewEntity.Get<PassengerSortingComponent>();

        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickDecline()
    {
        _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<OpenShopEvent>();
    }

    private void OnButtonClickReward()
    {
        _ecsWorld.NewEntity().Get<ShowAdvToPassengerSortEvent>();

        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }
}

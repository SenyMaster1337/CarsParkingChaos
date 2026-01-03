using Leopotam.Ecs;

public class CarShuffleUIButtonsReader : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _ecsWorld;
    private CarShuffleShower _buyPassengerShuffleShower;

    public CarShuffleUIButtonsReader(CarShuffleShower buyPassengerShuffleShower)
    {
        _buyPassengerShuffleShower = buyPassengerShuffleShower;
    }

    public void Init()
    {
        _buyPassengerShuffleShower.OpenBuyingCarShuffleButton.OnButtonClicked += OnButtonClickOpen;
        _buyPassengerShuffleShower.AcceptBuyingCarShuffleButton.OnButtonClicked += OnButtonClickAccept;
        _buyPassengerShuffleShower.DeclineBuyingCarShuffleButton.OnButtonClicked += OnButtonClickDecline;
        _buyPassengerShuffleShower.RewardShuffleButton.OnButtonClicked += OnButtonClickReward;
    }

    public void Destroy()
    {
        _buyPassengerShuffleShower.OpenBuyingCarShuffleButton.OnButtonClicked -= OnButtonClickOpen;
        _buyPassengerShuffleShower.AcceptBuyingCarShuffleButton.OnButtonClicked -= OnButtonClickAccept;
        _buyPassengerShuffleShower.DeclineBuyingCarShuffleButton.OnButtonClicked -= OnButtonClickDecline;
        _buyPassengerShuffleShower.RewardShuffleButton.OnButtonClicked -= OnButtonClickReward;
    }

    private void OnButtonClickOpen()
    {
        _ecsWorld.NewEntity().Get<OpenPassengerShuffleInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<CloseShopEvent>();
    }

    private void OnButtonClickAccept()
    {
        var TryBuyEventNewEntity = _ecsWorld.NewEntity();
        TryBuyEventNewEntity.Get<TryBuyEvent>();
        TryBuyEventNewEntity.Get<ShuffleComponent>();

        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickDecline()
    {
        _ecsWorld.NewEntity().Get<CloseShuffleInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<OpenShopEvent>();
    }

    private void OnButtonClickReward()
    {
        _ecsWorld.NewEntity().Get<ShowAdvToShuffleEvent>();
    }
}

using Leopotam.Ecs;
using UnityEngine;

public class CarShuffleUIButtonsReader : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _ecsWorld;
    private BuyCarShuffleShower _buyPassengerShuffleShower;

    public CarShuffleUIButtonsReader(BuyCarShuffleShower buyPassengerShuffleShower)
    {
        _buyPassengerShuffleShower = buyPassengerShuffleShower;
    }

    public void Init()
    {
        _buyPassengerShuffleShower.OpenBuyingCarShuffleButton.OnButtonClicked += OnButtonClickOpenBuyingPassengerShuffle;
        _buyPassengerShuffleShower.AcceptBuyingCarShuffleButton.OnButtonClicked += OnButtonClickAcceptBuyingPassengerShuffle;
        _buyPassengerShuffleShower.DeclineBuyingCarShuffleButton.OnButtonClicked += OnButtonClickDeclineBuyingPassengerShuffle;
    }

    public void Destroy()
    {
        _buyPassengerShuffleShower.OpenBuyingCarShuffleButton.OnButtonClicked -= OnButtonClickOpenBuyingPassengerShuffle;
        _buyPassengerShuffleShower.AcceptBuyingCarShuffleButton.OnButtonClicked -= OnButtonClickAcceptBuyingPassengerShuffle;
        _buyPassengerShuffleShower.DeclineBuyingCarShuffleButton.OnButtonClicked -= OnButtonClickDeclineBuyingPassengerShuffle;
    }

    private void OnButtonClickOpenBuyingPassengerShuffle()
    {
        _ecsWorld.NewEntity().Get<OpenPassengerShuffleInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<CloseShopEvent>();
    }

    private void OnButtonClickAcceptBuyingPassengerShuffle()
    {
        var TryBuyEventNewEntity = _ecsWorld.NewEntity();
        TryBuyEventNewEntity.Get<TryBuyEvent>();
        TryBuyEventNewEntity.Get<ShuffleComponent>();

        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickDeclineBuyingPassengerShuffle()
    {
        _ecsWorld.NewEntity().Get<CloseCarShuffleInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<OpenShopEvent>();
    }
}

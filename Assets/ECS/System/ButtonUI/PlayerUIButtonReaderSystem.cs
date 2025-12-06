using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIButtonReaderSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<EnableButtonsEvent> _enableFilter;
    private EcsFilter<DisableButtonsEvent> _disableFilter;

    private SoundMuteToggle _soundMueToggle;
    private RestartButtonClickReader _restartButtonClickReader;
    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;
    private LeaderboradShower _leaderboradShower;
    private ShopShower _shopShower;

    public PlayerUIButtonReaderSystem(SoundMuteToggle soundMuteToggle, RestartButtonClickReader restartButtonClickReader, LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower, LeaderboradShower leaderboradShower, ShopShower shopShower)
    {
        _soundMueToggle = soundMuteToggle;
        _restartButtonClickReader = restartButtonClickReader;
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
        _leaderboradShower = leaderboradShower;
        _shopShower = shopShower;
    }

    public void Init()
    {
        _soundMueToggle.MuteSoundButtonClickReader.OnButtonClicked += OnButtonClickMuteSound;
        _soundMueToggle.UnmuteSoundButtonClickReader.OnButtonClicked += OnButtonClickUnmuteSound;
        _restartButtonClickReader.OnButtonClicked += OnButtonClickRestart;

        _levelCompleteShower.NextLevelButtonClickReader.OnButtonClicked += OnButtonCkickNextLevel;
        _levelLossShower.RestartButtonClickReader.OnButtonClicked += OnButtonClickRestart;

        _leaderboradShower.LeaderboradOpenButtonClick.OnButtonClicked += OnButtonClickOpenLeaderboard;
        _leaderboradShower.LeaderboradCloseButtonClick.OnButtonClicked += OnButtonClickCloseLeaderboard;

        _shopShower.OpenShopButtonClickReader.OnButtonClicked += OnButtonClickOpenShop;
        _shopShower.CloseShopButtonClickReader.OnButtonClicked += OnButtonClickCloseShop;

        _shopShower.BuyPassengerSortingShower.OpenBuyingPassengerSortingButtonClickReader.OnButtonClicked += OnButtonClickOpenBuyingPassengerSorting;
        _shopShower.BuyPassengerSortingShower.AcceptBuyingPassengersSortingButtonClickReader.OnButtonClicked += OnButtonClickAcceptBuyingPassengerSorting;
        _shopShower.BuyPassengerSortingShower.DeclineBuyingPassengerSortingButtonClickReader.OnButtonClicked += OnButtonClickDeclineBuyingPassengerSorting;

        _shopShower.BuyPassengerShuffleShower.OpenBuyingPassengerMixerButtonClickReader.OnButtonClicked += OnButtonClickOpenBuyingPassengerSorting;
        _shopShower.BuyPassengerShuffleShower.AcceptBuyingPassengerMixerButtonClickReader.OnButtonClicked += OnButtonClickAcceptBuyingPassengerSorting;
        _shopShower.BuyPassengerShuffleShower.DeclineBuyingPassengerMixerButtonClickReader.OnButtonClicked += OnButtonClickDeclineBuyingPassengerSorting;
    }

    public void Destroy()
    {
        _soundMueToggle.MuteSoundButtonClickReader.OnButtonClicked -= OnButtonClickMuteSound;
        _soundMueToggle.UnmuteSoundButtonClickReader.OnButtonClicked -= OnButtonClickUnmuteSound;
        _restartButtonClickReader.OnButtonClicked -= OnButtonClickRestart;

        _levelCompleteShower.NextLevelButtonClickReader.OnButtonClicked -= OnButtonCkickNextLevel;
        _levelLossShower.RestartButtonClickReader.OnButtonClicked -= OnButtonClickRestart;

        _leaderboradShower.LeaderboradOpenButtonClick.OnButtonClicked -= OnButtonClickOpenLeaderboard;
        _leaderboradShower.LeaderboradCloseButtonClick.OnButtonClicked -= OnButtonClickCloseLeaderboard;

        _shopShower.OpenShopButtonClickReader.OnButtonClicked -= OnButtonClickOpenShop;
        _shopShower.CloseShopButtonClickReader.OnButtonClicked -= OnButtonClickCloseShop;

        _shopShower.BuyPassengerSortingShower.OpenBuyingPassengerSortingButtonClickReader.OnButtonClicked -= OnButtonClickOpenBuyingPassengerSorting;
        _shopShower.BuyPassengerSortingShower.AcceptBuyingPassengersSortingButtonClickReader.OnButtonClicked -= OnButtonClickAcceptBuyingPassengerSorting;
        _shopShower.BuyPassengerSortingShower.DeclineBuyingPassengerSortingButtonClickReader.OnButtonClicked -= OnButtonClickDeclineBuyingPassengerSorting;

        _shopShower.BuyPassengerShuffleShower.OpenBuyingPassengerMixerButtonClickReader.OnButtonClicked -= OnButtonClickOpenBuyingPassengerSorting;
        _shopShower.BuyPassengerShuffleShower.AcceptBuyingPassengerMixerButtonClickReader.OnButtonClicked -= OnButtonClickAcceptBuyingPassengerSorting;
        _shopShower.BuyPassengerShuffleShower.DeclineBuyingPassengerMixerButtonClickReader.OnButtonClicked -= OnButtonClickDeclineBuyingPassengerSorting;
    }

    public void Run()
    {
        foreach (var enableEntity in _enableFilter)
        {
            EnableButtons();
            _enableFilter.GetEntity(enableEntity).Del<EnableButtonsEvent>();
        }

        foreach (var disableEntity in _disableFilter)
        {
            DisableButtons();
            _disableFilter.GetEntity(disableEntity).Del<DisableButtonsEvent>();
        }
    }

    private void OnButtonClickMuteSound()
    {
        _ecsWorld.NewEntity().Get<MuteSoundEvent>();
    }

    private void OnButtonClickUnmuteSound()
    {
        _ecsWorld.NewEntity().Get<UnmuteSoundEvent>();
    }

    private void OnButtonClickRestart()
    {
        _ecsWorld.NewEntity().Get<RestartLevelEvent>();
    }

    private void OnButtonCkickNextLevel()
    {
        _ecsWorld.NewEntity().Get<LoadNextLevelEvent>();
    }

    private void OnButtonClickOpenLeaderboard()
    {
        _ecsWorld.NewEntity().Get<OpenLeaderboardEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
        DisableButtons();
    }

    private void OnButtonClickCloseLeaderboard()
    {
        _ecsWorld.NewEntity().Get<CloseLeaderboardEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
        EnableButtons();
    }

    private void OnButtonClickOpenShop()
    {
        _ecsWorld.NewEntity().Get<OpenShopEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
        DisableButtons();
    }

    private void OnButtonClickCloseShop()
    {
        _ecsWorld.NewEntity().Get<CloseShopEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
        EnableButtons();
    }

    private void OnButtonClickOpenBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<OpenPassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<CloseShopEvent>();
    }

    private void OnButtonClickAcceptBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<BuyPassengerSortingEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickDeclineBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<OpenShopEvent>();
    }

    private void OnButtonClickOpenBuyingPassengerShuffle()
    {
        _ecsWorld.NewEntity().Get<OpenPassengerShuffleInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<CloseShopEvent>();
    }

    private void OnButtonClickAcceptBuyingPassengerShuffle()
    {
        //_ecsWorld.NewEntity().Get<BuyPassengerSortingEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickDeclineBuyingPassengerShuffle()
    {
        _ecsWorld.NewEntity().Get<ClosePassengerShuffleInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<OpenShopEvent>();
    }

    private void EnableButtons()
    {
        _restartButtonClickReader.Button.interactable = true;
        _leaderboradShower.LeaderboradOpenButtonClick.Button.interactable = true;
        _shopShower.OpenShopButtonClickReader.Button.interactable = true;
    }

    private void DisableButtons()
    {
        _restartButtonClickReader.Button.interactable = false;
        _leaderboradShower.LeaderboradOpenButtonClick.Button.interactable = false;
        _shopShower.OpenShopButtonClickReader.Button.interactable = false;
    }
}

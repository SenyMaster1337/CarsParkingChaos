using Leopotam.Ecs;
using System;

public class PlayerUIButtonReaderSystem : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _ecsWorld;

    private MenuSettingsShower _menuSettings;
    private RestartButtonClickReader _restartButtonClickReader;
    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;
    private LeaderboradShower _leaderboradShower;
    private BuySortingPassengersShower _buySortingPassengersShower;

    public PlayerUIButtonReaderSystem(MenuSettingsShower menuSettings, RestartButtonClickReader restartButtonClickReader, LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower, LeaderboradShower leaderboradShower, BuySortingPassengersShower buySortingPassengersShower)
    {
        _menuSettings = menuSettings;
        _restartButtonClickReader = restartButtonClickReader;
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
        _leaderboradShower = leaderboradShower;
        _buySortingPassengersShower = buySortingPassengersShower;
    }

    public void Init()
    {
        _menuSettings.SettingsOpenButtonClick.OnButtonClicked += OnButtonClickOpenMenuSettings;
        _menuSettings.SettingsCloseButtonClick.OnButtonClicked += OnButtonClickCloseMenuSettings;
        _menuSettings.SoundMuteToggle.MuteSoundButtonClickReader.OnButtonClicked += OnButtonClickMuteSound;
        _menuSettings.SoundMuteToggle.UnmuteSoundButtonClickReader.OnButtonClicked += OnButtonClickUnmuteSound;
        _menuSettings.MusicMuteToggle.MuteMusicButtonClickReader.OnButtonClicked += OnButtonClickMuteMusic;
        _menuSettings.MusicMuteToggle.UnmuteMusicButtonClickReader.OnButtonClicked += OnButtonClickUnmuteMusic;
        _restartButtonClickReader.OnButtonClicked += OnButtonClickRestart;
        _levelCompleteShower.NextLevelButtonClickReader.OnButtonClicked += OnButtonCkickNextLevel;
        _levelLossShower.RestartButtonClickReader.OnButtonClicked += OnButtonClickRestart;
        _leaderboradShower.LeaderboradOpenButtonClick.OnButtonClicked += OnButtonClickOpenLeaderboard;
        _leaderboradShower.LeaderboradCloseButtonClick.OnButtonClicked += OnButtonClickCloseLeaderboard;
        _buySortingPassengersShower.OpenBuyingPassengerSortingButtonClickReader.OnButtonClicked += OnButtonClickOpenBuyingPassengerSorting;
        _buySortingPassengersShower.AcceptBuyingPassengersSortingButtonClickReader.OnButtonClicked += OnButtonClickAcceptBuyingPassengerSorting;
        _buySortingPassengersShower.DeclineBuyingPassengerSortingButtonClickReader.OnButtonClicked += OnButtonClickDeclineBuyingPassengerSorting;
    }

    public void Destroy()
    {
        _menuSettings.SettingsOpenButtonClick.OnButtonClicked -= OnButtonClickOpenMenuSettings;
        _menuSettings.SettingsCloseButtonClick.OnButtonClicked -= OnButtonClickCloseMenuSettings;
        _menuSettings.SoundMuteToggle.MuteSoundButtonClickReader.OnButtonClicked -= OnButtonClickMuteSound;
        _menuSettings.SoundMuteToggle.UnmuteSoundButtonClickReader.OnButtonClicked -= OnButtonClickUnmuteSound;
        _menuSettings.MusicMuteToggle.MuteMusicButtonClickReader.OnButtonClicked -= OnButtonClickMuteMusic;
        _menuSettings.MusicMuteToggle.UnmuteMusicButtonClickReader.OnButtonClicked -= OnButtonClickUnmuteMusic;
        _restartButtonClickReader.OnButtonClicked -= OnButtonClickRestart;
        _levelCompleteShower.NextLevelButtonClickReader.OnButtonClicked -= OnButtonCkickNextLevel;
        _levelLossShower.RestartButtonClickReader.OnButtonClicked -= OnButtonClickRestart;
        _leaderboradShower.LeaderboradOpenButtonClick.OnButtonClicked -= OnButtonClickOpenLeaderboard;
        _leaderboradShower.LeaderboradCloseButtonClick.OnButtonClicked -= OnButtonClickCloseLeaderboard;
        _buySortingPassengersShower.OpenBuyingPassengerSortingButtonClickReader.OnButtonClicked -= OnButtonClickOpenBuyingPassengerSorting;
        _buySortingPassengersShower.AcceptBuyingPassengersSortingButtonClickReader.OnButtonClicked -= OnButtonClickAcceptBuyingPassengerSorting;
        _buySortingPassengersShower.DeclineBuyingPassengerSortingButtonClickReader.OnButtonClicked -= OnButtonClickDeclineBuyingPassengerSorting;
    }

    private void OnButtonClickMuteSound()
    {
        _ecsWorld.NewEntity().Get<MuteSoundEvent>();
    }

    private void OnButtonClickUnmuteSound()
    {
        _ecsWorld.NewEntity().Get<UnmuteSoundEvent>();
    }

    private void OnButtonClickMuteMusic()
    {
        _ecsWorld.NewEntity().Get<MuteMusicEvent>();
    }

    private void OnButtonClickUnmuteMusic()
    {
        _ecsWorld.NewEntity().Get<UnmuteMusicEvent>();
    }

    private void OnButtonClickOpenMenuSettings()
    {
        _ecsWorld.NewEntity().Get<OpenSettingsEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickCloseMenuSettings()
    {
        _ecsWorld.NewEntity().Get<CloseSettingsEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
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
    }

    private void OnButtonClickCloseLeaderboard()
    {
        _ecsWorld.NewEntity().Get<CloseLeaderboardEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
    }

    private void OnButtonClickOpenBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<OpenPassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }

    private void OnButtonClickAcceptBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>(); 
        _ecsWorld.NewEntity().Get<BuyPassengerSortingEvent>();
        _ecsWorld.NewEntity().Get<SortPassengerEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
    }

    private void OnButtonClickDeclineBuyingPassengerSorting()
    {
        _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
    }
}

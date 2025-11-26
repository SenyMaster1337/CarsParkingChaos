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

    public PlayerUIButtonReaderSystem(MenuSettingsShower menuSettings, RestartButtonClickReader restartButtonClickReader, LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower, LeaderboradShower leaderboradShower)
    {
        _menuSettings = menuSettings;
        _restartButtonClickReader = restartButtonClickReader;
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
        _leaderboradShower = leaderboradShower;
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
    }

    private void OnButtonClickCloseMenuSettings()
    {
        _ecsWorld.NewEntity().Get<CloseSettingsEvent>();
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
    }

    private void OnButtonClickCloseLeaderboard()
    {
        _ecsWorld.NewEntity().Get<CloseLeaderboardEvent>();
    }
}

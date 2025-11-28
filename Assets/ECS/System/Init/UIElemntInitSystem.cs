using Leopotam.Ecs;
using UnityEngine.SceneManagement;
using UnityEngine;
using YG;

public class UIElemntInitSystem : IEcsInitSystem
{
    private const string MasterVolume = "MasterVolume";
    private const string MusicVolume = "MusicVolume";

    private EcsWorld _ecsWorld;
    private MenuSettingsShower _menuSettingsShower;
    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;
    private LeaderboradShower _leaderboradShower;

    private StaticData _staticData;

    public UIElemntInitSystem(MenuSettingsShower menuSettingsShower, LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower, LeaderboradShower leaderboradShower)
    {
        _menuSettingsShower = menuSettingsShower;
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
        _leaderboradShower = leaderboradShower;
    }

    public void Init()
    {
        InitSettings();
        InitLevel();
        InitLeaderboard();
    }

    private void InitSettings()
    {
        var settingsNewEntity = _ecsWorld.NewEntity();

        ref var settingsComponent = ref settingsNewEntity.Get<UISettingsComponent>();

        settingsComponent.menuSettingsShower = _menuSettingsShower;
        settingsComponent.menuSettingsShower.WindowGroup.alpha = 0f;
        settingsComponent.menuSettingsShower.WindowGroup.interactable = false;
        settingsComponent.menuSettingsShower.WindowGroup.blocksRaycasts = false;

        if (YG2.saves.musicSoundValue == 0 || YG2.saves.musicSoundValue == _staticData.MaxMusicSoundValue)
            settingsComponent.menuSettingsShower.MusicMuteToggle.AudioMixer.SetFloat(MusicVolume, _staticData.MaxMusicSoundValue);
        else
            settingsComponent.menuSettingsShower.MusicMuteToggle.AudioMixer.SetFloat(MusicVolume, _staticData.MinMusicSoundValue);

        if (YG2.saves.masterSoundValue == 0 || YG2.saves.masterSoundValue == _staticData.MaxMusicSoundValue)
            settingsComponent.menuSettingsShower.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MaxMasterSoundValue);
        else
            settingsComponent.menuSettingsShower.SoundMuteToggle.AudioMixer.SetFloat(MasterVolume, _staticData.MinMasterSoundValue);
    }

    private void InitLevel()
    {
        var currentLevelEntity = _ecsWorld.NewEntity();

        ref var levelComponent = ref currentLevelEntity.Get<LevelComponent>();
        levelComponent.isLevelCompleted = false;
        levelComponent.currentLevel = SceneManager.GetActiveScene().buildIndex;

        ref var completeLevelComponent = ref currentLevelEntity.Get<UICompleteLevelComponent>();
        completeLevelComponent.windowGroup = _levelCompleteShower.WindowGroup;
        completeLevelComponent.windowGroup.alpha = 0f;
        completeLevelComponent.windowGroup.interactable = false;
        completeLevelComponent.windowGroup.blocksRaycasts = false;

        ref var levelLossComponent = ref currentLevelEntity.Get<UILevelLossComponent>();
        levelLossComponent.levelLossShower = _levelLossShower;
        levelLossComponent.levelLossShower.WindowGroup.alpha = 0f;
        levelLossComponent.levelLossShower.WindowGroup.interactable = false;
        levelLossComponent.levelLossShower.WindowGroup.blocksRaycasts = false;
    }

    private void InitLeaderboard()
    {
        var leaderboardEntity = _ecsWorld.NewEntity();

        ref var leaderboradComponent = ref leaderboardEntity.Get<UILeaderboardComponent>();

        leaderboradComponent.leaderboardShower = _leaderboradShower;
        leaderboradComponent.leaderboardShower.WindowGroup.alpha = 0f;
        leaderboradComponent.leaderboardShower.WindowGroup.interactable = false;
        leaderboradComponent.leaderboardShower.WindowGroup.blocksRaycasts = false;
    }
}

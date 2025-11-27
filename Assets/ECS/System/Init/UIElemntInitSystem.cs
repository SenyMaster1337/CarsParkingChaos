using Leopotam.Ecs;
using UnityEngine.SceneManagement;

public class UIElemntInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private MenuSettingsShower _menuSettingsShower;
    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;
    private LeaderboradShower _leaderboradShower;

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
        InitLevelLoss();
        InitLeaderboard();
    }

    private void InitSettings()
    {
        var settingsNewEntity = _ecsWorld.NewEntity();

        ref var settingComponent = ref settingsNewEntity.Get<SettingsComponent>();

        settingComponent.menuSettingsShower = _menuSettingsShower;
        settingComponent.menuSettingsShower.WindowGroup.alpha = 0f;
        settingComponent.menuSettingsShower.WindowGroup.interactable = false;
        settingComponent.menuSettingsShower.WindowGroup.blocksRaycasts = false;
    }

    private void InitLevel()
    {
        var currentLevelEntity = _ecsWorld.NewEntity();

        ref var levelComponent = ref currentLevelEntity.Get<LevelComponent>();
        levelComponent.isLevelCompleted = false;
        levelComponent.currentLevel = SceneManager.GetActiveScene().buildIndex;

        ref var completeLevelComponent = ref currentLevelEntity.Get<CompleteLevelComponent>();

        completeLevelComponent.windowGroup = _levelCompleteShower.WindowGroup;
        completeLevelComponent.windowGroup.alpha = 0f;
        completeLevelComponent.windowGroup.interactable = false;
        completeLevelComponent.windowGroup.blocksRaycasts = false;
    }

    private void InitLevelLoss()
    {
        var levelLossNewEntity = _ecsWorld.NewEntity();

        ref var levelLossComponent = ref levelLossNewEntity.Get<LevelLossComponent>();

        levelLossComponent.levelLossShower = _levelLossShower;
        levelLossComponent.levelLossShower.WindowGroup.alpha = 0f;
        levelLossComponent.levelLossShower.WindowGroup.interactable = false;
        levelLossComponent.levelLossShower.WindowGroup.blocksRaycasts = false;
    }

    private void InitLeaderboard()
    {
        var leaderboardEntity = _ecsWorld.NewEntity();

        ref var leaderboradComponent = ref leaderboardEntity.Get<LeaderboardComponent>();

        leaderboradComponent.leaderboardShower = _leaderboradShower;
        leaderboradComponent.leaderboardShower.WindowGroup.alpha = 0f;
        leaderboradComponent.leaderboardShower.WindowGroup.interactable = false;
        leaderboradComponent.leaderboardShower.WindowGroup.blocksRaycasts = false;
    }
}

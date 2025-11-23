using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElemntInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private MenuSettingsShower _menuSettingsShower;
    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;

    public UIElemntInitSystem(MenuSettingsShower menuSettingsShower, LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower)
    {
        _menuSettingsShower = menuSettingsShower;
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
    }

    public void Init()
    {
        InitSettings();
        InitLevel();
        InitLevelLoss();
    }

    private void InitSettings()
    {
        var settingsNewEntity = _ecsWorld.NewEntity();

        ref var completeLevelComponent = ref settingsNewEntity.Get<SettingsComponent>();

        completeLevelComponent.menuSettingsShower = _menuSettingsShower;
        completeLevelComponent.menuSettingsShower.WindowGroup.alpha = 0f;
        completeLevelComponent.menuSettingsShower.WindowGroup.interactable = false;
        completeLevelComponent.menuSettingsShower.WindowGroup.blocksRaycasts = false;
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
}

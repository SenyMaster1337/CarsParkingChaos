using Leopotam.Ecs;

public class LevelShowInitSystem : IEcsInitSystem
{
    private EcsFilter<LevelComponent> _filter;

    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;
    private LevelCurrentShower _levelCurrentShower;
    private StaticData _staticData;

    public LevelShowInitSystem(LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower, LevelCurrentShower levelCurrentShower)
    {
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
        _levelCurrentShower = levelCurrentShower;
    }

    public void Init()
    {
        InitShowLevel();
    }

    private void InitShowLevel()
    {
        foreach (var entity in _filter)
        {
            ref var levelComponent = ref _filter.Get1(entity);

            ref var completeLevelComponent = ref levelComponent.entity.Get<UICompleteLevelComponent>();
            completeLevelComponent.levelCompleteShower = _levelCompleteShower;
            completeLevelComponent.levelCompleteShower.WindowGroup.alpha = 0f;
            completeLevelComponent.levelCompleteShower.WindowGroup.interactable = false;
            completeLevelComponent.levelCompleteShower.WindowGroup.blocksRaycasts = false;
            completeLevelComponent.levelCompleteShower.CoinsNumberToWinText.Value.SetText($"{_staticData.NumberCointAddedPerWin}");

            completeLevelComponent.levelCompleteShower.BlackBackground.WindowGroup.alpha = 0f;
            completeLevelComponent.levelCompleteShower.BlackBackground.WindowGroup.interactable = false;
            completeLevelComponent.levelCompleteShower.BlackBackground.WindowGroup.blocksRaycasts = false;

            ref var levelLossComponent = ref levelComponent.entity.Get<UILevelLossComponent>();
            levelLossComponent.levelLossShower = _levelLossShower;
            levelLossComponent.levelLossShower.WindowGroup.alpha = 0f;
            levelLossComponent.levelLossShower.WindowGroup.interactable = false;
            levelLossComponent.levelLossShower.WindowGroup.blocksRaycasts = false;

            ref var levelUIComponent = ref levelComponent.entity.Get<UILevelComponent>();
            levelUIComponent.levelCurrentShower = _levelCurrentShower;
            levelUIComponent.levelCurrentShower.CurrentLevelNumberText.Value.SetText($"{levelComponent.currentLevel}");
        }
    }
}

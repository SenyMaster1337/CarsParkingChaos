using Leopotam.Ecs;

public class LevelShowInitSystem : IEcsInitSystem
{
    private EcsFilter<LevelComponent> _filter;

    private LevelCompleteShower _levelCompleteShower;
    private LevelLossShower _levelLossShower;

    public LevelShowInitSystem(LevelCompleteShower levelCompleteShower, LevelLossShower levelLossShower)
    {
        _levelCompleteShower = levelCompleteShower;
        _levelLossShower = levelLossShower;
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
            completeLevelComponent.windowGroup = _levelCompleteShower.WindowGroup;
            completeLevelComponent.windowGroup.alpha = 0f;
            completeLevelComponent.windowGroup.interactable = false;
            completeLevelComponent.windowGroup.blocksRaycasts = false;

            ref var levelLossComponent = ref levelComponent.entity.Get<UILevelLossComponent>();
            levelLossComponent.levelLossShower = _levelLossShower;
            levelLossComponent.levelLossShower.WindowGroup.alpha = 0f;
            levelLossComponent.levelLossShower.WindowGroup.interactable = false;
            levelLossComponent.levelLossShower.WindowGroup.blocksRaycasts = false;
        }
    }
}

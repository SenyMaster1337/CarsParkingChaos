using Leopotam.Ecs;
using System.Collections.Generic;

public class LevelProgressSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<LevelComponent> _filter;
    private EcsFilter<UICompleteLevelComponent> _UIfilter;

    private StaticData _staticData;
    private List<Passenger> _passengers;
    private bool _isTimerToCompltiteEnable;

    public LevelProgressSystem(List<Passenger> passengers)
    {
        _passengers = passengers;
        _isTimerToCompltiteEnable = false;
    }

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var levelComponent = ref _filter.Get1(entity);
            var levelEntity = _filter.GetEntity(entity);

            if (_passengers.Count == 0 && _isTimerToCompltiteEnable == false)
            {
                _isTimerToCompltiteEnable = true;
                StartTimer(entity, _staticData.TimeToLevelShowLevelComplete);
            }

            if (levelEntity.Has<LevelCompleteEvent>())
            {
                CompleteLevel(entity, levelComponent);
                levelComponent.isLevelCompleted = true;
                levelEntity.Del<LevelCompleteEvent>();
            }
        }
    }

    private void StartTimer(int entity, float duration)
    {
        _filter.GetEntity(entity).Get<TimerComponent>() = new TimerComponent
        {
            TimeLeft = duration,
            IsActive = true
        };
    }

    private void CompleteLevel(int entity, LevelComponent levelComponent)
    {
        levelComponent.isLevelCompleted = true;
        StartYGEvents(levelComponent.currentLevel);
        ShowWinWindow(entity);
    }

    private void ShowWinWindow(int entity)
    {
        ref var completeLevelComponent = ref _UIfilter.Get1(entity);
        completeLevelComponent.windowGroup.alpha = 1.0f;
        completeLevelComponent.windowGroup.interactable = true;
        completeLevelComponent.windowGroup.blocksRaycasts = true;
    }

    private void StartYGEvents(int nextSceneIndex)
    {
        _ecsWorld.NewEntity().Get<YGSaveProgressEvent>() = new YGSaveProgressEvent
        {
            levelIndex = nextSceneIndex,
            coinsWinner = 25
        };
    }
}

using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;


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
                CompleteLevel(entity, ref levelComponent);
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

    private void CompleteLevel(int entity, ref LevelComponent levelComponent)
    {
        levelComponent.isLevelCompleted = true;
        levelComponent.currentLevel++;
        Debug.Log(levelComponent.currentLevel);

        _ecsWorld.NewEntity().Get<AddPointsWinningLeaderboardEvent>();
        _ecsWorld.NewEntity().Get<AddCoinsWinningEvent>();
        _ecsWorld.NewEntity().Get<YGSaveProgressEvent>();

        ShowWinWindow(entity);
    }

    private void ShowWinWindow(int entity)
    {
        ref var completeLevelComponent = ref _UIfilter.Get1(entity);
        completeLevelComponent.levelCompleteShower.WindowGroup.alpha = 1.0f;
        completeLevelComponent.levelCompleteShower.WindowGroup.interactable = true;
        completeLevelComponent.levelCompleteShower.WindowGroup.blocksRaycasts = true;
        _ecsWorld.NewEntity().Get<DisableButtonsEvent>();
    }
}

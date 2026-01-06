using System.Collections.Generic;
using Leopotam.Ecs;

public class LevelProgressSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<LevelComponent> _filter;
    private EcsFilter<UICompleteLevelComponent> _UIfilter;
    private EcsFilter<LevelCompleteEvent> _levleCompleteFilter;

    private StaticData _staticData;
    private List<Passenger> _passengers;
    private bool _isTimerToCompltiteEnable;

    public LevelProgressSystem()
    {
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

            foreach (var completeEntity in _levleCompleteFilter)
            {
                if (levelEntity.Has<LevelCompleteEvent>())
                {
                    CompleteLevel(entity, ref levelComponent);
                    levelComponent.IsLevelCompleted = true;
                    levelEntity.Del<LevelCompleteEvent>();
                }
            }
        }
    }

    private void StartTimer(int entity, float duration)
    {
        _filter.GetEntity(entity).Get<TimerComponent>() = new TimerComponent
        {
            TimeLeft = duration,
            IsActive = true,
        };
    }

    private void CompleteLevel(int entity, ref LevelComponent levelComponent)
    {
        levelComponent.IsLevelCompleted = true;
        levelComponent.CurrentLevel++;

        _ecsWorld.NewEntity().Get<YGSavePlayerLevelEvent>();
        _ecsWorld.NewEntity().Get<AddPointsWinningLeaderboardEvent>();
        _ecsWorld.NewEntity().Get<AddCoinsWinningEvent>();
        _ecsWorld.NewEntity().Get<YGSavePlayerCoinsCountEvent>();
        _ecsWorld.NewEntity().Get<YGClearDataRewardParkingSlots>();
        _ecsWorld.NewEntity().Get<YGSaveProgressEvent>();

        ShowWinWindow(entity);
    }

    private void ShowWinWindow(int entity)
    {
        ref var completeLevelComponent = ref _UIfilter.Get1(entity);
        completeLevelComponent.LevelCompleteShower.WindowGroup.alpha = 1.0f;
        completeLevelComponent.LevelCompleteShower.WindowGroup.interactable = true;
        completeLevelComponent.LevelCompleteShower.WindowGroup.blocksRaycasts = true;

        completeLevelComponent.LevelCompleteShower.BlackBackground.WindowGroup.alpha = 1.0f;

        _ecsWorld.NewEntity().Get<DisableButtonsEvent>();
    }
}
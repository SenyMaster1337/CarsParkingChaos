using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressSystem : IEcsRunSystem
{
    private EcsFilter<LevelComponent, CompleteLevelComponent> _filter;

    private List<Passenger> _passengers;

    public LevelProgressSystem(List<Passenger> passengers)
    {
        _passengers = passengers;
    }

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var levelComponent = ref _filter.Get1(entity);

            if (_passengers.Count == 0 && levelComponent.isLevelCompleted == false)
            {
                levelComponent.isLevelCompleted = true;
                ShowWinWindow(entity);
            }
        }
    }

    private void ShowWinWindow(int entity)
    {
        ref var completeLevelComponent = ref _filter.Get2(entity);
        completeLevelComponent.windowGroup.alpha = 1.0f;
        completeLevelComponent.windowGroup.interactable = true;
        completeLevelComponent.windowGroup.blocksRaycasts = true;
    }
}

using Leopotam.Ecs;
using UnityEngine;

public class LevelLossSystem : IEcsRunSystem
{
    private EcsFilter<LevelLossComponent> _filter;
    private EcsFilter<ShowLossWindowEvent> _showLoss;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            foreach (var showLossEntity in _showLoss)
            {
                var verifyEntityEvent = _showLoss.GetEntity(showLossEntity);
                ShowLossWindow(entity);
                verifyEntityEvent.Del<ShowLossWindowEvent>();
            }
        }
    }

    private void ShowLossWindow(int entity)
    {
        ref var lossComponent = ref _filter.Get1(entity);
        lossComponent.levelLossShower.WindowGroup.alpha = 1;
        lossComponent.levelLossShower.WindowGroup.interactable = true;
        lossComponent.levelLossShower.WindowGroup.blocksRaycasts = true;
    }
}

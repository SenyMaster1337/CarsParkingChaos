using Leopotam.Ecs;

public class LevelLossShowerSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<UILevelLossComponent> _filter;
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
        lossComponent.LevelLossShower.WindowGroup.alpha = 1;
        lossComponent.LevelLossShower.WindowGroup.interactable = true;
        lossComponent.LevelLossShower.WindowGroup.blocksRaycasts = true;

        lossComponent.LevelLossShower.BlackBackground.WindowGroup.alpha = 1f;

        _ecsWorld.NewEntity().Get<DisableButtonsEvent>();
        _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
    }
}

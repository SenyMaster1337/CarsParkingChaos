using Leopotam.Ecs;

public class EndGameShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private EndGameShower _endGameShower;

    public EndGameShowerInitSystem(EndGameShower endGameShower)
    {
        _endGameShower = endGameShower;
    }

    public void Init()
    {
        var endGameShowerNewEntity = _ecsWorld.NewEntity();

        ref var endGameShowerComponent = ref endGameShowerNewEntity.Get<EndGameShowerComponent>();
        endGameShowerComponent.EndGameShower = _endGameShower;

        endGameShowerComponent.EndGameShower.WindowGroup.alpha = 0f;
        endGameShowerComponent.EndGameShower.WindowGroup.interactable = false;
        endGameShowerComponent.EndGameShower.WindowGroup.blocksRaycasts = false;
    }
}

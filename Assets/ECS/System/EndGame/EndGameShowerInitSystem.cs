using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        endGameShowerComponent.endGameShower = _endGameShower;

        endGameShowerComponent.endGameShower.WindowGroup.alpha = 0f;
        endGameShowerComponent.endGameShower.WindowGroup.interactable = false;
        endGameShowerComponent.endGameShower.WindowGroup.blocksRaycasts = false;
    }
}

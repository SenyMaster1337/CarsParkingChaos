using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameShowerSystem : IEcsRunSystem
{
    private EcsFilter<EndGameShowerComponent> _endGameShowerFilter;
    private EcsFilter<ShowEndGameWindowEvent> _showWindowEvent;

    public void Run()
    {
        foreach(var endGameEntity in _endGameShowerFilter)
        {
            ref var endGameShowerComponent = ref _endGameShowerFilter.Get1(endGameEntity);

            foreach (var showWindowEntity in _showWindowEvent)
            {
                _showWindowEvent.GetEntity(showWindowEntity).Del<ShowEndGameWindowEvent>();
            }
        }
    }
}

using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerShuffleShowerSystem : IEcsRunSystem
{
    private EcsFilter<PassengerShuffleShowerComponent> _sortingFilter;
    private EcsFilter<OpenPassengerShuffleInfoShowerEvent> _openFilter;
    private EcsFilter<ClosePassengerShuffleInfoShowerEvent> _closeFilter;

    public void Run()
    {
        foreach (var sortingEntity in _sortingFilter)
        {
            ref var sortingShowerComponent = ref _sortingFilter.Get1(sortingEntity);

            foreach (var openEntity in _openFilter)
            {
                var openEvent = _openFilter.GetEntity(openEntity);
                OpenSortingInfo(sortingShowerComponent);
                openEvent.Del<OpenPassengerSortingInfoShowerEvent>();
            }

            foreach (var closeEntity in _closeFilter)
            {
                var closeEvent = _closeFilter.GetEntity(closeEntity);
                CloseSortingInfo(sortingShowerComponent);
                closeEvent.Del<ClosePassengerSortingInfoShowerEvent>();
            }
        }
    }

    private void OpenSortingInfo(PassengerShuffleShowerComponent shuffleShowerComponent)
    {
        shuffleShowerComponent.buyPassengerShuffleShower.WindowGroup.alpha = 1.0f;
        shuffleShowerComponent.buyPassengerShuffleShower.WindowGroup.interactable = true;
        shuffleShowerComponent.buyPassengerShuffleShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseSortingInfo(PassengerShuffleShowerComponent shuffleShowerComponent)
    {
        shuffleShowerComponent.buyPassengerShuffleShower.WindowGroup.alpha = 0f;
        shuffleShowerComponent.buyPassengerShuffleShower.WindowGroup.interactable = false;
        shuffleShowerComponent.buyPassengerShuffleShower.WindowGroup.blocksRaycasts = false;
    }
}

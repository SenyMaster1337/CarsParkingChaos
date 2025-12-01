using Leopotam.Ecs;
using UnityEngine;

public class PassengerSortingShowerSystem : IEcsRunSystem
{
    private EcsFilter<PassengerSortingShowerComponent> _sortingFilter;
    private EcsFilter<OpenPassengerSortingInfoShowerEvent> _openInfo;
    private EcsFilter<ClosePassengerSortingInfoShowerEvent> _closeInfo;

    public void Run()
    {
        foreach (var sortingEntity in _sortingFilter)
        {
            ref var sortingShowerComponent = ref _sortingFilter.Get1(sortingEntity);

            foreach (var openEntity in _openInfo)
            {
                var openEvent = _openInfo.GetEntity(openEntity);
                OpenSortingInfo(sortingShowerComponent);
                openEvent.Del<OpenPassengerSortingInfoShowerEvent>();
            }

            foreach (var closeEntity in _closeInfo)
            {
                var closeEvent = _closeInfo.GetEntity(closeEntity);
                CloseSortingInfo(sortingShowerComponent);
                closeEvent.Del<ClosePassengerSortingInfoShowerEvent>();
            }
        }
    }

    private void OpenSortingInfo(PassengerSortingShowerComponent sortingShowerComponent)
    {
        sortingShowerComponent.buySortingPassengersShower.WindowGroup.alpha = 1.0f;
        sortingShowerComponent.buySortingPassengersShower.WindowGroup.interactable = true;
        sortingShowerComponent.buySortingPassengersShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseSortingInfo(PassengerSortingShowerComponent sortingShowerComponent)
    {
        sortingShowerComponent.buySortingPassengersShower.WindowGroup.alpha = 0f;
        sortingShowerComponent.buySortingPassengersShower.WindowGroup.interactable = false;
        sortingShowerComponent.buySortingPassengersShower.WindowGroup.blocksRaycasts = false;
    }
}

using Leopotam.Ecs;
using UnityEngine;

public class PassengerSortingShowerSystem : IEcsRunSystem
{
    private EcsFilter<PassengerSortingShowerComponent> _sortingFilter;
    private EcsFilter<OpenPassengerSortingInfoShowerEvent> _openFilter;
    private EcsFilter<ClosePassengerSortingInfoShowerEvent> _closeFilter;

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

    private void OpenSortingInfo(PassengerSortingShowerComponent sortingShowerComponent)
    {
        sortingShowerComponent.buyPassengerSortingShower.WindowGroup.alpha = 1.0f;
        sortingShowerComponent.buyPassengerSortingShower.WindowGroup.interactable = true;
        sortingShowerComponent.buyPassengerSortingShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseSortingInfo(PassengerSortingShowerComponent sortingShowerComponent)
    {
        sortingShowerComponent.buyPassengerSortingShower.WindowGroup.alpha = 0f;
        sortingShowerComponent.buyPassengerSortingShower.WindowGroup.interactable = false;
        sortingShowerComponent.buyPassengerSortingShower.WindowGroup.blocksRaycasts = false;
    }
}

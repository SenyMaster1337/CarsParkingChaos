using Leopotam.Ecs;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
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
                    OpenSortingInfo(sortingShowerComponent);
                    _openFilter.GetEntity(openEntity).Del<OpenPassengerSortingInfoShowerEvent>();
                }

                foreach (var closeEntity in _closeFilter)
                {
                    CloseSortingInfo(sortingShowerComponent);
                    _closeFilter.GetEntity(closeEntity).Del<ClosePassengerSortingInfoShowerEvent>();
                }
            }
        }

        private void OpenSortingInfo(PassengerSortingShowerComponent sortingShowerComponent)
        {
            sortingShowerComponent.BuyPassengerSortingShower.WindowGroup.alpha = 1.0f;
            sortingShowerComponent.BuyPassengerSortingShower.WindowGroup.interactable = true;
            sortingShowerComponent.BuyPassengerSortingShower.WindowGroup.blocksRaycasts = true;
        }

        private void CloseSortingInfo(PassengerSortingShowerComponent sortingShowerComponent)
        {
            sortingShowerComponent.BuyPassengerSortingShower.WindowGroup.alpha = 0f;
            sortingShowerComponent.BuyPassengerSortingShower.WindowGroup.interactable = false;
            sortingShowerComponent.BuyPassengerSortingShower.WindowGroup.blocksRaycasts = false;
        }
    }
}

using Leopotam.Ecs;

namespace CarParkingChaos.ECS.Systems
{
    public class CarShuffleShowerSystem : IEcsRunSystem
    {
        private EcsFilter<PassengerShuffleShowerComponent> _sortingFilter;
        private EcsFilter<OpenPassengerShuffleInfoShowerEvent> _openFilter;
        private EcsFilter<CloseShuffleInfoShowerEvent> _closeFilter;

        public void Run()
        {
            foreach (var sortingEntity in _sortingFilter)
            {
                ref var sortingShowerComponent = ref _sortingFilter.Get1(sortingEntity);

                foreach (var openEntity in _openFilter)
                {
                    var openEvent = _openFilter.GetEntity(openEntity);
                    OpenSortingInfo(sortingShowerComponent);
                    openEvent.Del<OpenPassengerShuffleInfoShowerEvent>();
                }

                foreach (var closeEntity in _closeFilter)
                {
                    var closeEvent = _closeFilter.GetEntity(closeEntity);
                    CloseSortingInfo(sortingShowerComponent);
                    closeEvent.Del<CloseShuffleInfoShowerEvent>();
                }
            }
        }

        private void OpenSortingInfo(PassengerShuffleShowerComponent shuffleShowerComponent)
        {
            shuffleShowerComponent.BuyPassengerShuffleShower.WindowGroup.alpha = 1.0f;
            shuffleShowerComponent.BuyPassengerShuffleShower.WindowGroup.interactable = true;
            shuffleShowerComponent.BuyPassengerShuffleShower.WindowGroup.blocksRaycasts = true;
        }

        private void CloseSortingInfo(PassengerShuffleShowerComponent shuffleShowerComponent)
        {
            shuffleShowerComponent.BuyPassengerShuffleShower.WindowGroup.alpha = 0f;
            shuffleShowerComponent.BuyPassengerShuffleShower.WindowGroup.interactable = false;
            shuffleShowerComponent.BuyPassengerShuffleShower.WindowGroup.blocksRaycasts = false;
        }
    }
}

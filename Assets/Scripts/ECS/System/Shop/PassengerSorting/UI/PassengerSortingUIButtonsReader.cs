using Leopotam.Ecs;
using CarParkingChaos.UI.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class PassengerSortingUIButtonsReader : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _ecsWorld;

        private PassengerSortingShower _buyPassengerSortingShower;

        public PassengerSortingUIButtonsReader(PassengerSortingShower buyPassengerSortingShower)
        {
            _buyPassengerSortingShower = buyPassengerSortingShower;
        }

        public void Init()
        {
            _buyPassengerSortingShower.OpenBuyingPassengerSortingButton.OnButtonClicked += OnButtonClickOpen;
            _buyPassengerSortingShower.AcceptBuyingPassengersSortingButton.OnButtonClicked += OnButtonClickAccept;
            _buyPassengerSortingShower.DeclineBuyingPassengerSortingButton.OnButtonClicked += OnButtonClickDecline;
            _buyPassengerSortingShower.RewardPassengerSortingButton.OnButtonClicked += OnButtonClickReward;
        }

        public void Destroy()
        {
            _buyPassengerSortingShower.OpenBuyingPassengerSortingButton.OnButtonClicked -= OnButtonClickOpen;
            _buyPassengerSortingShower.AcceptBuyingPassengersSortingButton.OnButtonClicked -= OnButtonClickAccept;
            _buyPassengerSortingShower.DeclineBuyingPassengerSortingButton.OnButtonClicked -= OnButtonClickDecline;
            _buyPassengerSortingShower.RewardPassengerSortingButton.OnButtonClicked -= OnButtonClickReward;
        }

        private void OnButtonClickOpen()
        {
            _ecsWorld.NewEntity().Get<OpenPassengerSortingInfoShowerEvent>();
            _ecsWorld.NewEntity().Get<CloseShopEvent>();
        }

        private void OnButtonClickAccept()
        {
            var tryBuyEventNewEntity = _ecsWorld.NewEntity();
            tryBuyEventNewEntity.Get<TryBuyEvent>();
            tryBuyEventNewEntity.Get<PassengerSortingComponent>();

            _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
        }

        private void OnButtonClickDecline()
        {
            _ecsWorld.NewEntity().Get<ClosePassengerSortingInfoShowerEvent>();
            _ecsWorld.NewEntity().Get<OpenShopEvent>();
        }

        private void OnButtonClickReward()
        {
            _ecsWorld.NewEntity().Get<ShowAdvToPassengerSortEvent>();

            _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
        }
    }
}

using Leopotam.Ecs;
using CarParkingChaos.UI.Markers;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class UnlockParkingSlotShowerButtonReaderSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _ecsWorld;
        private ADVUnlockParkingSlotShower _advUnlockParkingSlotShower;

        public UnlockParkingSlotShowerButtonReaderSystem(ADVUnlockParkingSlotShower advUnlockParkingSlotShower)
        {
            _advUnlockParkingSlotShower = advUnlockParkingSlotShower;
        }

        public void Init()
        {
            _advUnlockParkingSlotShower.ShowAdvUnclockParkingSlotButton.OnButtonClicked += OnButtonClickShowAdvToUnlock;
            _advUnlockParkingSlotShower.CloseAdvUnlockParkingSlotrButton.OnButtonClicked += OnButtonClickCloseAdvUnlock;
        }

        public void Destroy()
        {
            _advUnlockParkingSlotShower.ShowAdvUnclockParkingSlotButton.OnButtonClicked -= OnButtonClickShowAdvToUnlock;
            _advUnlockParkingSlotShower.CloseAdvUnlockParkingSlotrButton.OnButtonClicked -= OnButtonClickCloseAdvUnlock;
        }

        private void OnButtonClickShowAdvToUnlock()
        {
            _ecsWorld.NewEntity().Get<ShowADVToUnlockParkingSlotEvent>();
            OnButtonClickCloseAdvUnlock();
        }

        private void OnButtonClickCloseAdvUnlock()
        {
            _ecsWorld.NewEntity().Get<CloseADVUnlockParkingSlotEvent>();
            _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
            _ecsWorld.NewEntity().Get<EnableRaycastReaderEvent>();
        }
    }
}

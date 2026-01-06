using System.Collections.Generic;
using Leopotam.Ecs;
using YG;

public class ParkingReservationInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private List<ParkingSlot> _allParkingSlots;
    private List<ParkingSlot> _unlockParkingSlots;
    private SceneData _sceneData;

    public ParkingReservationInitSystem(List<ParkingSlot> parkingSlots)
    {
        _allParkingSlots = parkingSlots;
        _unlockParkingSlots = new List<ParkingSlot>();
    }

    public void Init()
    {
        InitParkingSlots();
        InitParkingReservationComponent();
    }

    private void InitParkingSlots()
    {
        int parkingSlotsCount = _sceneData.UnlockParkingSlotsCount + YG2.saves.AdditionalRewardParkingSlotsCount;

        for (int i = 0; i < parkingSlotsCount; i++)
        {
            _allParkingSlots[i].GetComponentInChildren<OpenADVParkingSlotUnlock>().gameObject.SetActive(false);

            var parkingSlotNewEntity = _ecsWorld.NewEntity();

            ref var parkingComponent = ref parkingSlotNewEntity.Get<ParkingComponent>();
            parkingComponent.Car = null;
            parkingComponent.IsReserved = false;

            _allParkingSlots[i].Entity = parkingSlotNewEntity;

            _unlockParkingSlots.Add(_allParkingSlots[i]);
        }
    }

    private void InitParkingReservationComponent()
    {
        var parkingReservationEntity = _ecsWorld.NewEntity();

        ref var parkingReservationComponent = ref parkingReservationEntity.Get<ParkingReservationComponent>();
        parkingReservationComponent.ParkingSlots = _unlockParkingSlots;
    }
}

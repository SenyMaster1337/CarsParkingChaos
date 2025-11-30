using Leopotam.Ecs;
using System.Collections.Generic;

public class ParkingInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private List<ParkingSlot> _parkingSlots;

    public ParkingInitSystem(List<ParkingSlot> parkingSlots)
    {
        _parkingSlots = parkingSlots;
    }

    public void Init()
    {
        InitParkingSlots();
        InitParkingReservationComponent();
    }

    private void InitParkingSlots()
    {
        for (int i = 0; i < _parkingSlots.Count; i++)
        {
            var parkingSlotNewEntity = _ecsWorld.NewEntity();

            ref var parkingComponent = ref parkingSlotNewEntity.Get<ParkingComponent>();
            parkingComponent.car = null;
            parkingComponent.isReserved = false;

            _parkingSlots[i].Entity = parkingSlotNewEntity;
        }
    }

    private void InitParkingReservationComponent()
    {
        var parkingReservationEntity = _ecsWorld.NewEntity();

        ref var parkingReservationComponent = ref parkingReservationEntity.Get<ParkingReservationComponent>();
        parkingReservationComponent.parkingSlots = _parkingSlots;
    }
}

using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class ParkingReservationSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsFilter<ParkingCancelReservationEvent> _cancelParkingReserve;

    private RaycastReaderSystem _raycastReaderSystem;
    private List<ParkingSlot> _parkingSlots;

    public ParkingReservationSystem(List<ParkingSlot> parkingSlots, RaycastReaderSystem raycastReaderSystem)
    {
        _raycastReaderSystem = raycastReaderSystem;
        _parkingSlots = parkingSlots;
    }

    public void Init()
    {
        _raycastReaderSystem.EntityDetected += BrokeedParkingSlot;
    }

    public void Destroy()
    {
        _raycastReaderSystem.EntityDetected -= BrokeedParkingSlot;
    }

    private void BrokeedParkingSlot(EcsEntity hitEntity)
    {
        ref var carComponent = ref hitEntity.Get<CarComponent>();

        if (carComponent.canClickable == false)
            return;

        for (int i = 0; i < _parkingSlots.Count; i++)
        {
            ref var parkingComponent = ref _parkingSlots[i].Entity.Get<ParkingComponent>();

            if (parkingComponent.isReserved == false)
            {
                carComponent.canClickable = false;
                carComponent.parkingReservedSlot = _parkingSlots[i];

                ref var movable = ref hitEntity.Get<CarMovableComponent>();
                movable.isMoving = true;

                parkingComponent.isReserved = true;
                return;
            }
        }

    }

    public void Run()
    {
        foreach (var cancelEntity in _cancelParkingReserve)
        {
            Debug.Log(_cancelParkingReserve.GetEntitiesCount());

            ref var cancelReservationEvent = ref _cancelParkingReserve.Get1(cancelEntity);

            CancelParkingReserved(cancelReservationEvent);

            var cancelEntityEvent = _cancelParkingReserve.GetEntity(cancelEntity);
            cancelEntityEvent.Del<ParkingCancelReservationEvent>();
        }
    }

    private void CancelParkingReserved(ParkingCancelReservationEvent cancelReservationEvent)
    {
        if (_parkingSlots.Contains(cancelReservationEvent.parkingSlot))
        {
            ref var parkingComponent = ref cancelReservationEvent.parkingSlot.Entity.Get<ParkingComponent>();
            parkingComponent.isReserved = false;
        }
    }
}

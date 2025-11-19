using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class ParkingReservationSystem : IEcsInitSystem, IEcsDestroySystem
{
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
        ref var component = ref hitEntity.Get<CarComponent>();

        if (component.canClickable == false)
            return;

        for (int i = 0; i < _parkingSlots.Count; i++)
        {
            ref var parkingComponent = ref _parkingSlots[i].Entity.Get<ParkingComponent>();

            if (parkingComponent.isReserved == false)
            {
                ref var carComponent = ref hitEntity.Get<CarComponent>();
                carComponent.parkingReservedSlot = _parkingSlots[i];

                ref var movable = ref hitEntity.Get<CarMovableComponent>();
                movable.isMoving = true;

                parkingComponent.isReserved = true;
                return;
            }
        }

    }
}

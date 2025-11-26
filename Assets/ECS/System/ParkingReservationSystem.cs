using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class ParkingReservationSystem : IEcsInitSystem, IEcsRunSystem
{
    private readonly float _timeLeftInTimerToVerifyCarsInParking = 4f;

    private EcsWorld _ecsWorld;
    private EcsFilter<ParkingReservationComponent> _filter;
    private EcsFilter<ReservedParkingSlotEvent> _reservedSlot;
    private EcsFilter<ParkingCancelReservationEvent> _cancelParkingReserve;
    private EcsFilter<VerifyCarsInParkingDataEvent> _verifyCarsInParkingData;

    private List<ParkingSlot> _reservedParkingSlots;
    private bool _isParkingFull = false;

    public ParkingReservationSystem()
    {
        _reservedParkingSlots = new List<ParkingSlot>();
    }

    public void Init()
    {
        InitReservedParkingSlots();
    }

    public void Run()
    { 
        foreach (var entity in _filter)
        {
            ref var parkingReservationComponent = ref _filter.Get1(entity);

            ToggleSwitchRayReaderActiveEvent(parkingReservationComponent.parkingSlots);
            TrySaveCarInParkingData(entity, parkingReservationComponent.parkingSlots);

            foreach (var reservedEntity in _reservedSlot)
            {
                ref var reserveEvent = ref _reservedSlot.Get1(entity);

                var reservedEntityEvent = _reservedSlot.GetEntity(reservedEntity);
                ReserveParkingSlot(reserveEvent.carEntity, parkingReservationComponent.parkingSlots);
                reservedEntityEvent.Del<ReservedParkingSlotEvent>();
            }

            foreach (var cancelEntity in _cancelParkingReserve)
            {
                ref var cancelReservationEvent = ref _cancelParkingReserve.Get1(cancelEntity);

                var cancelEntityEvent = _cancelParkingReserve.GetEntity(cancelEntity);
                CancelParkingReserved(cancelReservationEvent, parkingReservationComponent.parkingSlots);
                cancelEntityEvent.Del<ParkingCancelReservationEvent>();
            }

            foreach (var verifyEntity in _verifyCarsInParkingData)
            {
                var verifyEntityEvent = _verifyCarsInParkingData.GetEntity(verifyEntity);
                VerifyCarsInParkingData(parkingReservationComponent.parkingSlots);
                verifyEntityEvent.Del<VerifyCarsInParkingDataEvent>();
            }
        }
    }

    private void InitReservedParkingSlots()
    {
        for (int i = 0; i < _reservedParkingSlots.Count; i++)
        {
            var parkingNewEntity = _ecsWorld.NewEntity();

            ref var parkingComponent = ref parkingNewEntity.Get<ParkingComponent>();
            parkingComponent.car = null;

            _reservedParkingSlots[i].Entity = parkingNewEntity;
        }
    }

    private void VerifyCarsInParkingData(List<ParkingSlot> parkingSlots)
    {
        int count = 0;

        if (_reservedParkingSlots == null || _reservedParkingSlots.Count < parkingSlots.Count)
        {
            _isParkingFull = false;
            return;
        }

        for (int i = 0; i < parkingSlots.Count; i++)
        {
            ref var parkingComponent = ref parkingSlots[i].Entity.Get<ParkingComponent>();
            ref var ReservedparkingComponent = ref _reservedParkingSlots[i].Entity.Get<ParkingComponent>();

            if (parkingComponent.car == ReservedparkingComponent.car)
            {
                count++;
            }
            else
            {
                _isParkingFull = false;
                return;
            }

            if (count == parkingSlots.Count)
                _ecsWorld.NewEntity().Get<ShowLossWindowEvent>();
        }
    }

    private void TrySaveCarInParkingData(int entity, List<ParkingSlot> parkingSlots)
    {
        if (_reservedParkingSlots.Count >= parkingSlots.Count && _isParkingFull == false)
        {
            for (int i = 0; i < parkingSlots.Count; i++)
            {
                ref var parkingComponent = ref parkingSlots[i].Entity.Get<ParkingComponent>();
                ref var ReservedparkingComponent = ref _reservedParkingSlots[i].Entity.Get<ParkingComponent>();

                ReservedparkingComponent.car = parkingComponent.car;
            }

            _filter.GetEntity(entity).Get<TimerComponent>() = new TimerComponent
            {
                TimeLeft = _timeLeftInTimerToVerifyCarsInParking,
                IsActive = true
            };

            _isParkingFull = true;
        }
    }

    private void ReserveParkingSlot(EcsEntity carEcsEntity, List<ParkingSlot> parkingSlots)
    {
        ref var carComponent = ref carEcsEntity.Get<CarComponent>();

        for (int i = 0; i < parkingSlots.Count; i++)
        {
            ref var parkingComponent = ref parkingSlots[i].Entity.Get<ParkingComponent>();

            if (parkingComponent.isReserved == false)
            {
                carComponent.canClickable = false;
                carComponent.parkingReservedSlot = parkingSlots[i];

                parkingComponent.car = carComponent.car;
                parkingComponent.isReserved = true;
                _reservedParkingSlots.Add(parkingSlots[i]);

                carEcsEntity.Get<CarActivatedMovableEvent>();

                return;
            }
        }

    }

    private void CancelParkingReserved(ParkingCancelReservationEvent cancelReservationEvent, List<ParkingSlot> parkingSlots)
    {
        if (parkingSlots.Contains(cancelReservationEvent.parkingSlot))
        {
            int slotIndex = parkingSlots.IndexOf(cancelReservationEvent.parkingSlot);

            ref var parkingComponent1 = ref parkingSlots[slotIndex].Entity.Get<ParkingComponent>();
            parkingComponent1.car = null;
            parkingComponent1.isReserved = false;

            _reservedParkingSlots.Remove(cancelReservationEvent.parkingSlot);
        }
    }

    public void ToggleSwitchRayReaderActiveEvent(List<ParkingSlot> parkingSlots)
    {
        int maxReservedParkingSlot = 0;

        for (int i = 0; i < parkingSlots.Count; i++)
        {
            ref var parkingComponent = ref parkingSlots[i].Entity.Get<ParkingComponent>();

            if (parkingComponent.isReserved)
            {
                maxReservedParkingSlot++;
            }
            else
            {
                _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
                return;
            }

            if (maxReservedParkingSlot == parkingSlots.Count)
            {
                _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
                return;
            }
        }
    }
}

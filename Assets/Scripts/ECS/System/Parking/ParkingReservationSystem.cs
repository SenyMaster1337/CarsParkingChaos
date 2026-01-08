using System.Collections.Generic;
using Leopotam.Ecs;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.ECS.Components;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class ParkingReservationSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<ParkingReservationComponent> _filter;
        private EcsFilter<ReservedParkingSlotEvent> _reservedSlot;
        private EcsFilter<ParkingCancelReservationEvent> _cancelParkingReserve;
        private EcsFilter<VerifyCarsInParkingDataEvent> _verifyCarsInParkingData;
        private EcsFilter<AddParkingSlotEvent> _addParkingSlotFilter;
        private EcsFilter<EnableRaycastReaderToggleSwitchMethodEvent>
            _enableRaycastReaderToggleSwitchFilter;
        private EcsFilter<DisableRaycastReaderToggleSwitchMethodEvent>
            _disableRaycastReaderToggleSwitchFilter;

        private List<ParkingSlot> _reservedParkingSlots;
        private bool _isParkingFull;
        private bool _isToggleSwitchRaycastReaderEventEnable;
        private bool _isSavingDataActive;
        private StaticData _staticData;

        public ParkingReservationSystem()
        {
            _reservedParkingSlots = new List<ParkingSlot>();
            _isParkingFull = false;
            _isToggleSwitchRaycastReaderEventEnable = true;
            _isSavingDataActive = false;
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var parkingReservationComponent = ref _filter.Get1(entity);

                foreach (var reservedEntity in _reservedSlot)
                {
                    ref var reserveEvent = ref _reservedSlot.Get1(entity);

                    var reservedEntityEvent =
                        _reservedSlot.GetEntity(reservedEntity);
                    ReserveParkingSlot(reserveEvent.CarEntity,
                        parkingReservationComponent.ParkingSlots);

                    if (_isToggleSwitchRaycastReaderEventEnable)
                        ToggleSwitchRaycastReaderActiveEvent(
                            parkingReservationComponent.ParkingSlots, entity);

                    reservedEntityEvent.Del<ReservedParkingSlotEvent>();
                }

                foreach (var cancelEntity in _cancelParkingReserve)
                {
                    ref var cancelReservationEvent =
                        ref _cancelParkingReserve.Get1(cancelEntity);

                    var cancelEntityEvent =
                        _cancelParkingReserve.GetEntity(cancelEntity);
                    CancelParkingReserved(cancelReservationEvent,
                        parkingReservationComponent.ParkingSlots);

                    if (_isToggleSwitchRaycastReaderEventEnable)
                        ToggleSwitchRaycastReaderActiveEvent(
                            parkingReservationComponent.ParkingSlots, entity);

                    cancelEntityEvent.Del<ParkingCancelReservationEvent>();
                }

                foreach (var verifyEntity in _verifyCarsInParkingData)
                {
                    var verifyEntityEvent =
                        _verifyCarsInParkingData.GetEntity(verifyEntity);
                    VerifyCarsInParkingData(
                        parkingReservationComponent.ParkingSlots);
                    verifyEntityEvent.Del<VerifyCarsInParkingDataEvent>();
                }

                foreach (var addParkingSlotEntity in _addParkingSlotFilter)
                {
                    var addSlotEventEntity =
                        _addParkingSlotFilter.GetEntity(addParkingSlotEntity);
                    AddParkingSlot(ref parkingReservationComponent,
                        addSlotEventEntity);
                    addSlotEventEntity.Del<AddParkingSlotEvent>();
                }

                foreach (var enableToggleSwitchRaycastReaderEntity in
                    _enableRaycastReaderToggleSwitchFilter)
                {
                    _isToggleSwitchRaycastReaderEventEnable = true;
                    _enableRaycastReaderToggleSwitchFilter
                        .GetEntity(enableToggleSwitchRaycastReaderEntity)
                        .Del<EnableRaycastReaderToggleSwitchMethodEvent>();
                }

                foreach (var disableToggleSwitchRaycastReaderEntity in
                    _disableRaycastReaderToggleSwitchFilter)
                {
                    _isToggleSwitchRaycastReaderEventEnable = false;
                    _disableRaycastReaderToggleSwitchFilter
                        .GetEntity(disableToggleSwitchRaycastReaderEntity)
                        .Del<DisableRaycastReaderToggleSwitchMethodEvent>();
                }
            }
        }

        private void AddParkingSlot(
            ref ParkingReservationComponent parkingReservationComponent,
            EcsEntity addSlotEventEntity)
        {
            var parkingSlotNewEntity = _ecsWorld.NewEntity();
            ref var parkingComponent = ref parkingSlotNewEntity
                .Get<ParkingComponent>();
            parkingComponent.IsReserved = false;

            ref var parkingSlotEvent =
                ref addSlotEventEntity.Get<AddParkingSlotEvent>();
            parkingSlotEvent.ParkingSlot.Entity = parkingSlotNewEntity;

            parkingReservationComponent.ParkingSlots.Add(
                parkingSlotEvent.ParkingSlot);
        }

        private void ReserveParkingSlot(
            EcsEntity carEcsEntity,
            List<ParkingSlot> parkingSlots)
        {
            ref var carComponent = ref carEcsEntity.Get<CarComponent>();

            for (int i = 0; i < parkingSlots.Count; i++)
            {
                ref var parkingComponent = ref parkingSlots[i].Entity
                    .Get<ParkingComponent>();

                if (parkingComponent.IsReserved == false)
                {
                    carComponent.CanClickable = false;
                    carComponent.ParkingReservedSlot = parkingSlots[i];

                    parkingComponent.IsReserved = true;
                    _reservedParkingSlots.Add(parkingSlots[i]);

                    carEcsEntity.Get<ActivateCarMovableEvent>();

                    return;
                }
            }
        }

        private void CancelParkingReserved(
            ParkingCancelReservationEvent cancelReservationEvent,
            List<ParkingSlot> parkingSlots)
        {
            if (parkingSlots.Contains(cancelReservationEvent.ParkingSlot))
            {
                int slotIndex = parkingSlots.IndexOf(
                    cancelReservationEvent.ParkingSlot);

                ref var parkingComponent1 = ref parkingSlots[slotIndex].Entity
                    .Get<ParkingComponent>();
                parkingComponent1.IsReserved = false;

                _reservedParkingSlots.Remove(cancelReservationEvent.ParkingSlot);
                _isParkingFull = false;
            }
        }

        public void ToggleSwitchRaycastReaderActiveEvent(
            List<ParkingSlot> parkingSlots,
            int entity)
        {
            if (_reservedParkingSlots.Count == parkingSlots.Count)
            {
                _isParkingFull = true;
                SaveCarInParkingData(entity, parkingSlots);
                _ecsWorld.NewEntity().Get<RaycastReaderDisableEvent>();
                _ecsWorld.NewEntity().Get<DisableButtonsEvent>();
                return;
            }
            else
            {
                _isParkingFull = false;
                DeclineSaveCarInParkingData(entity);
                _ecsWorld.NewEntity().Get<EnableRaycastReaderEvent>();
                _ecsWorld.NewEntity().Get<EnableButtonsEvent>();
                return;
            }
        }

        private void SaveCarInParkingData(
            int entity,
            List<ParkingSlot> parkingSlots)
        {
            if (_isSavingDataActive == true)
                return;

            if (_reservedParkingSlots.Count == parkingSlots.Count)
            {
                _filter.GetEntity(entity).Get<TimerComponent>() =
                    new TimerComponent
                    {
                        TimeLeft =
                            _staticData.TimeLeftInTimerToVerifyCarsInParking,
                        IsActive = true,
                    };

                _isSavingDataActive = true;
            }
        }

        private void DeclineSaveCarInParkingData(int entity)
        {
            _isSavingDataActive = false;

            if (_filter.GetEntity(entity).Has<TimerComponent>())
            {
                _filter.GetEntity(entity).Del<TimerComponent>();
            }
        }

        private void VerifyCarsInParkingData(List<ParkingSlot> parkingSlots)
        {
            _isSavingDataActive = false;

            if (_reservedParkingSlots == null ||
                _reservedParkingSlots.Count < parkingSlots.Count ||
                _isParkingFull == false)
                return;

            _ecsWorld.NewEntity().Get<ShowLossWindowEvent>();
        }
    }
}
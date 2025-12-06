using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CarSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CarComponent> _filter;

    private StaticData _staticData;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var component = ref _filter.Get1(entity);

            if (component.reservedSeats.Count == component.maxPassengersSlots && component.isNotEmptySeats == false)
            {
                component.isNotEmptySeats = true;
                StartCancelParkingReserverEvent(component.parkingReservedSlot);
            }

            if (component.passengers.Count == component.maxPassengersSlots && component.isAllPassengersBoarded == false)
            {
                component.isAllPassengersBoarded = true;
                StartTimer(entity, _staticData.TimeDisableCarInScene);
            }
        }
    }

    private void StartTimer(int entity, float duration)
    {
        _filter.GetEntity(entity).Get<TimerComponent>() = new TimerComponent
        {
            TimeLeft = duration,
            IsActive = true
        };
    }

    private void StartCancelParkingReserverEvent(ParkingSlot slot)
    {
        _ecsWorld.NewEntity().Get<ParkingCancelReservationEvent>() = new ParkingCancelReservationEvent
        {
            parkingSlot = slot
        };
    }
}

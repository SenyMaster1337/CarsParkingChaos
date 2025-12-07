using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CarLeavingSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CarLeavingComponent> _carLeavingFilter;
    private EcsFilter<CarComponent> _carComponentFilter;

    private StaticData _staticData;

    public void Run()
    {
        foreach (var leavingEntity in _carLeavingFilter)
        {
            foreach (var carEntity in _carComponentFilter)
            {
                ref var component = ref _carComponentFilter.Get1(carEntity);

                if (component.reservedSeats.Count == component.maxPassengersSlots && component.isNotEmptySeats == false)
                {
                    component.isNotEmptySeats = true;
                    StartCancelParkingReserverEvent(component.parkingReservedSlot);

                    ref var leavingComponent = ref _carLeavingFilter.Get1(leavingEntity);
                    leavingComponent.cars.Remove(component.car);
                }

                if (component.passengers.Count == component.maxPassengersSlots && component.isAllPassengersBoarded == false)
                {
                    component.isAllPassengersBoarded = true;
                    StartTimer(carEntity, _staticData.TimeDisableCarInScene);
                }
            }
        }
    }

    private void StartTimer(int entity, float duration)
    {
        _carComponentFilter.GetEntity(entity).Get<TimerComponent>() = new TimerComponent
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

using Leopotam.Ecs;

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

                if (component.ReservedSeats.Count == component.MaxPassengersSlots && component.IsNotEmptySeats == false)
                {
                    component.IsNotEmptySeats = true;
                    StartCancelParkingReserverEvent(component.ParkingReservedSlot);

                    ref var leavingComponent = ref _carLeavingFilter.Get1(leavingEntity);
                    leavingComponent.Cars.Remove(component.Car);
                }

                if (component.Passengers.Count == component.MaxPassengersSlots && component.IsAllPassengersBoarded == false)
                {
                    component.IsAllPassengersBoarded = true;
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
            IsActive = true,
        };
    }

    private void StartCancelParkingReserverEvent(ParkingSlot slot)
    {
        _ecsWorld.NewEntity().Get<ParkingCancelReservationEvent>() = new ParkingCancelReservationEvent
        {
            ParkingSlot = slot,
        };
    }
}

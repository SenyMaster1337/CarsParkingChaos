using Leopotam.Ecs;
using System.Collections.Generic;

public class PassengerSortingInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    public void Init()
    {
        var passengerSortingNewEntity = _ecsWorld.NewEntity();

        ref var passengerSortingComponent = ref passengerSortingNewEntity.Get<PassengerSortingComponent>();

        ref var getDataEvent = ref passengerSortingNewEntity.Get<GetPassengersAndCarsDataEvent>();
        getDataEvent.passengers = new List<Passenger>();
        getDataEvent.cars = new List<Vehicle>();
    }
}

using Leopotam.Ecs;
using System.Collections.Generic;

public class ShuffleInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private List<Vehicle> _cars;
    private List<Passenger> _passengers;

    public ShuffleInitSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        var passengerShuffleNewEntity = _ecsWorld.NewEntity();
        
        ref var passengerShuffleComponent = ref passengerShuffleNewEntity.Get<ShuffleComponent>();
        passengerShuffleComponent.cars = _cars;
        passengerShuffleComponent.passengers = _passengers;
    }
}

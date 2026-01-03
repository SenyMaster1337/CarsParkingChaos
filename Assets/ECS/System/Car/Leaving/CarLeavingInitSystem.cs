using Leopotam.Ecs;
using System.Collections.Generic;

public class CarLeavingInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private List<Vehicle> _cars;

    public CarLeavingInitSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        var carLeavingNewEntity = _ecsWorld.NewEntity();

        ref var carLeavingComponent = ref carLeavingNewEntity.Get<CarLeavingComponent>();
        carLeavingComponent.cars = _cars;   
    }
}

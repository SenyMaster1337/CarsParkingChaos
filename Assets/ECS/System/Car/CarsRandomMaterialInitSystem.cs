using Leopotam.Ecs;
using System.Collections.Generic;

public class CarsRandomMaterialInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private List<Vehicle> _cars;

    public CarsRandomMaterialInitSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        var carsRandomColorNewEntity = _ecsWorld.NewEntity();

        ref var carsRandomColorComponent = ref carsRandomColorNewEntity.Get<CarsRandomColorComponent>();
        carsRandomColorComponent.cars = _cars;
    }
}

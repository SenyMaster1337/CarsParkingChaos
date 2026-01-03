using Leopotam.Ecs;
using System.Collections.Generic;

public class PassengersCountShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private List<Passenger> _passengers;
    private PassengersCountText _passengersCountText;

    public PassengersCountShowerInitSystem(PassengersCountText currentPassengersCount)
    {
        _passengersCountText = currentPassengersCount;
    }

    public void Init()
    {
        var passengersCountShowerNewEntity = _ecsWorld.NewEntity();

        ref var passengersCountShowerComponent = ref passengersCountShowerNewEntity.Get<PassengersCountShowerComponent>();
        passengersCountShowerComponent.passengersCountText = _passengersCountText;

        passengersCountShowerComponent.passengersCountText.Value.SetText($"{_passengers.Count}");
    }
}

using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersCountShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    private List<Passenger> _passengers;
    private PassengersCountText _passengersCountText;

    public PassengersCountShowerInitSystem(List<Passenger> passengers, PassengersCountText currentPassengersCount)
    {
        _passengers = passengers;
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

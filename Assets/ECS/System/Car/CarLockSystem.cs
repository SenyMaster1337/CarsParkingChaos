using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CarLockSystem : IEcsRunSystem
{
    private readonly float _timeLeftToTimer = 2f;
    private EcsFilter<CarComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var carComponent = ref _filter.Get1(entity);  

            if (carComponent.passengers.Count == carComponent.maxPassengersSlots && carComponent.isAllPassengersBoarded == false)
            {

                carComponent.isAllPassengersBoarded = true;
                StartTimer(entity, _timeLeftToTimer);
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
}

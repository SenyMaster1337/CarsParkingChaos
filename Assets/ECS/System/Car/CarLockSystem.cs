using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CarLockSystem : IEcsRunSystem
{
    private EcsFilter<CarComponent> _filter;

    private StaticData _staticData;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var carComponent = ref _filter.Get1(entity);  

            if (carComponent.passengers.Count == carComponent.maxPassengersSlots && carComponent.isAllPassengersBoarded == false)
            {
                carComponent.isAllPassengersBoarded = true;
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
}

using Leopotam.Ecs;
using UnityEngine;

public class TimerSystem : IEcsRunSystem
{
    private EcsFilter<TimerComponent> _filter;
    private EcsFilter<NoTimeLeftEvent> _noTimeLeft;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var timer = ref _filter.Get1(entity);
            var entityComponent = _filter.GetEntity(entity);

            if (timer.IsActive == false)
                continue;

            timer.TimeLeft -= Time.deltaTime;

            if (timer.TimeLeft <= 0f)
            {
                timer.IsActive = false;
                timer.TimeLeft = 0f;

                entityComponent.Get<NoTimeLeftEvent>();
                entityComponent.Del<TimerComponent>();
                Debug.Log("TimerSystem: Таймер завершен!");
            }
        }

        foreach (var entityEvent in _noTimeLeft)
        {
            var entityNoTimeLeftEvent = _noTimeLeft.GetEntity(entityEvent);
            TryAddDisableComponentToCar(entityNoTimeLeftEvent);
            TryAddRestartWindow(entityNoTimeLeftEvent);
            entityNoTimeLeftEvent.Del<NoTimeLeftEvent>();
        }
    }

    private void TryAddDisableComponentToCar(EcsEntity entityNoTimeLeftEvent)
    {
        if (entityNoTimeLeftEvent.Has<CarComponent>())
        {
            entityNoTimeLeftEvent.Get<DisableComponent>();
        }
    }

    private void TryAddRestartWindow(EcsEntity entityNoTimeLeftEvent)
    {
        if (entityNoTimeLeftEvent.Has<ParkingReservationComponent>())
        {
            entityNoTimeLeftEvent.Get<VerifyCarsInParkingDataEvent>();
        }
    }
}

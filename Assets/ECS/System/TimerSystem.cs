using Leopotam.Ecs;
using UnityEngine;

public class TimerSystem : IEcsRunSystem
{
    private EcsFilter<TimerComponent> _filter;

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

                entityComponent.Del<TimerComponent>();
                Debug.Log("TimerSystem: Таймер завершен!");

                AddDisableComponent(entity);
            }
        }
    }

    private void AddDisableComponent(int entity)
    {
        _filter.GetEntity(entity).Get<DisableComponent>();
    }
}

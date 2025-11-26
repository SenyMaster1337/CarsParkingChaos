using Leopotam.Ecs;
using UnityEngine;

public class CooldownSystem : IEcsRunSystem
{
    private readonly EcsFilter<CooldownEvent> _cooldown;

    public void Run()
    {
        foreach (var i in _cooldown)
        {
            ref var cooldown = ref _cooldown.Get1(i);
            cooldown.remainingTime -= Time.deltaTime;

            if (cooldown.remainingTime <= 0)
            {
                ref var entity = ref _cooldown.GetEntity(i);
                entity.Del<CooldownEvent>();
            }
        }
    }

}

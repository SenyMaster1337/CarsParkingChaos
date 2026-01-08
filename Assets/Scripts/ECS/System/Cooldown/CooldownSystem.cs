using Leopotam.Ecs;
using UnityEngine;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class CooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CooldownEvent> _cooldown;

        public void Run()
        {
            foreach (var i in _cooldown)
            {
                ref var cooldown = ref _cooldown.Get1(i);
                cooldown.RemainingTime -= Time.deltaTime;

                if (cooldown.RemainingTime <= 0)
                {
                    ref var entity = ref _cooldown.GetEntity(i);
                    entity.Del<CooldownEvent>();
                }
            }
        }
    }
}

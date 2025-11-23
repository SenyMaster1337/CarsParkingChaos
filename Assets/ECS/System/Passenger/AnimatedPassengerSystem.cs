using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPassengerSystem : IEcsRunSystem
{
    private static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));

    private EcsFilter<PassengerMovableComponent, PassengerAnimationComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var movabe = ref _filter.Get1(entity);
            ref var animation = ref _filter.Get2(entity);

            animation.animator.SetBool(IsWalking, movabe.isMoving);
        }
    }
}

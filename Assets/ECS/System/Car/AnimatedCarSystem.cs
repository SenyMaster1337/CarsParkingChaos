using Leopotam.Ecs;
using UnityEngine;

public class AnimatedCarSystem : IEcsRunSystem
{
    private static readonly int IsLeaving = Animator.StringToHash(nameof(IsLeaving));
    private static readonly int IsCrashed = Animator.StringToHash(nameof(IsCrashed));

    private EcsFilter<CarComponent, CarAnimationComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var carComponent = ref _filter.Get1(entity);
            ref var animationComponent = ref _filter.Get2(entity);

            animationComponent.animator.SetBool(IsLeaving, carComponent.isAllPassengersBoarded);
            animationComponent.animator.SetBool(IsCrashed, carComponent.isCrashed);
        }
    }
}

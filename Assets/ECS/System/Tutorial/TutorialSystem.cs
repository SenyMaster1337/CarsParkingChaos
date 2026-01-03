using Leopotam.Ecs;
using System.Collections.Generic;

public class TutorialSystem : IEcsRunSystem
{
    private EcsFilter<TutorialHideHandEvent> _handTutorialHide;
    private List<Vehicle> _cars;

    public TutorialSystem(List<Vehicle> cars) 
    {
        _cars = cars;
    }

    public void Run()
    {
        foreach (var handTutorialEntity in _handTutorialHide)
        {
            ref var handHideEvent = ref _handTutorialHide.Get1(handTutorialEntity);

            HideHand(handHideEvent.ecsEntity);
            ShowHand();
            _handTutorialHide.GetEntity(handTutorialEntity).Del<TutorialHideHandEvent>();
        }
    }

    private void HideHand(EcsEntity carEcsEntity)
    {
        ref var carTutorialComponent = ref carEcsEntity.Get<TutorialComponent>();
        carTutorialComponent.windowGroup.alpha = 0f;
        carTutorialComponent.windowGroup.interactable = false;
        carTutorialComponent.windowGroup.blocksRaycasts = false;
        carEcsEntity.Del<TutorialComponent>();
    }

    private void ShowHand()
    {
        foreach (var car in _cars)
        {
            if (car.Entity.IsAlive() && car.Entity.Has<TutorialComponent>())
            {
                ref var tutorialComponent = ref car.Entity.Get<TutorialComponent>();
                tutorialComponent.windowGroup.alpha = 1f;
                tutorialComponent.windowGroup.interactable = true;
                tutorialComponent.windowGroup.blocksRaycasts = true;
                return;
            }
        }
    }
}

using Leopotam.Ecs;
using System.Collections.Generic;

public class TutorialSystem : IEcsRunSystem
{
    private EcsFilter<HandTutorialHideEvent> _handTutorialHide;
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

            var reservedEntityEvent = _handTutorialHide.GetEntity(handTutorialEntity);
            HideTutorialHand(handHideEvent.ecsEntity);
            reservedEntityEvent.Del<HandTutorialHideEvent>();
        }
    }

    private void HideTutorialHand(EcsEntity carEcsEntity)
    {
        ref var carTutorialComponent = ref carEcsEntity.Get<TutorialComponent>();
        carTutorialComponent.windowGroup.alpha = 0f;
        carTutorialComponent.windowGroup.interactable = false;
        carTutorialComponent.windowGroup.blocksRaycasts = false;
        carEcsEntity.Del<TutorialComponent>();

        foreach (var car in _cars)
        {
            if (car.Entity.Has<TutorialComponent>())
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

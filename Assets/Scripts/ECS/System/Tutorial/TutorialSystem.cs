using System.Collections.Generic;
using Leopotam.Ecs;

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

            HideHand(handHideEvent.EcsEntity);
            ShowHand();
            _handTutorialHide.GetEntity(handTutorialEntity).Del<TutorialHideHandEvent>();
        }
    }

    private void HideHand(EcsEntity carEcsEntity)
    {
        ref var carTutorialComponent = ref carEcsEntity.Get<TutorialComponent>();
        carTutorialComponent.WindowGroup.alpha = 0f;
        carTutorialComponent.WindowGroup.interactable = false;
        carTutorialComponent.WindowGroup.blocksRaycasts = false;
        carEcsEntity.Del<TutorialComponent>();
    }

    private void ShowHand()
    {
        foreach (var car in _cars)
        {
            if (car.Entity.IsAlive() && car.Entity.Has<TutorialComponent>())
            {
                ref var tutorialComponent = ref car.Entity.Get<TutorialComponent>();
                tutorialComponent.WindowGroup.alpha = 1f;
                tutorialComponent.WindowGroup.interactable = true;
                tutorialComponent.WindowGroup.blocksRaycasts = true;
                return;
            }
        }
    }
}

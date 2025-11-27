using Leopotam.Ecs;
using System.Collections.Generic;

public class TutorialInitSystem : IEcsInitSystem
{
    private List<Vehicle> _cars;

    public TutorialInitSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        InitHandsTutorial();
    }

    private void InitHandsTutorial()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            ref var carTutorialComponent = ref _cars[i].Entity.Get<TutorialComponent>();
            carTutorialComponent.windowGroup = _cars[i].GetComponentInChildren<HandToturialShower>().WindowGroup;

            if (i == 0)
            {
                carTutorialComponent.windowGroup.alpha = 1f;
                carTutorialComponent.windowGroup.interactable = true;
                carTutorialComponent.windowGroup.blocksRaycasts = true;
            }
            else
            {
                carTutorialComponent.windowGroup.alpha = 0f;
                carTutorialComponent.windowGroup.interactable = false;
                carTutorialComponent.windowGroup.blocksRaycasts = false;
            }
        }
    }
}

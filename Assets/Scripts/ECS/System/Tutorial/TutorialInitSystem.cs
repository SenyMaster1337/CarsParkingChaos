using System.Collections.Generic;
using Leopotam.Ecs;

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
            carTutorialComponent.WindowGroup = _cars[i].GetComponentInChildren<HandToturialShower>().WindowGroup;

            if (i == 0)
            {
                carTutorialComponent.WindowGroup.alpha = 1f;
                carTutorialComponent.WindowGroup.interactable = true;
                carTutorialComponent.WindowGroup.blocksRaycasts = true;
            }
            else
            {
                carTutorialComponent.WindowGroup.alpha = 0f;
                carTutorialComponent.WindowGroup.interactable = false;
                carTutorialComponent.WindowGroup.blocksRaycasts = false;
            }
        }
    }
}

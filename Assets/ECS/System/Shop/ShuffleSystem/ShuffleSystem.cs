using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<ShuffleComponent> _shuffleComponentFilter;
    private EcsFilter<ShuffleEvent> _shuffleEventfilter;

    public void Run()
    {
        foreach (var shuffleComponentEntity in _shuffleComponentFilter)
        {
            ref var shuffleComponent = ref _shuffleComponentFilter.Get1(shuffleComponentEntity);

            foreach (var shuffleEventEntity in _shuffleEventfilter)
            {
                if (shuffleComponent.passengers.Count > 0)
                {
                    StartConfirmBuyingEvent();

                    System.Random random = new();

                    for (int i = 0; i < shuffleComponent.cars.Count; i++)
                    {
                        int randomIndex = random.Next(i, shuffleComponent.cars.Count);

                        ref var firstCarComponent = ref shuffleComponent.cars[i].Entity.Get<CarComponent>();
                        ref var secondCarComponent = ref shuffleComponent.cars[randomIndex].Entity.Get<CarComponent>();

                        if (firstCarComponent.canCrashed == false || secondCarComponent.canCrashed == false)
                            continue;

                        ref var firstCarMovable = ref shuffleComponent.cars[i].Entity.Get<CarMovableComponent>();
                        ref var secondCarMovable = ref shuffleComponent.cars[randomIndex].Entity.Get<CarMovableComponent>();

                        firstCarMovable.spawnPosition = secondCarMovable.currentTransform.position;
                        secondCarMovable.spawnPosition = firstCarMovable.currentTransform.position;

                        secondCarMovable.currentTransform.position = secondCarMovable.spawnPosition;
                        secondCarMovable.currentTransform.position = firstCarMovable.spawnPosition;
                    }
                }

                _ecsWorld.NewEntity().Get<RaycastReaderEnableEvent>();
                _shuffleEventfilter.GetEntity(shuffleEventEntity).Del<ShuffleEvent>();
            }
        }
    }

    private void StartConfirmBuyingEvent()
    {
        var confirmEventNewEntity = _ecsWorld.NewEntity();
        confirmEventNewEntity.Get<ConfirmBuyingEvent>();
        confirmEventNewEntity.Get<PassengerShuffleConfirmBuyingEvent>();
    }
}

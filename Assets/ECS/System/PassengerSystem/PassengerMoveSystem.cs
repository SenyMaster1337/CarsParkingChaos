using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerMoveSystem : IEcsRunSystem
{
    private EcsFilter<PassengerMovableComponent, PassengerComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var movable = ref _filter.Get1(entity);

            if (movable.isMoving)
            {
                if (movable.targetCarPosition != Vector3.zero)
                {
                    MoveToPosition(movable, movable.targetCarPosition);

                    if (movable.currentTransform.position.IsEnoughClose(movable.targetCarPosition, 6f))
                    {
                        ref var component = ref _filter.Get2(entity);
                        component.carComponent.passengers.Add(component);

                        movable.isMoving = false;
                        movable.targetCarPosition = Vector3.zero;
                        AddDisableComponent(entity);
                    }
                }

                if (movable.startQueuePosition != Vector3.zero && movable.isPositionStartQueuePosition == false)
                {
                    MoveToPosition(movable, movable.startQueuePosition);

                    if (movable.currentTransform.position == movable.startQueuePosition)
                    {
                        movable.isPositionStartQueuePosition = true;
                        movable.isMoving = false;
                    }
                }

                if (movable.currentQueuePointPosition != Vector3.zero && movable.isNeedShiftQueue == true)
                {
                    MoveToPosition(movable, movable.currentQueuePointPosition);

                    if (movable.currentTransform.position == movable.currentQueuePointPosition)
                    {
                        movable.isNeedShiftQueue = false;
                        movable.isMoving = false;
                    }
                }
            }
        }
    }

    private static void MoveToPosition(PassengerMovableComponent movable, Vector3 targetPosition)
    {
        movable.currentTransform.LookAt(targetPosition);
        movable.currentTransform.position = Vector3.MoveTowards(movable.currentTransform.position, targetPosition, movable.moveSpeed * Time.deltaTime);
    }

    private void AddDisableComponent(int entity)
    {
        _filter.GetEntity(entity).Get<DisableComponent>();
    }
}

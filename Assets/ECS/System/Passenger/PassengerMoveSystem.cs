using Leopotam.Ecs;
using UnityEngine;

public class PassengerMoveSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<PassengerMovableComponent, PassengerComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var component = ref _filter.Get2(entity);
            ref var movable = ref _filter.Get1(entity);

            TryMoveToStartPointQueue(component, ref movable, _filter.GetEntity(entity));
            TryMoveToNewQueuePoint(ref movable, _filter.GetEntity(entity));

            if (movable.isMoving)
            {
                if (movable.targetCarPosition != Vector3.zero)
                {
                    MoveToPosition(movable, movable.targetCarPosition);

                    if (movable.currentTransform.position.IsEnoughClose(movable.targetCarPosition, 6f))
                    {
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

                if (movable.queuePointPosition != Vector3.zero && movable.isNeedShiftQueue == true)
                {
                    MoveToPosition(movable, movable.queuePointPosition);

                    if (movable.currentTransform.position == movable.queuePointPosition)
                    {
                        movable.isNeedShiftQueue = false;
                        movable.isMoving = false;
                    }
                }
            }
        }
    }

    private void TryMoveToNewQueuePoint(ref PassengerMovableComponent movable, EcsEntity entityEvent)
    {
        if (entityEvent.Has<PassengerMoveInQueuePointEvent>())
        {
            ref var moveQueueEvent = ref entityEvent.Get<PassengerMoveInQueuePointEvent>();

            movable.isMoving = true;
            movable.isNeedShiftQueue = true;
            movable.queuePointPosition = moveQueueEvent.queuePointPosition;
            entityEvent.Del<PassengerMoveInQueuePointEvent>();
        }
    }

    private void TryMoveToStartPointQueue(PassengerComponent component, ref PassengerMovableComponent movable, EcsEntity entityEvent)
    {
        if (entityEvent.Has<PassengerMoveStartQueuePointEvent>())
        {
            movable.isMoving = true;
            movable.startQueuePosition = component.startQueuePosition;
            entityEvent.Del<PassengerMoveStartQueuePointEvent>();
        }
    }

    private void MoveToPosition(PassengerMovableComponent movable, Vector3 targetPosition)
    {
        movable.currentTransform.LookAt(targetPosition);
        movable.currentTransform.position = Vector3.MoveTowards(movable.currentTransform.position, targetPosition, movable.moveSpeed * Time.deltaTime);
    }

    private void AddDisableComponent(int entity)
    {
        _filter.GetEntity(entity).Get<DisableUnitsEvent>();
    }
}

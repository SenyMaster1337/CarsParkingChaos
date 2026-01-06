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

            if (movable.IsMoving)
            {
                if (movable.TargetCarPosition != Vector3.zero)
                {
                    MoveToPosition(movable, movable.TargetCarPosition);

                    if (movable.CurrentTransform.position.IsEnoughClose(movable.TargetCarPosition, 6f))
                    {
                        component.CarComponent.Passengers.Add(component);

                        movable.IsMoving = false;
                        movable.TargetCarPosition = Vector3.zero;
                        AddDisableComponent(entity);
                    }
                }

                if (movable.StartQueuePosition != Vector3.zero && movable.IsPositionStartQueuePosition == false)
                {
                    MoveToPosition(movable, movable.StartQueuePosition);

                    if (movable.CurrentTransform.position == movable.StartQueuePosition)
                    {
                        movable.IsPositionStartQueuePosition = true;
                        movable.IsMoving = false;
                    }
                }

                if (movable.QueuePointPosition != Vector3.zero && movable.IsNeedShiftQueue == true)
                {
                    MoveToPosition(movable, movable.QueuePointPosition);

                    if (movable.CurrentTransform.position == movable.QueuePointPosition)
                    {
                        movable.IsNeedShiftQueue = false;
                        movable.IsMoving = false;
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

            movable.IsMoving = true;
            movable.IsNeedShiftQueue = true;
            movable.QueuePointPosition = moveQueueEvent.QueuePointPosition;
            entityEvent.Del<PassengerMoveInQueuePointEvent>();
        }
    }

    private void TryMoveToStartPointQueue(PassengerComponent component, ref PassengerMovableComponent movable, EcsEntity entityEvent)
    {
        if (entityEvent.Has<PassengerMoveStartQueuePointEvent>())
        {
            movable.IsMoving = true;
            movable.StartQueuePosition = component.StartQueuePosition;
            entityEvent.Del<PassengerMoveStartQueuePointEvent>();
        }
    }

    private void MoveToPosition(PassengerMovableComponent movable, Vector3 targetPosition)
    {
        movable.CurrentTransform.LookAt(targetPosition);
        movable.CurrentTransform.position = Vector3.MoveTowards(movable.CurrentTransform.position, targetPosition, movable.MoveSpeed * Time.deltaTime);
    }

    private void AddDisableComponent(int entity)
    {
        _filter.GetEntity(entity).Get<DisableUnitsEvent>();
    }
}

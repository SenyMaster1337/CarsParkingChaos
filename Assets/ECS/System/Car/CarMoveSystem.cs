using Leopotam.Ecs;
using UnityEngine;

public class CarMoveSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CarMovableComponent, CarComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var movable = ref _filter.Get1(entity);
            ref var component = ref _filter.Get2(entity);

            movable = TryPark(entity, movable, component);
            movable = TryMovableAcritvated(entity, movable);

            if (movable.isMoving)
            {
                if (movable.isReverseEnable == true)
                {
                    MoveToPosition(movable, movable.spawnPosition);

                    if (movable.currentTransform.position == movable.spawnPosition)
                    {
                        movable.isMoving = false;
                        movable.isReverseEnable = false;
                        component.isCrashed = false;
                    }
                }
                else
                {
                    movable.currentTransform.Translate(Vector3.forward * movable.moveSpeed * Time.deltaTime);
                    component = TryDisableCrashHandler(movable, component);
                    movable = TrySpeedUp(movable, component);
                }

                if (movable.targetPoint != Vector3.zero)
                {
                    MoveToPosition(movable, movable.targetPoint);
                    movable.currentTransform.LookAt(movable.targetPoint);
                }
            }

            if (component.reservedSeats.Count == component.maxPassengersSlots && component.isNotEmptySeats == false)
                component.isNotEmptySeats = true;
        }
    }

    private CarMovableComponent TryMovableAcritvated(int entity, CarMovableComponent movable)
    {
        var entityMovableEvent = _filter.GetEntity(entity);

        if (entityMovableEvent.Has<CarActivatedMovableEvent>())
        {
            movable.isMoving = true;
            entityMovableEvent.Del<CarActivatedMovableEvent>();
        }

        return movable;
    }

    private CarMovableComponent TryPark(int entity, CarMovableComponent movable, CarComponent component)
    {
        var entityParkingEvent = _filter.GetEntity(entity);

        if (entityParkingEvent.Has<CarParkingEvent>())
        {
            movable.isMoving = false;
            movable.targetPoint = Vector3.zero;
            movable.currentTransform.position = component.parkingReservedSlot.transform.position;
            movable.currentTransform.rotation = component.rorationCarInParking;
            entityParkingEvent.Del<CarParkingEvent>();
        }

        return movable;
    }

    private void MoveToPosition(CarMovableComponent movable, Vector3 targetPosition)
    {
        movable.currentTransform.position = Vector3.MoveTowards(movable.currentTransform.position, targetPosition, movable.moveSpeed * Time.deltaTime);
    }

    private CarMovableComponent TrySpeedUp(CarMovableComponent movable, CarComponent component)
    {
        if (movable.moveSpeed < 100f && component.canCrashed == false)
        {
            movable.moveSpeed += 100f * Time.deltaTime;
            movable.moveSpeed = Mathf.Min(movable.moveSpeed, 100f);
        }

        return movable;
    }

    private CarComponent TryDisableCrashHandler(CarMovableComponent movable, CarComponent component)
    {
        if (movable.currentTransform.position.SqrDistance(movable.spawnPosition) > component.distanceToDisableCrashHandler && component.canCrashed == true)
        {
            component.canCrashed = false;
            component.crashHandler.enabled = false;
            component.crashHandler.DisableBoxCollider();

            _ecsWorld.NewEntity().Get<ReservedParkingSlotEvent>() = new ReservedParkingSlotEvent
            {
                carEntity = component.car.Entity
            };

            Debug.Log($"{component.car} CrashHanlderDisable");
        }

        return component;
    }
}

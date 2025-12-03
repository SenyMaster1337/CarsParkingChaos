using Leopotam.Ecs;
using UnityEngine;

public class CarMoveSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CarMovableComponent, CarComponent> _filter;

    private StaticData _staticData;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var movable = ref _filter.Get1(entity);
            ref var component = ref _filter.Get2(entity);

            TryPark(entity, ref movable, ref component);
            TryMovableActivated(entity, ref movable, ref component);

            if (movable.isMoving)
            {
                if (movable.isReverseDirectionEnable == true)
                {
                    MoveToPosition(movable, movable.spawnPosition);

                    if (movable.currentTransform.position == movable.spawnPosition)
                    {
                        component.isCrashed = false;
                        movable.isMoving = false;
                        movable.isReverseDirectionEnable = false;
                        component.canClickable = true;
                    }
                }
                else
                {
                    movable.currentTransform.Translate(Vector3.forward * movable.moveSpeed * Time.deltaTime);
                    TryDisableCrashHandler(ref movable, ref component);
                    TrySpeedUp(ref movable, ref component);
                }

                if (movable.targetPoint != Vector3.zero)
                {
                    MoveToPosition(movable, movable.targetPoint);
                    movable.currentTransform.LookAt(movable.targetPoint);
                }
            }

            if (component.reservedSeats.Count == component.maxPassengersSlots && component.isNotEmptySeats == false)
            {
                component.isNotEmptySeats = true;
                StartCancelParkingReserverEvent(component.parkingReservedSlot);
            }
        }
    }

    private void TryMovableActivated(int entity, ref CarMovableComponent movable, ref CarComponent carComponent)
    {
        var entityMovableEvent = _filter.GetEntity(entity);

        if (entityMovableEvent.Has<CarActivatedMovableEvent>())
        {
            carComponent.canClickable = false;
            movable.isMoving = true;
            entityMovableEvent.Del<CarActivatedMovableEvent>();
        }
    }

    private void TryPark(int entity, ref CarMovableComponent movable, ref CarComponent component)
    {
        var entityParkingEvent = _filter.GetEntity(entity);

        if (entityParkingEvent.Has<CarParkingEvent>())
        {
            movable.isMoving = false;
            movable.targetPoint = Vector3.zero;
            movable.currentTransform.position = component.parkingReservedSlot.transform.position;
            movable.currentTransform.rotation = component.rorationCarInParking;
            component.isParked = true;
            entityParkingEvent.Del<CarParkingEvent>();
        }
    }

    private void MoveToPosition(CarMovableComponent movable, Vector3 targetPosition)
    {
        movable.currentTransform.position = Vector3.MoveTowards(movable.currentTransform.position, targetPosition, movable.moveSpeed * Time.deltaTime);
    }

    private void TrySpeedUp(ref CarMovableComponent movable, ref CarComponent component)
    {
        if (movable.moveSpeed < _staticData.MaxLinerCarSpeed && component.canCrashed == false)
        {
            movable.moveSpeed += _staticData.MaxLinerCarSpeed * Time.deltaTime;
            movable.moveSpeed = Mathf.Min(movable.moveSpeed, _staticData.MaxLinerCarSpeed);
        }
    }

    private void TryDisableCrashHandler(ref CarMovableComponent movable, ref CarComponent component)
    {
        if (movable.currentTransform.position.SqrDistance(movable.spawnPosition) > component.distanceToDisableCrashHandler && component.canCrashed == true)
        {
            component.canCrashed = false;
            component.crashHandler.enabled = false;
            component.crashHandler.DisableBoxCollider();
            component.isParked = false;
        }
    }

    private void StartCancelParkingReserverEvent(ParkingSlot slot)
    {
        _ecsWorld.NewEntity().Get<ParkingCancelReservationEvent>() = new ParkingCancelReservationEvent
        {
            parkingSlot = slot
        };
    }
}

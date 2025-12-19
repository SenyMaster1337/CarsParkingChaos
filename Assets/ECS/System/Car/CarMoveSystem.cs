using Leopotam.Ecs;
using UnityEngine;

public class CarMoveSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CarMovableComponent, CarComponent> _filter;
    private EcsFilter<ActivateCarMovableEvent> _activateCarMovableFilter;
    private EcsFilter<CarParkingEvent> _carParkingFilter;

    private StaticData _staticData;
    private float _stopSqrDistance;

    public CarMoveSystem()
    {
        _stopSqrDistance = 0.05f;
    }

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var movable = ref _filter.Get1(entity);
            ref var component = ref _filter.Get2(entity);

            foreach (var activateCarMovableEntity in _activateCarMovableFilter)
            {
                TryMovableActivated(entity, ref movable, ref component);
            }

            foreach (var parkingEntity in _carParkingFilter)
            {
                TryPark(entity, ref movable, ref component);
            }

            if (movable.isMoving)
            {
                if (movable.isReverseDirectionEnable == true)
                {
                    MoveToStartPointWithoutPhysics(ref movable);

                    if (movable.rigidbody.position.SqrDistance(movable.spawnPosition) < _stopSqrDistance)
                    {
                        movable.rigidbody.interpolation = RigidbodyInterpolation.None;
                        movable.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

                        movable.rigidbody.velocity = Vector3.zero;
                        movable.rigidbody.angularVelocity = Vector3.zero;

                        StartCancelParkingReserverEvent(component.parkingReservedSlot);
                        component.isCrashed = false;
                        movable.isMoving = false;
                        movable.isReverseDirectionEnable = false;
                        component.canClickable = true;
                    }
                }
                else
                {
                    //movable.currentTransform.Translate(Vector3.forward * movable.moveSpeed * Time.deltaTime);
                    //movable.rigidbody.MovePosition(movable.rigidbody.position + movable.transform.forward * movable.moveSpeed * Time.fixedDeltaTime);
                    MoveForwardPhysics(ref movable);
                    TryDisableCrashHandler(ref movable, ref component);
                    TrySpeedUp(ref movable, ref component);
                }

                if (movable.targetPoint != Vector3.zero)
                {
                    //movable.transform.LookAt(movable.targetPoint);
                    //movable.rigidbody.MoveRotation(Quaternion.LookRotation((movable.targetPoint - movable.rigidbody.position).normalized));
                    movable.rigidbody.rotation = Quaternion.LookRotation((movable.targetPoint - movable.rigidbody.position).normalized);
                    movable.rigidbody.angularVelocity = Vector3.zero;
                    movable.targetPoint = Vector3.zero;
                }
            }
        }
    }

    private void MoveForwardPhysics(ref CarMovableComponent movable)
    {
        movable.rigidbody.velocity = movable.transform.forward * movable.moveSpeed;
    }

    private void MoveToStartPointWithoutPhysics(ref CarMovableComponent movable)
    {
        movable.rigidbody.MovePosition(movable.rigidbody.position + (-movable.transform.forward) * movable.moveSpeed * Time.fixedDeltaTime);
    }

    private void TryMovableActivated(int entity, ref CarMovableComponent movable, ref CarComponent carComponent)
    {
        var entityMovableEvent = _filter.GetEntity(entity);

        if (entityMovableEvent.Has<ActivateCarMovableEvent>())
        {
            movable.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            movable.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            carComponent.canClickable = false;
            movable.isMoving = true;
            entityMovableEvent.Del<ActivateCarMovableEvent>();
        }
    }

    private void TryPark(int entity, ref CarMovableComponent movable, ref CarComponent component)
    {
        var entityParkingEvent = _filter.GetEntity(entity);

        if (entityParkingEvent.Has<CarParkingEvent>())
        {
            movable.rigidbody.interpolation = RigidbodyInterpolation.None;
            movable.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

            movable.rigidbody.velocity = Vector3.zero;
            movable.rigidbody.angularVelocity = Vector3.zero;

            movable.isMoving = false;
            movable.targetPoint = Vector3.zero;
            movable.rigidbody.position = component.parkingReservedSlot.transform.position;
            movable.rigidbody.rotation = component.rorationCarInParking;
            component.isParked = true;
            entityParkingEvent.Del<CarParkingEvent>();
        }
    }

    private void TrySpeedUp(ref CarMovableComponent movable, ref CarComponent component)
    {
        if (movable.moveSpeed < _staticData.MaxCarSpeed && component.canCrashed == false)
        {
            movable.moveSpeed += _staticData.LinerCarSpeedUp * Time.fixedDeltaTime;
            movable.moveSpeed = Mathf.Min(movable.moveSpeed, _staticData.MaxCarSpeed);
        }
    }

    private void TryDisableCrashHandler(ref CarMovableComponent movable, ref CarComponent component)
    {
        if (movable.rigidbody.position.SqrDistance(movable.spawnPosition) > component.distanceToDisableCrashHandler && component.canCrashed == true)
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
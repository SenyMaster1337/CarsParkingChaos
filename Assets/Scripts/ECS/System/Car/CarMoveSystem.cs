using Leopotam.Ecs;
using UnityEngine;

public class CarMoveSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<CarMovableComponent, CarComponent> _filter;
    private EcsFilter<ActivateCarMovableEvent> _activateCarMovableFilter;
    private EcsFilter<CarParkingEvent> _carParkingFilter;

    private StaticData _staticData;
    private float _stopCarSqrDistance;

    public CarMoveSystem()
    {
        _stopCarSqrDistance = 0.05f;
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

            if (movable.IsMoving)
            {
                if (movable.IsReverseDirectionEnable == true)
                {
                    MoveToStartPointWithoutPhysics(ref movable);

                    if (movable.Rigidbody.position.SqrDistance(movable.SpawnPosition) < _stopCarSqrDistance)
                    {
                        movable.Rigidbody.interpolation = RigidbodyInterpolation.None;
                        movable.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

                        movable.Rigidbody.velocity = Vector3.zero;
                        movable.Rigidbody.angularVelocity = Vector3.zero;

                        StartCancelParkingReserverEvent(component.ParkingReservedSlot);
                        component.IsCrashed = false;
                        movable.IsMoving = false;
                        movable.IsReverseDirectionEnable = false;
                        component.CanClickable = true;
                    }
                }
                else
                {
                    MoveForwardPhysics(ref movable);
                    TryDisableCrashHandler(ref movable, ref component);
                    TrySpeedUp(ref movable, ref component);
                }

                if (movable.TargetPoint != Vector3.zero)
                {
                    movable.Rigidbody.rotation = Quaternion.LookRotation((movable.TargetPoint - movable.Rigidbody.position).normalized);
                    movable.Rigidbody.angularVelocity = Vector3.zero;
                    movable.TargetPoint = Vector3.zero;
                }
            }
        }
    }

    private void MoveForwardPhysics(ref CarMovableComponent movable)
    {
        movable.Rigidbody.velocity = movable.Transform.forward * movable.MoveSpeed;
    }

    private void MoveToStartPointWithoutPhysics(ref CarMovableComponent movable)
    {
        movable.Rigidbody.MovePosition(movable.Rigidbody.position - movable.Transform.forward * (movable.MoveSpeed * Time.fixedDeltaTime));
    }

    private void TryMovableActivated(int entity, ref CarMovableComponent movable, ref CarComponent carComponent)
    {
        var entityMovableEvent = _filter.GetEntity(entity);

        if (entityMovableEvent.Has<ActivateCarMovableEvent>())
        {
            movable.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            movable.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            carComponent.CanClickable = false;
            movable.IsMoving = true;
            entityMovableEvent.Del<ActivateCarMovableEvent>();
        }
    }

    private void TryPark(int entity, ref CarMovableComponent movable, ref CarComponent component)
    {
        var entityParkingEvent = _filter.GetEntity(entity);

        if (entityParkingEvent.Has<CarParkingEvent>())
        {
            movable.Rigidbody.interpolation = RigidbodyInterpolation.None;
            movable.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

            movable.Rigidbody.velocity = Vector3.zero;
            movable.Rigidbody.angularVelocity = Vector3.zero;

            movable.IsMoving = false;
            movable.TargetPoint = Vector3.zero;
            movable.Rigidbody.position = component.ParkingReservedSlot.transform.position;
            movable.Rigidbody.rotation = component.RorationCarInParking;
            component.IsParked = true;
            entityParkingEvent.Del<CarParkingEvent>();
        }
    }

    private void TrySpeedUp(ref CarMovableComponent movable, ref CarComponent component)
    {
        if (movable.MoveSpeed < _staticData.MaxCarSpeed && component.CanCrashed == false)
        {
            movable.MoveSpeed += _staticData.LinerCarSpeedUp * Time.fixedDeltaTime;
            movable.MoveSpeed = Mathf.Min(movable.MoveSpeed, _staticData.MaxCarSpeed);
        }
    }

    private void TryDisableCrashHandler(ref CarMovableComponent movable, ref CarComponent component)
    {
        if (movable.Rigidbody.position.SqrDistance(movable.SpawnPosition) > component.DistanceToDisableCrashHandler && component.CanCrashed == true)
        {
            component.CanCrashed = false;
            component.CrashHandler.enabled = false;
            component.CrashHandler.DisableBoxCollider();
            component.IsParked = false;
        }
    }

    private void StartCancelParkingReserverEvent(ParkingSlot slot)
    {
        _ecsWorld.NewEntity().Get<ParkingCancelReservationEvent>() = new ParkingCancelReservationEvent
        {
            ParkingSlot = slot,
        };
    }
}
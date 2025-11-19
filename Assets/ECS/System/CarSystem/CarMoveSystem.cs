using Leopotam.Ecs;
using UnityEngine;

public class CarMoveSystem : IEcsRunSystem
{
    private const float DistanceToDisableCrashHandler = 18f;

    private EcsFilter<CarMovableComponent, CarComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var movable = ref _filter.Get1(entity);
            ref var component = ref _filter.Get2(entity);

            if (movable.isMoving)
            {
                if (movable.isReverseEnable == false)
                {
                    movable.currentTransform.Translate(Vector3.forward * movable.moveSpeed * Time.deltaTime);
                }
                else
                {
                    movable.currentTransform.position = Vector3.MoveTowards(movable.currentTransform.position, movable.spawnPosition, movable.moveSpeed * Time.deltaTime);

                    if (movable.currentTransform.position == movable.spawnPosition)
                    {
                        movable.isMoving = false;
                        movable.isReverseEnable = false;
                    }
                }

                if (movable.targetPoint != Vector3.zero)
                {
                    movable.currentTransform.LookAt(movable.targetPoint);
                    movable.currentTransform.position = Vector3.MoveTowards(movable.currentTransform.position, movable.targetPoint, movable.moveSpeed * Time.deltaTime);
                }
            }

            if (component.reservedSeats.Count == component.maxPassengersSlots && component.isNotEmptySeats == false)
            {
                Debug.Log("Все места зарезервированы");
                component.isNotEmptySeats = true;
            }

            DisableCrashHandler(movable, component);
        }
    }

    private static void DisableCrashHandler(CarMovableComponent movable, CarComponent component)
    {
        if (movable.currentTransform.position.SqrDistance(movable.spawnPosition) > DistanceToDisableCrashHandler && component.canCrashed == true)
        {
            component.crashHandler.enabled = false;
            component.crashHandler.DisableBoxCollider();
            component.canCrashed = false;
        }
    }
}

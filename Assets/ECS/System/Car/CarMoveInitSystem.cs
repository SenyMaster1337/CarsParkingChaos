using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveInitSystem : IEcsInitSystem
{
    private StaticData _staticData;
    private List<Vehicle> _cars;

    public CarMoveInitSystem(List<Vehicle> cars)
    {
        _cars = cars;
    }

    public void Init()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            ref var carMovable = ref _cars[i].Entity.Get<CarMovableComponent>();
            carMovable.car = _cars[i];
            carMovable.transform = _cars[i].gameObject.transform;
            carMovable.spawnPosition = _cars[i].gameObject.transform.position;
            carMovable.targetPoint = Vector3.zero;

            carMovable.moveSpeed = _staticData.CarSpeed;

            carMovable.isMoving = false;
            carMovable.isReverseDirectionEnable = false;
            carMovable.isSpeedUpEnable = false;

            carMovable.rigidbody = _cars[i].GetComponent<Rigidbody>();
            carMovable.rigidbody.drag = 5f;
            carMovable.rigidbody.angularDrag = 10f;
            carMovable.rigidbody.interpolation = RigidbodyInterpolation.None;
            carMovable.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            carMovable.rigidbody.constraints |= RigidbodyConstraints.FreezeRotationX;
            carMovable.rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;
            carMovable.carRotates = new();
            //carMovable.rigidbody.isKinematic = true;
        }
    }
}

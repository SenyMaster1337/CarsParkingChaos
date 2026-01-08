using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.ECS.Components;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Systems
{
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
                carMovable.Transform = _cars[i].gameObject.transform;
                carMovable.SpawnPosition = _cars[i].gameObject.transform.position;
                carMovable.TargetPoint = Vector3.zero;

                carMovable.MoveSpeed = _staticData.CarSpeed;

                carMovable.IsMoving = false;
                carMovable.IsReverseDirectionEnable = false;

                carMovable.Rigidbody = _cars[i].GetComponent<Rigidbody>();
                carMovable.Rigidbody.drag = 5f;
                carMovable.Rigidbody.angularDrag = 10f;
                carMovable.Rigidbody.interpolation = RigidbodyInterpolation.None;
                carMovable.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                carMovable.Rigidbody.constraints |= RigidbodyConstraints.FreezeRotationX;
                carMovable.Rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;
                carMovable.CarRotates = new List<CarRotate>();
            }
        }
    }
}

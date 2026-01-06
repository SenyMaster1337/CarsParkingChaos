using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class PassengersInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private StaticData _staticData;
        private SceneData _sceneData;

        private List<Passenger> _passengers;
        private StartQueuePoint _startQueuePoint;

        public PassengersInitSystem(StartQueuePoint startQueuePoint)
        {
            _startQueuePoint = startQueuePoint;
            _passengers = new List<Passenger>();
        }

        public void Init()
        {
            InitPassengers();
        }

        private void InitPassengers()
        {
            for (int i = 0; i < _passengers.Count; i++)
            {
                var passengerNewEntity = _ecsWorld.NewEntity();

                ref var passengerComponent = ref passengerNewEntity.Get<PassengerComponent>();
                passengerComponent.Passenger = _passengers[i];
                passengerComponent.Renderer = _passengers[i].gameObject.GetComponentInChildren<PassengerRenderer>().Renderer;
                passengerComponent.StartQueuePosition = _startQueuePoint.transform.position;
                passengerComponent.IsSorted = false;

                ref var passengerMovable = ref passengerNewEntity.Get<PassengerMovableComponent>();
                passengerMovable.CurrentTransform = _passengers[i].gameObject.transform;

                if (i < _sceneData.QueuePositions.Count)
                {
                    passengerMovable.CurrentTransform.position = _sceneData.QueuePositions[i].position;
                    passengerMovable.CurrentTransform.rotation = _sceneData.QueuePositions[i].rotation;
                    passengerMovable.QueuePointPosition = _sceneData.QueuePositions[i].position;
                }
                else
                {
                    int lastIndex = _sceneData.QueuePositions.Count - 1;
                    passengerComponent.Passenger.gameObject.SetActive(false);
                    passengerMovable.CurrentTransform.position = _sceneData.QueuePositions[lastIndex].position;
                    passengerMovable.CurrentTransform.rotation = _sceneData.QueuePositions[lastIndex].rotation;
                    passengerMovable.QueuePointPosition = _sceneData.QueuePositions[lastIndex].position;

                    if (i == _passengers.Count - 1)
                        passengerComponent.Passenger.gameObject.SetActive(true);
                }

                passengerMovable.MoveSpeed = _staticData.PassengerSpeed;
                passengerMovable.TargetCarPosition = Vector3.zero;

                passengerMovable.IsMoving = false;
                passengerMovable.IsNeedShiftQueue = false;

                if (i == 0)
                    passengerMovable.IsPositionStartQueuePosition = true;
                else
                    passengerMovable.IsPositionStartQueuePosition = false;

                ref var passengerAnimationComponent = ref passengerNewEntity.Get<PassengerAnimationComponent>();
                passengerAnimationComponent.Animator = _passengers[i].GetComponentInChildren<Animator>();

                _passengers[i].Entity = passengerNewEntity;
            }

            _ecsWorld.NewEntity().Get<SortPassengerInColorCarsEvent>();
        }
    }
}

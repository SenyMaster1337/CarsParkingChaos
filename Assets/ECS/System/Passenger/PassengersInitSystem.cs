using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengersInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private StaticData _staticData;
    private SceneData _sceneData;

    private List<Passenger> _passengers;
    private StartQueuePoint _startQueuePoint;

    public PassengersInitSystem(List<Passenger> passengers, StartQueuePoint startQueuePoint)
    {
        _passengers = passengers;
        _startQueuePoint = startQueuePoint;
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
            passengerComponent.passenger = _passengers[i];
            passengerComponent.renderer = _passengers[i].gameObject.GetComponentInChildren<Renderer>();
            passengerComponent.startQueuePosition = _startQueuePoint.transform.position;

            ref var passengerMovable = ref passengerNewEntity.Get<PassengerMovableComponent>();
            passengerMovable.currentTransform = _passengers[i].gameObject.transform;

            if (i < _sceneData.QueuePositions.Count)
            {
                passengerMovable.currentTransform.position = _sceneData.QueuePositions[i].position;
                passengerMovable.currentTransform.rotation = _sceneData.QueuePositions[i].rotation;
                passengerMovable.queuePointPosition = _sceneData.QueuePositions[i].position;
            }
            else
            {
                int lastIndex = _sceneData.QueuePositions.Count - 1;
                passengerMovable.currentTransform.position = _sceneData.QueuePositions[lastIndex].position;
                passengerMovable.currentTransform.rotation = _sceneData.QueuePositions[lastIndex].rotation;
                passengerMovable.queuePointPosition = _sceneData.QueuePositions[lastIndex].position;
            }

            passengerMovable.moveSpeed = _staticData.PassengerSpeed;
            passengerMovable.targetCarPosition = Vector3.zero;

            passengerMovable.isMoving = false;
            passengerMovable.isNeedShiftQueue = false;

            if (i == 0)
                passengerMovable.isPositionStartQueuePosition = true;
            else
                passengerMovable.isPositionStartQueuePosition = false;

            ref var passengerAnimationComponent = ref passengerNewEntity.Get<PassengerAnimationComponent>();
            passengerAnimationComponent.animator = _passengers[i].GetComponentInChildren<Animator>();

            _passengers[i].Entity = passengerNewEntity;
        }
    }
}

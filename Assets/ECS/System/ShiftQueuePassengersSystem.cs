using Leopotam.Ecs;
using System.Collections.Generic;

public class ShiftQueuePassengersSystem : IEcsRunSystem
{
    private List<Passenger> _passengers;
    private SceneData _sceneData;

    public void Run()
    {
        ShiftFirstPassenger();
    }

    private void ShiftFirstPassenger()
    {
        if (_passengers.Count == 0)
            return;

        ref var passengerMovable = ref _passengers[0].Entity.Get<PassengerMovableComponent>();
        ref var passengerComponent = ref _passengers[0].Entity.Get<PassengerComponent>();

        if (passengerMovable.isPositionStartQueuePosition == false && passengerMovable.isMoving == false)
        {
            _passengers[0].Entity.Get<PassengerMoveStartQueuePointEvent>();
            ShiftQueue();
        }
    }

    private void ShiftQueue()
    {
        if (_passengers.Count <= 1)
            return;

        for (int j = 1; j < _passengers.Count; j++)
        {
            ref var previousPassengerMovable = ref _passengers[j - 1].Entity.Get<PassengerMovableComponent>();
            ref var previousPassengerComponent = ref _passengers[j - 1].Entity.Get<PassengerComponent>();

            if (previousPassengerMovable.isPositionStartQueuePosition == true)
                continue;

            if (previousPassengerMovable.queuePointPosition != _sceneData.QueuePositions[_sceneData.QueuePositions.Count - 1].position)
                previousPassengerComponent.passenger.gameObject.SetActive(true);

            StartMoveQueuePointEvent(_passengers[j], previousPassengerMovable);
        }
    }

    private void StartMoveQueuePointEvent(Passenger passenger, PassengerMovableComponent previousPassengerMovable)
    {
        passenger.Entity.Get<PassengerMoveInQueuePointEvent>() = new PassengerMoveInQueuePointEvent
        {
            queuePointPosition = previousPassengerMovable.currentTransform.position
        };
    }
}

using Leopotam.Ecs;
using System.Collections.Generic;

public class ShiftQueuePassengersSystem : IEcsRunSystem
{
    private List<Passenger> _passengers;

    public ShiftQueuePassengersSystem(List<Passenger> passengers)
    {
        _passengers = passengers;
    }

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

            if (previousPassengerMovable.isPositionStartQueuePosition == true)
                continue;

            StartMoveQueuePointEvent(_passengers[j], previousPassengerMovable);
        }
    }

    private void StartMoveQueuePointEvent(Passenger passenger, PassengerMovableComponent previousPassengerMovable)
    {
        passenger.Entity.Get<PassengerMoveQueuePointEvent>() = new PassengerMoveQueuePointEvent
        {
            queuePointPosition = previousPassengerMovable.currentTransform.position
        };
    }
}

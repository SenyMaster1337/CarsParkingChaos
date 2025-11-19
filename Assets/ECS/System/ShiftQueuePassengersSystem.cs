using Leopotam.Ecs;
using System.Collections.Generic;

public class ShiftQueuePassengersSystem : IEcsRunSystem
{
    private List<Passenger> _passengers;
    private StartQueuePoint _startQueuePoint;

    public ShiftQueuePassengersSystem(List<Passenger> passengers, StartQueuePoint startQueuePoint)
    {
        _passengers = passengers;
        _startQueuePoint = startQueuePoint;
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

        if (passengerMovable.isPositionStartQueuePosition == false && passengerMovable.isMoving == false)
        {
            passengerMovable.startQueuePosition = _startQueuePoint.transform.position;
            passengerMovable.isMoving = true;
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

            ref var passengerMovable = ref _passengers[j].Entity.Get<PassengerMovableComponent>();
            passengerMovable.isMoving = true;
            passengerMovable.isNeedShiftQueue = true;
            passengerMovable.currentQueuePointPosition = previousPassengerMovable.currentTransform.position;
        }
    }
}

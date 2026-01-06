using System.Collections.Generic;
using Leopotam.Ecs;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class ShiftQueuePassengersSystem : IEcsRunSystem
    {
        private List<Passenger> _passengers;
        private SceneData _sceneData;

        public void Run()
        {
            MoveFirstPassenger();
        }

        private void MoveFirstPassenger()
        {
            if (_passengers.Count == 0)
                return;

            ref var passengerMovable = ref _passengers[0].Entity.Get<PassengerMovableComponent>();
            ref var passengerComponent = ref _passengers[0].Entity.Get<PassengerComponent>();

            if (passengerMovable.IsPositionStartQueuePosition == false && passengerMovable.IsMoving == false)
            {
                _passengers[0].Entity.Get<PassengerMoveStartQueuePointEvent>();
                MoveQueue();
            }
        }

        private void MoveQueue()
        {
            if (_passengers.Count <= 1)
                return;

            for (int j = 1; j < _passengers.Count; j++)
            {
                ref var previousPassengerMovable = ref _passengers[j - 1].Entity.Get<PassengerMovableComponent>();
                ref var previousPassengerComponent = ref _passengers[j - 1].Entity.Get<PassengerComponent>();

                if (previousPassengerMovable.IsPositionStartQueuePosition == true)
                    continue;

                if (previousPassengerMovable.QueuePointPosition != _sceneData.QueuePositions[^1].position)
                    previousPassengerComponent.Passenger.gameObject.SetActive(true);

                _passengers[j].Entity.Get<PassengerMoveInQueuePointEvent>() = new PassengerMoveInQueuePointEvent
                {
                    QueuePointPosition = previousPassengerMovable.CurrentTransform.position,
                };
            }
        }
    }
}

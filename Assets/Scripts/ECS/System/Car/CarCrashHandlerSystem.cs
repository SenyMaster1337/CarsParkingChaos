using System.Collections.Generic;
using Leopotam.Ecs;
using CarParkingChaos.Handler;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Systems
{
    public class CarCrashHandlerSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsWorld _ecsWorld;
        private List<Vehicle> _cars;

        public CarCrashHandlerSystem(List<Vehicle> cars)
        {
            _cars = cars;
        }

        public void Init()
        {
            for (int i = 0; i < _cars.Count; i++)
            {
                _cars[i].GetComponentInChildren<CrashHandler>().OnCollisionCar += ProcessCrash;
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < _cars.Count; i++)
            {
                if (_cars[i] != null)
                    _cars[i].GetComponentInChildren<CrashHandler>().OnCollisionCar -= ProcessCrash;
            }
        }

        private void ProcessCrash(Vehicle crashHandlerCar, Vehicle carCrashed)
        {
            ref var componentcrashHandlerCar = ref crashHandlerCar.Entity.Get<CarComponent>();
            ref var componentCarCrashed = ref carCrashed.Entity.Get<CarComponent>();

            if (componentCarCrashed.CanCrashed == true && componentcrashHandlerCar.CanCrashed == true)
            {
                componentcrashHandlerCar.IsCrashed = true;

                StartCancelParkingReserverEvent(componentcrashHandlerCar.ParkingReservedSlot);

                ref var movableCrashHandlerCar = ref crashHandlerCar.Entity.Get<CarMovableComponent>();
                movableCrashHandlerCar.IsReverseDirectionEnable = true;
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
}

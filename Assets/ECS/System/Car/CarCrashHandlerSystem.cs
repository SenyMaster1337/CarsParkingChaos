using Leopotam.Ecs;
using System.Collections.Generic;

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

        if (componentCarCrashed.canCrashed == true && componentcrashHandlerCar.canCrashed == true)
        {
            componentcrashHandlerCar.isCrashed = true;

            StartCancelParkingReserverEvent(componentcrashHandlerCar.parkingReservedSlot);

            ref var movableCrashHandlerCar = ref crashHandlerCar.Entity.Get<CarMovableComponent>();
            movableCrashHandlerCar.isReverseDirectionEnable = true;
        }
    }

    private void StartCancelParkingReserverEvent(ParkingSlot slot)
    {
        _ecsWorld.NewEntity().Get<ParkingCancelReservationEvent>() = new ParkingCancelReservationEvent
        {
            parkingSlot = slot
        };
    }
}

using Leopotam.Ecs;
using System.Collections.Generic;

public class CarCrashHandlerSystem : IEcsInitSystem, IEcsDestroySystem
{
    private EcsWorld _ecsWorld;
    private List<Vehicle> _crashHandler;

    public CarCrashHandlerSystem(List<Vehicle> collisionHandler)
    {
        _crashHandler = collisionHandler;
    }

    public void Init()
    {
        for (int i = 0; i < _crashHandler.Count; i++)
        {
            _crashHandler[i].GetComponentInChildren<CrashHandler>().OnCollisionCar += ComeBack;
        }
    }

    public void Destroy()
    {
        for (int i = 0; i < _crashHandler.Count; i++)
        {
            if (_crashHandler[i] != null)
                _crashHandler[i].GetComponentInChildren<CrashHandler>().OnCollisionCar -= ComeBack;
        }
    }

    private void ComeBack(Vehicle crashHandlerCar, Vehicle carCrashed)
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

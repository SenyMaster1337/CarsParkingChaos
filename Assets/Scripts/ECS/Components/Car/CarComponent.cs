using System.Collections.Generic;
using UnityEngine;
using CarParkingChaos.Handler;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Components
{
    public struct CarComponent
    {
        public Vehicle Car;
        public CrashHandler CrashHandler;
        public Renderer Renderer;
        public int MaxPassengersSlots;
        public List<PassengerComponent> Passengers;
        public List<PassengerComponent> ReservedSeats;
        public bool IsNotEmptySeats;
        public bool IsAllPassengersBoarded;
        public bool IsParked;
        public bool CanClickable;
        public bool CanCrashed;
        public bool IsCrashed;
        public float DistanceToDisableCrashHandler;
        public Quaternion RorationCarInParking;
        public ParkingSlot ParkingReservedSlot;
    }
}

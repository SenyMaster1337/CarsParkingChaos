using System.Collections.Generic;
using UnityEngine;

public struct CarComponent
{
    public Vehicle car;
    public ParkingSlot parkingReservedSlot;
    public CrashHandler crashHandler;

    public Renderer renderer;

    public int maxPassengersSlots;
    public List<PassengerComponent> passengers;
    public List<PassengerComponent> reservedSeats;

    public bool isNotEmptySeats;
    public bool isAllPassengersBoarded;

    public bool isParked;
    public bool isCrashHandlerEnabled;
    public bool canClickable;
    public bool canCrashed;
    public bool isCrashed;

    public Quaternion rorationCarInParking;
    public float distanceToDisableCrashHandler;
}

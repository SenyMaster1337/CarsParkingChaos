using System.Collections.Generic;
using CarParkingChaos.Markers;

public struct GetUnitsDataEvent
{
    public List<Vehicle> AllCarsInLevel;
    public List<Vehicle> CarsOnlyParkingZoneList;
    public List<Passenger> AllPassengersInLevel;
}
using System.Collections.Generic;

public struct GetUnitsDataEvent
{
    public List<Vehicle> AllCarsInLevel;
    public List<Vehicle> CarsOnlyParkingZoneList;
    public List<Passenger> AllPassengersInLevel;
}
using System.Collections.Generic;

public struct GetUnitsDataEvent
{
    public List<Vehicle> allCarsInLevel;
    public List<Vehicle> carsOnlyParkingZoneList;
    public List<Passenger> allPassengersInLevel;
}

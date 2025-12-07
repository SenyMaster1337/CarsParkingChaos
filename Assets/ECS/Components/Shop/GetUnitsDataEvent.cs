using System.Collections.Generic;

public struct GetUnitsDataEvent
{
    public List<Vehicle> allCarsInLevel;
    public List<Vehicle> carsOnlyParkingZone;
    public List<Passenger> allPassengersInLevel;
}

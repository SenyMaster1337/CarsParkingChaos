using System.Collections.Generic;

public struct GetUnitsDataEvent
{
    public List<Vehicle> allCarsInLevel;
    public List<Vehicle> carsOnlyInParking;
    public List<Passenger> allPassengersInLevel;
}

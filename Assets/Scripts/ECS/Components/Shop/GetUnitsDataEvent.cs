using System.Collections.Generic;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Components
{
    public struct GetUnitsDataEvent
    {
        public List<Vehicle> CarsOnlyParkingZoneList;
        public List<Passenger> AllPassengersInLevel;
    }
}

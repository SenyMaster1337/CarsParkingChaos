using System.Collections.Generic;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Components
{
    public struct ShuffleComponent
    {
        public List<Vehicle> Cars;
        public List<Passenger> Passengers;
    }
}

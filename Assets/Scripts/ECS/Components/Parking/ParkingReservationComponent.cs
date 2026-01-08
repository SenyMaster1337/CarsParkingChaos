using System.Collections.Generic;
using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Components
{
    public struct ParkingReservationComponent
    {
        public List<ParkingSlot> ParkingSlots;
    }
}

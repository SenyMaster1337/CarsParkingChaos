using CarParkingChaos.Markers;

namespace CarParkingChaos.ECS.Components
{
    public struct SaveParkingSlotEvent
    {
        public ParkingSlot ParkingSlot;
        public OpenADVParkingSlotUnlock OpenADVParkingSlotUnlock;
    }
}

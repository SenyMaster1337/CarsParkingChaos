using UnityEngine;

namespace CarParkingChaos.Markers
{
    public class OpenADVParkingSlotUnlock : MonoBehaviour
    {
        [field: SerializeField] public ParkingSlot ParkingSlot { get; private set; }
    }
}

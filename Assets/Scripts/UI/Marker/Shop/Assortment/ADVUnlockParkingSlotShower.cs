using UnityEngine;

public class ADVUnlockParkingSlotShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public ShowAdvUnclockParkingSlotButton ShowAdvUnclockParkingSlotButton { get; private set; }
    [field: SerializeField] public CloseAdvUnlockParkingSlotrButton CloseAdvUnlockParkingSlotrButton { get; private set; }
}

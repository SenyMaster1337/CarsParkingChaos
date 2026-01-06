using UnityEngine;

namespace CarParkingChaos.Counter
{
    public class PassengerCounter : MonoBehaviour
    {
        [field: SerializeField] public DesktopPointPassengerCounter DesktopPointPassengerCounter { get; private set; }
        [field: SerializeField] public MobilePointPassengerCounter MobilePointPassengerCounter { get; private set; }
    }
}

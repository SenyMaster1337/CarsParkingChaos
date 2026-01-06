using UnityEngine;

namespace CarParkingChaos.Sounds
{
    public class BaseSound : MonoBehaviour
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
    }
}

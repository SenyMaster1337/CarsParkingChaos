using UnityEngine;

namespace CarParkingChaos.Effects
{
    public class BaseEffect : MonoBehaviour
    {
        [field: SerializeField] public ParticleSystem ParticleSystem { get; private set; }
    }
}

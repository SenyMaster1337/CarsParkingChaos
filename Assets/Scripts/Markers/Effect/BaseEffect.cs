using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    [field: SerializeField] public ParticleSystem ParticleSystem { get; private set; }
}

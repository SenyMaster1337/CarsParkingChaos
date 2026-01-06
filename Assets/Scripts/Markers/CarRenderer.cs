using UnityEngine;

namespace CarParkingChaos.Markers
{
    public class CarRenderer : MonoBehaviour
    {
        [field: SerializeField] public Renderer Renderer { get; private set; }
    }
}

using UnityEngine;
using CarParkingChaos.UI.Text;

namespace CarParkingChaos.UI.Markers
{
    public class LevelCurrentShower : MonoBehaviour
    {
        [field: SerializeField] public CurrentLevelNumberText CurrentLevelNumberText { get; private set; }
    }
}

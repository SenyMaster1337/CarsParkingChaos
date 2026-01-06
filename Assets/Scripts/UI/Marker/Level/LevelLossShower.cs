using UnityEngine;
using CarParkingChaos.UI.Buttons;

namespace CarParkingChaos.UI.Markers
{
    public class LevelLossShower : MonoBehaviour
    {
        [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
        [field: SerializeField] public RestartButtonClickReader RestartButtonClickReader { get; private set; }
        [field: SerializeField] public BlackBackground BlackBackground { get; private set; }
    }
}

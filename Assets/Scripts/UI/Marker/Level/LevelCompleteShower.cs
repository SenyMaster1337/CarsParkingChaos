using UnityEngine;
using CarParkingChaos.UI.Buttons;
using CarParkingChaos.UI.Text;

namespace CarParkingChaos.UI.Markers
{
    public class LevelCompleteShower : MonoBehaviour
    {
        [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
        [field: SerializeField] public NextLevelButtonClickReader NextLevelButtonClickReader { get; private set; }
        [field: SerializeField] public CoinsNumberToWinText CoinsNumberToWinText { get; private set; }
        [field: SerializeField] public BlackBackground BlackBackground { get; private set; }
    }
}

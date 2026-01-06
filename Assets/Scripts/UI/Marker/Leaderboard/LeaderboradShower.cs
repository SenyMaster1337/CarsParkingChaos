using UnityEngine;
using CarParkingChaos.UI.Buttons;

namespace CarParkingChaos.UI.Markers
{
    public class LeaderboradShower : MonoBehaviour
    {
        [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
        [field: SerializeField] public LeaderboradOpenButton LeaderboradOpenButtonClick { get; private set; }
        [field: SerializeField] public LeaderboardCloseButton LeaderboradCloseButtonClick { get; private set; }
    }
}

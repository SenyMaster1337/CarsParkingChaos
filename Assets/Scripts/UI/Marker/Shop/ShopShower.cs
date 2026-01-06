using UnityEngine;
using CarParkingChaos.UI.Buttons;

namespace CarParkingChaos.UI.Markers
{
    public class ShopShower : MonoBehaviour
    {
        [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
        [field: SerializeField] public OpenShopButton OpenShopButtonClickReader { get; private set; }
        [field: SerializeField] public CloseShopButton CloseShopButtonClickReader { get; private set; }
        [field: SerializeField] public PassengerSortingShower BuyPassengerSortingShower { get; private set; }
        [field: SerializeField] public CarShuffleShower BuyPassengerShuffleShower { get; private set; }
        [field: SerializeField] public NotCarsToSortingWindow NotCarsToSortingWindow { get; private set; }
        [field: SerializeField] public NotEnoughCarsToShuffle NotEnoughCarsToShuffleWindow { get; private set; }
        [field: SerializeField] public NotEnoughMoneyWindow NotEnoughMoneyWindow { get; private set; }
    }
}

using UnityEngine;
using CarParkingChaos.UI.Buttons;
using CarParkingChaos.UI.Text;

namespace CarParkingChaos.UI.Markers
{
    public class CarShuffleShower : MonoBehaviour
    {
        [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
        [field: SerializeField] public PriceBuyingCarShuffleText PriceBuyingCarShuffleText { get; private set; }
        [field: SerializeField] public PriceBuyingCarShuffleShopAsssortmentMenuText PriceBuyingCarShuffleShopAsssortmentMenuText { get; private set; }
        [field: SerializeField] public OpenBuyingWindowCarShuffleButton OpenBuyingCarShuffleButton { get; private set; }
        [field: SerializeField] public BuyCarShuffleButton AcceptBuyingCarShuffleButton { get; private set; }
        [field: SerializeField] public RewardShuffleButton RewardShuffleButton { get; private set; }
        [field: SerializeField] public CloseBuyingWindowCarShuffleButton DeclineBuyingCarShuffleButton { get; private set; }
    }
}

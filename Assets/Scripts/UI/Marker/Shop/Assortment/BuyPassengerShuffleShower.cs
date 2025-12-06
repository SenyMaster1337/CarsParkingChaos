using UnityEngine;

public class BuyPassengerShuffleShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerShuffleText PriceBuyingPassengerShuffleText { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerShuffleShopAsssortmentMenuText PriceBuyingPassengerShuffleShopAsssortmentMenuText { get; private set; }
    [field: SerializeField] public OpenBuyingPassengerShuffleButton OpenBuyingPassengerMixerButtonClickReader { get; private set; }
    [field: SerializeField] public AcceptBuyingPassengerShuffleButton AcceptBuyingPassengerMixerButtonClickReader { get; private set; }
    [field: SerializeField] public DeclineBuyingPassengerShuffleButton DeclineBuyingPassengerMixerButtonClickReader { get; private set; }
}

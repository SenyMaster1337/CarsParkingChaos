using UnityEngine;

public class BuyPassengerMixerShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerMixerText PriceBuyingPassengerMixerText { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerMixerShopAsssortmentMenuText PriceBuyingPassengerMixerShopAsssortmentMenuText { get; private set; }
    [field: SerializeField] public OpenBuyingPassengerMixerButtonClickReader OpenBuyingPassengerMixerButtonClickReader { get; private set; }
    [field: SerializeField] public AcceptBuyingPassengerMixerButtonClickReader AcceptBuyingPassengerMixerButtonClickReader { get; private set; }
    [field: SerializeField] public DeclineBuyingPassengerMixerButtonClickReader DeclineBuyingPassengerMixerButtonClickReader { get; private set; }
}

using UnityEngine;

public class BuyCarMixerShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public PriceBuyingCarMixerText PriceBuyingCarMixerText { get; private set; }
    [field: SerializeField] public PriceBuyingCarMixerShopAsssortmentMenuText PriceBuyingCarMixerShopAsssortmentMenuText { get; private set; }
    [field: SerializeField] public OpenMenuBuyingCarMixerButton OpenMenuBuyingCarMixerButton { get; private set; }
    [field: SerializeField] public BuyCarMixerButton BuyCarMixerButton { get; private set; }
    [field: SerializeField] public CloseMenuBuyingCarMixerButton CloseMenuBuyingCarMixerButton { get; private set; }
}

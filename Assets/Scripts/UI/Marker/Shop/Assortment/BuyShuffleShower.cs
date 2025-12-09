using UnityEngine;

public class BuyShuffleShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public PriceBuyingCarShuffleText PriceBuyingCarShuffleText { get; private set; }
    [field: SerializeField] public PriceBuyingCarShuffleShopAsssortmentMenuText PriceBuyingCarShuffleShopAsssortmentMenuText { get; private set; }
    [field: SerializeField] public OpenBuyingCarShuffleButton OpenBuyingCarShuffleButton { get; private set; }
    [field: SerializeField] public AcceptBuyingCarShuffleButton AcceptBuyingCarShuffleButton { get; private set; }
    [field: SerializeField] public RewardShuffleButton RewardShuffleButton { get; private set; }
    [field: SerializeField] public DeclineBuyingCarShuffleButton DeclineBuyingCarShuffleButton { get; private set; }
}

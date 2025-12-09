using UnityEngine;

public class BuyPassengerSortingShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerSortingText PriceBuyingPassengerSortingText { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerSortingShopAsssortmentMenuText PriceBuyingPassengerSortingShopAsssortmentMenuText { get; private set; }
    [field: SerializeField] public OpenBuyingPassengerSortingButtonClickReader OpenBuyingPassengerSortingButton { get; private set; }
    [field: SerializeField] public AcceptBuyingPassengerSortingButtonClickReader AcceptBuyingPassengersSortingButton { get; private set; }
    [field: SerializeField] public RewardPassengerSortingButton RewardPassengerSortingButton { get; private set; }
    [field: SerializeField] public DeclineBuyingPassengeSortingButtonClickReader DeclineBuyingPassengerSortingButton { get; private set; }
}

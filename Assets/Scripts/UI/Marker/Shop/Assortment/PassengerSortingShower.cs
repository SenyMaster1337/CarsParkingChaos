using UnityEngine;

public class PassengerSortingShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerSortingText PriceBuyingPassengerSortingText { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerSortingShopAsssortmentMenuText PriceBuyingPassengerSortingShopAsssortmentMenuText { get; private set; }
    [field: SerializeField] public OpenBuyingWindowPassengerSortingButton OpenBuyingPassengerSortingButton { get; private set; }
    [field: SerializeField] public BuyPassengerSortingButton AcceptBuyingPassengersSortingButton { get; private set; }
    [field: SerializeField] public RewardPassengerSortingButton RewardPassengerSortingButton { get; private set; }
    [field: SerializeField] public CloseBuyingWindowPassengeSortingButton DeclineBuyingPassengerSortingButton { get; private set; }
}

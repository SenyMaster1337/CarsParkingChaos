using UnityEngine;

public class BuyPassengerSortingShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerSortingText PriceBuyingPassengerSortingText { get; private set; }
    [field: SerializeField] public PriceBuyingPassengerSortingShopAsssortmentMenuText PriceBuyingPassengerSortingShopAsssortmentMenuText { get; private set; }
    [field: SerializeField] public OpenBuyingPassengerSortingButtonClickReader OpenBuyingPassengerSortingButtonClickReader { get; private set; }
    [field: SerializeField] public AcceptBuyingPassengerSortingButtonClickReader AcceptBuyingPassengersSortingButtonClickReader { get; private set; }
    [field: SerializeField] public DeclineBuyingPassengeSortingButtonClickReader DeclineBuyingPassengerSortingButtonClickReader { get; private set; }
}

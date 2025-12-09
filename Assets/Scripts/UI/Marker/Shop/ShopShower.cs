using UnityEngine;

public class ShopShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public OpenShopButton OpenShopButtonClickReader { get; private set; }
    [field: SerializeField] public CloseShopButton CloseShopButtonClickReader { get; private set; }
    [field: SerializeField] public BuyPassengerSortingShower BuyPassengerSortingShower { get; private set; }
    [field: SerializeField] public BuyShuffleShower BuyPassengerShuffleShower { get; private set; }
}

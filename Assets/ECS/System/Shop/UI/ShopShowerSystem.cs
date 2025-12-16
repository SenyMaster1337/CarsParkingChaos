using Leopotam.Ecs;
using UnityEngine;
using DG.Tweening;

public class ShopShowerSystem : IEcsRunSystem
{
    private EcsFilter<ShopShowerComponent> _shopFilter;
    private EcsFilter<OpenShopEvent> _openFilter;
    private EcsFilter<CloseShopEvent> _closeFilter;
    private EcsFilter<ShowNotCarsToSortingWindowEvent> _showNotCarsFilter;
    private EcsFilter<ShowNotEnoughMoneyWindowEvent> _showNotEnoughMoneyFilter;
    private EcsFilter<ShowNotEnoughCarsToShuffleEvent> _showNotEnoughCarsFilter;

    public void Run()
    {
        foreach (var shopEntity in _shopFilter)
        {
            ref var shopShowerComponent = ref _shopFilter.Get1(shopEntity);

            foreach (var openEntity in _openFilter)
            {
                var openEvent = _openFilter.GetEntity(openEntity);
                OpenShop(shopShowerComponent);
                openEvent.Del<OpenShopEvent>();
            }

            foreach (var closeEntity in _closeFilter)
            {
                var closeEvent = _closeFilter.GetEntity(closeEntity);
                CloseShop(shopShowerComponent);
                closeEvent.Del<CloseShopEvent>();
            }

            foreach (var notCarsEntity in _showNotCarsFilter)
            {
                ShowErrorInfo(shopShowerComponent.ShopShower.NotCarsToSortingWindow.WindowGroup);
                _showNotCarsFilter.GetEntity(notCarsEntity).Del<ShowNotCarsToSortingWindowEvent>();
            }

            foreach (var notEnoughMoneyEntity in _showNotEnoughMoneyFilter)
            {
                ShowErrorInfo(shopShowerComponent.ShopShower.NotEnoughMoneyWindow.WindowGroup);
                _showNotEnoughMoneyFilter.GetEntity(notEnoughMoneyEntity).Del<ShowNotEnoughMoneyWindowEvent>();
            }

            foreach (var notEnoughCarsEntity in _showNotEnoughCarsFilter)
            {
                ShowErrorInfo(shopShowerComponent.ShopShower.NotEnoughCarsToShuffleWindow.WindowGroup);
                _showNotEnoughCarsFilter.GetEntity(notEnoughCarsEntity).Del<ShowNotEnoughCarsToShuffleEvent>();
            }
        }
    }

    private void ShowErrorInfo(CanvasGroup windowGroup)
    {
        windowGroup.DOKill();
        windowGroup.alpha = 1.0f;
        windowGroup.DOFade(0f, 7f);
    }

    private void OpenShop(ShopShowerComponent shopShowerComponent)
    {
        shopShowerComponent.ShopShower.WindowGroup.alpha = 1.0f;
        shopShowerComponent.ShopShower.WindowGroup.interactable = true;
        shopShowerComponent.ShopShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseShop(ShopShowerComponent shopShowerComponent)
    {
        shopShowerComponent.ShopShower.WindowGroup.alpha = 0f;
        shopShowerComponent.ShopShower.WindowGroup.interactable = false;
        shopShowerComponent.ShopShower.WindowGroup.blocksRaycasts = false;
    }
}

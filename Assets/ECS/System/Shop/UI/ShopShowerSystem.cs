using Leopotam.Ecs;
using UnityEngine;

public class ShopShowerSystem : IEcsRunSystem
{
    private EcsFilter<ShopShowerComponent> _shopFilter;
    private EcsFilter<OpenShopEvent> _openFilter;
    private EcsFilter<CloseShopEvent> _closeFilter;

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
        }
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

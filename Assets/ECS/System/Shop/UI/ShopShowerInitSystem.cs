using Leopotam.Ecs;

public class ShopShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private ShopShower _shopShower;

    public ShopShowerInitSystem(ShopShower shopShower)
    {
        _shopShower = shopShower;
    }

    public void Init()
    {
        var shopShowerNewEntity = _ecsWorld.NewEntity();

        ref var shopShowerComponent = ref shopShowerNewEntity.Get<ShopShowerComponent>();
        shopShowerComponent.ShopShower = _shopShower;

        shopShowerComponent.ShopShower.WindowGroup.alpha = 0f;
        shopShowerComponent.ShopShower.WindowGroup.interactable = false;
        shopShowerComponent.ShopShower.WindowGroup.blocksRaycasts = false;
    }
}

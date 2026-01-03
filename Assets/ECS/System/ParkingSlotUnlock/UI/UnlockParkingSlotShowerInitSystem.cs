using Leopotam.Ecs;

public class UnlockParkingSlotShowerInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private ADVUnlockParkingSlotShower _advUnlockParkingSlotShower;

    public UnlockParkingSlotShowerInitSystem(ADVUnlockParkingSlotShower advUnlockParkingSlotShower)
    {
        _advUnlockParkingSlotShower = advUnlockParkingSlotShower;
    }

    public void Init()
    {
        var advUnlockParkingSlotNewEntity = _ecsWorld.NewEntity();

        ref var unlockParkingSlotShowerComponent = ref advUnlockParkingSlotNewEntity.Get<ADVUnlockParkingSlotShowerComponent>();
        unlockParkingSlotShowerComponent.advUnlockParkingSlotShower = _advUnlockParkingSlotShower;

        unlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.alpha = 0f;
        unlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.interactable = false;
        unlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.blocksRaycasts = false;
    }
}

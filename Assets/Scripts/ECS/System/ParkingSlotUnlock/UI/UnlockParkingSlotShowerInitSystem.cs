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
        unlockParkingSlotShowerComponent.AdvUnlockParkingSlotShower = _advUnlockParkingSlotShower;

        unlockParkingSlotShowerComponent.AdvUnlockParkingSlotShower.WindowGroup.alpha = 0f;
        unlockParkingSlotShowerComponent.AdvUnlockParkingSlotShower.WindowGroup.interactable = false;
        unlockParkingSlotShowerComponent.AdvUnlockParkingSlotShower.WindowGroup.blocksRaycasts = false;
    }
}

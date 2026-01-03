using Leopotam.Ecs;

public class UnlockParkingSlotShowerSystem : IEcsRunSystem
{
    private EcsFilter<ADVUnlockParkingSlotShowerComponent> _unlockFilter;
    private EcsFilter<OpenADVUnlockParkingSlotEvent> _openFilter;
    private EcsFilter<CloseADVUnlockParkingSlotEvent> _closeFilter;

    public void Run()
    {
        foreach (var sortingEntity in _unlockFilter)
        {
            ref var sortingShowerComponent = ref _unlockFilter.Get1(sortingEntity);

            foreach (var openEntity in _openFilter)
            {
                var openEvent = _openFilter.GetEntity(openEntity);
                OpenUnlockShower(sortingShowerComponent);
                openEvent.Del<OpenADVUnlockParkingSlotEvent>();
            }

            foreach (var closeEntity in _closeFilter)
            {
                var closeEvent = _closeFilter.GetEntity(closeEntity);
                CloseUnlockShower(sortingShowerComponent);
                closeEvent.Del<CloseADVUnlockParkingSlotEvent>();
            }
        }
    }

    private void OpenUnlockShower(ADVUnlockParkingSlotShowerComponent advUnlockParkingSlotShowerComponent)
    {
        advUnlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.alpha = 1.0f;
        advUnlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.interactable = true;
        advUnlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseUnlockShower(ADVUnlockParkingSlotShowerComponent advUnlockParkingSlotShowerComponent)
    {
        advUnlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.alpha = 0f;
        advUnlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.interactable = false;
        advUnlockParkingSlotShowerComponent.advUnlockParkingSlotShower.WindowGroup.blocksRaycasts = false;
    }
}

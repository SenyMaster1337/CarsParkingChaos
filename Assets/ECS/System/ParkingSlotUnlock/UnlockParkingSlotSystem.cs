using Leopotam.Ecs;
using UnityEngine;
using YG;

public class UnlockParkingSlotSystem : IEcsRunSystem
{
    public string RewardID = "UnlockParkingSlotRewardID";

    private EcsWorld _ecsWorld;
    private EcsFilter<SaveParkingSlotEvent> _saveFilter;
    private EcsFilter<ShowADVToUnlockParkingSlotEvent> _showAdvFilter;
    private EcsFilter<LevelComponent> _levelFilter;

    private ParkingSlot _parkingSlot;
    private OpenADVParkingSlotUnlock _openADVParkingSlotUnlock;

    public void Run()
    {
        foreach (var saveEntity in _saveFilter)
        {
            var saveEventEntity = _saveFilter.GetEntity(saveEntity);
            SaveParkingSlot(saveEventEntity);
            saveEventEntity.Del<SaveParkingSlotEvent>();
        }

        foreach (var showAdvEntity in _showAdvFilter)
        {
            foreach (var levelEntity in _levelFilter)
            {
                ref var levelComponent = ref _levelFilter.Get1(levelEntity);
                int currentLevel = levelComponent.currentLevel;

                YG2.RewardedAdvShow(RewardID, () =>
                {
                    if (RewardID == "UnlockParkingSlotRewardID")
                    {
                        StartAddParkingSlotEvent();
                        StartYGSaveParkingSlotsEvent(currentLevel);
                    }
                });

                _showAdvFilter.GetEntity(showAdvEntity).Del<ShowADVToUnlockParkingSlotEvent>();
            }
        }
    }

    private void SaveParkingSlot(EcsEntity saveEventEntity)
    {
        ref var saveParkingSlotEvent = ref saveEventEntity.Get<SaveParkingSlotEvent>();
        _parkingSlot = saveParkingSlotEvent.parkingSlot;
        _openADVParkingSlotUnlock = saveParkingSlotEvent.openADVParkingSlotUnlock;
    }

    private void StartAddParkingSlotEvent()
    {
        _ecsWorld.NewEntity().Get<AddParkingSlotEvent>() = new AddParkingSlotEvent
        {
            parkingSlot = _parkingSlot
        };

        _openADVParkingSlotUnlock.gameObject.SetActive(false);
    }

    private void StartYGSaveParkingSlotsEvent(int currentLevel)
    {
        _ecsWorld.NewEntity().Get<YGSaveRewardParkingSlotsEvent>();
        _ecsWorld.NewEntity().Get<YGSaveProgressEvent>();
    }
}

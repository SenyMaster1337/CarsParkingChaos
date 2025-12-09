using Leopotam.Ecs;
using UnityEngine;
using YG;

public class UnlockParkingSlotSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
    private EcsFilter<SaveParkingSlotEvent> _saveFilter;
    private EcsFilter<ShowADVToUnlockParkingSlotEvent> _showAdvFilter;

    public string rewardID;
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
            YG2.RewardedAdvShow(rewardID, () =>
            {
                _ecsWorld.NewEntity().Get<AddParkingSlotEvent>() = new AddParkingSlotEvent
                {
                    parkingSlot = _parkingSlot
                };

                _openADVParkingSlotUnlock.gameObject.SetActive(false);
                _showAdvFilter.GetEntity(showAdvEntity).Del<ShowADVToUnlockParkingSlotEvent>();
            });
        }
    }

    private void SaveParkingSlot(EcsEntity saveEventEntity)
    {
        ref var saveParkingSlotEvent = ref saveEventEntity.Get<SaveParkingSlotEvent>();
        _parkingSlot = saveParkingSlotEvent.parkingSlot;
        _openADVParkingSlotUnlock = saveParkingSlotEvent.openADVParkingSlotUnlock;
    }
}

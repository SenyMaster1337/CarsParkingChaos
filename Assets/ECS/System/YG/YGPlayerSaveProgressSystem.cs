using Leopotam.Ecs;
using YG;
using UnityEngine;
using System.Collections.Generic;

public class YGPlayerSaveProgressSystem : IEcsRunSystem
{
    private EcsFilter<YGSaveEnityComponentsEvent> _saveEnityComponentsFilter;
    private EcsFilter<YGSaveRewardParkingSlotsEvent> _saveParkingSlotsFilter;
    private EcsFilter<YGClearDataRewardParkingSlots> _clearParkingSlotsFilter;
    private EcsFilter<YGSaveProgressEvent> _saveProgressFilter;
    private EcsFilter<LevelComponent> _levelFilter;
    private EcsFilter<CurrencyComponent> _currecnyFilter;

    public void Run()
    {
        foreach (var levelEntity in _saveEnityComponentsFilter)
        {
            ref var saveProgressEvent = ref _saveEnityComponentsFilter.Get1(levelEntity);
            SaveEntityComponents();
            _saveEnityComponentsFilter.GetEntity(levelEntity).Del<YGSaveEnityComponentsEvent>();
        }

        foreach (var saveParkingSlotsEntity  in _saveParkingSlotsFilter)
        {
            ref var saveParkingSlotsEvent = ref _saveParkingSlotsFilter.Get1(saveParkingSlotsEntity);
            YG2.saves.additionalRewardParkingSlotsCount += 1;
            _saveParkingSlotsFilter.GetEntity(saveParkingSlotsEntity).Del<YGSaveRewardParkingSlotsEvent>();
        }

        foreach(var clearDataParkingSlotsEntity in _clearParkingSlotsFilter)
        {
            ref var clearParkingSlotsEvent = ref _clearParkingSlotsFilter.Get1(clearDataParkingSlotsEntity);
            YG2.saves.additionalRewardParkingSlotsCount = 0;
            _clearParkingSlotsFilter.GetEntity(clearDataParkingSlotsEntity).Del<YGClearDataRewardParkingSlots>();
        }

        foreach(var saveProgressEntity in _saveProgressFilter)
        {
            YG2.SaveProgress();
            _saveProgressFilter.GetEntity(saveProgressEntity).Del<YGSaveProgressEvent>();
        }
    }

    private void SaveEntityComponents()
    {
        foreach (var levelEntity in _levelFilter)
        {
            ref var levelComponent = ref _levelFilter.Get1(levelEntity);
            int newLevel = levelComponent.currentLevel;
            YG2.saves.level = newLevel;
        }

        foreach (var currencyEntity in _currecnyFilter)
        {
            ref var currencyComponent = ref _currecnyFilter.Get1(currencyEntity);
            int newCoins = currencyComponent.playerCoins;
            YG2.saves.coins = newCoins;
        }
    }
}

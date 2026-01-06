using Leopotam.Ecs;
using YG;

public class YGPlayerSaveProgressSystem : IEcsRunSystem
{
    private EcsFilter<YGSavePlayerCoinsCountEvent> _saveCoinsFilter;
    private EcsFilter<YGSavePlayerLevelEvent> _saveLevelFilter;
    private EcsFilter<YGSaveRewardParkingSlotsEvent> _saveParkingSlotsFilter;
    private EcsFilter<YGClearDataRewardParkingSlots> _clearParkingSlotsFilter;
    private EcsFilter<YGSaveProgressEvent> _saveProgressFilter;
    private EcsFilter<LevelComponent> _levelFilter;
    private EcsFilter<CurrencyComponent> _currecnyFilter;

    public void Run()
    {
        foreach (var coinsEntity in _saveCoinsFilter)
        {
            ref var saveCoinsEvent = ref _saveCoinsFilter.Get1(coinsEntity);
            SaveCoins();
            _saveCoinsFilter.GetEntity(coinsEntity).Del<YGSavePlayerCoinsCountEvent>();
        }

        foreach (var levelEntity in _saveLevelFilter)
        {
            ref var saveLevelEvent = ref _saveLevelFilter.Get1(levelEntity);
            SaveLevel();
            _saveLevelFilter.GetEntity(levelEntity).Del<YGSavePlayerLevelEvent>();
        }

        foreach (var saveParkingSlotsEntity in _saveParkingSlotsFilter)
        {
            ref var saveParkingSlotsEvent = ref _saveParkingSlotsFilter.Get1(saveParkingSlotsEntity);
            YG2.saves.AdditionalRewardParkingSlotsCount += 1;
            _saveParkingSlotsFilter.GetEntity(saveParkingSlotsEntity).Del<YGSaveRewardParkingSlotsEvent>();
        }

        foreach (var clearDataParkingSlotsEntity in _clearParkingSlotsFilter)
        {
            ref var clearParkingSlotsEvent = ref _clearParkingSlotsFilter.Get1(clearDataParkingSlotsEntity);
            YG2.saves.AdditionalRewardParkingSlotsCount = 0;
            _clearParkingSlotsFilter.GetEntity(clearDataParkingSlotsEntity).Del<YGClearDataRewardParkingSlots>();
        }

        foreach (var saveProgressEntity in _saveProgressFilter)
        {
            YG2.SaveProgress();
            _saveProgressFilter.GetEntity(saveProgressEntity).Del<YGSaveProgressEvent>();
        }
    }

    private void SaveCoins()
    {
        foreach (var currencyEntity in _currecnyFilter)
        {
            ref var currencyComponent = ref _currecnyFilter.Get1(currencyEntity);
            int newCoins = currencyComponent.PlayerCoins;
            YG2.saves.Coins = newCoins;
        }
    }

    private void SaveLevel()
    {
        foreach (var levelEntity in _levelFilter)
        {
            ref var levelComponent = ref _levelFilter.Get1(levelEntity);
            int newLevel = levelComponent.CurrentLevel;
            YG2.saves.Level = newLevel;
        }
    }
}

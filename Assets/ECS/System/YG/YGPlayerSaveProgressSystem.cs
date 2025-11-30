using Leopotam.Ecs;
using YG;
using UnityEngine;

public class YGPlayerSaveProgressSystem : IEcsRunSystem
{
    private EcsFilter<YGSaveProgressEvent> _saveProgress;
    private EcsFilter<LevelComponent> _levelFilter;
    private EcsFilter<CurrencyComponent> _currecnyFilter;

    public void Run()
    {
        foreach (var levelEntity in _saveProgress)
        {
            ref var saveProgressEvent = ref _saveProgress.Get1(levelEntity);
            SaveEntityComponents();
            _saveProgress.GetEntity(levelEntity).Del<YGSaveProgressEvent>();
        }
    }

    private void SaveEntityComponents()
    {
        foreach (var levelEntity in _levelFilter)
        {
            ref var levelComponent = ref _levelFilter.Get1(levelEntity);
            int newLevel = levelComponent.currentLevel;
            Debug.Log(levelComponent.currentLevel);
            YG2.saves.level = newLevel;
        }

        foreach (var currencyEntity in _currecnyFilter)
        {
            ref var currencyComponent = ref _currecnyFilter.Get1(currencyEntity);
            int newCoins = currencyComponent.playerCoins;
            YG2.saves.coins = newCoins;
        }

        YG2.SaveProgress();
    }
}

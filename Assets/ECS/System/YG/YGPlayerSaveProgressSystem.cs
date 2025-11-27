using Leopotam.Ecs;
using UnityEngine;
using YG;

public class YGPlayerSaveProgressSystem : IEcsRunSystem
{
    private EcsFilter<YGSaveProgressEvent> _saveProgress;

    public void Run()
    {
        foreach (var levelEntity in _saveProgress)
        {
            ref var saveProgressEvent = ref _saveProgress.Get1(levelEntity);
            YG2.saves.level = saveProgressEvent.levelIndex + 1;
            YG2.saves.coins += saveProgressEvent.coinsWinner;
            YG2.SaveProgress();
            Debug.Log($"хцпнбни опнцпеяя янупюмем: спнбемэ: {YG2.saves.level} /// лнмерш: {YG2.saves.coins}");

            _saveProgress.GetEntity(levelEntity).Del<YGSaveProgressEvent>();
        }
    }
}

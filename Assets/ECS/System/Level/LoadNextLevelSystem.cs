using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelSystem : IEcsRunSystem
{
    private EcsFilter<LevelComponent> _filter;
    private EcsFilter<LoadNextLevelEvent> _nextLevelEvent;

    public void Run()
    {
        foreach (var nextLevelEntity in _nextLevelEvent)
        {
            var eventNextLevelEntity = _nextLevelEvent.GetEntity(nextLevelEntity);
            LoadLevel();
            eventNextLevelEntity.Del<LoadNextLevelEvent>();
        }
    }

    private void LoadLevel()
    {
        foreach (var entity in _filter)
        {
            ref var levelComponent = ref _filter.Get1(entity);

            //UnityEngine.SceneManagement.SceneManager.LoadScene(levelComponent.currentLevel + 1);

            int nextSceneIndex = levelComponent.currentLevel + 1;

            // Проверяем существует ли сцена с таким индексом
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
                Debug.Log($"<color=green>✅ Загружаем сцену {nextSceneIndex}</color>");
            }
            else
            {
                Debug.LogWarning($"<color=orange>⚠️ Сцены с индексом {nextSceneIndex} не существует!</color>");
                // Дополнительные действия при отсутствии сцены
            }
        }
    }
}

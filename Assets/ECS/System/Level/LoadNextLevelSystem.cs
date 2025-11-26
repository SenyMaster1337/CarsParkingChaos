using Leopotam.Ecs;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadNextLevelSystem : IEcsRunSystem
{
    private EcsWorld _ecsWorld;
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

            int nextSceneIndex = levelComponent.currentLevel + 1;

            _ecsWorld.NewEntity().Get<InterstitialAdvShowEvent>();

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(nextSceneIndex);

            Debug.Log("некст");
        }
    }
}

using Leopotam.Ecs;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadNextLevelSystem : IEcsRunSystem
{
    private readonly int _valueToNextLevelIndex = 1;
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

            int nextSceneIndex = levelComponent.currentLevel + _valueToNextLevelIndex;

            if (nextSceneIndex < 0 || nextSceneIndex > SceneManager.sceneCountInBuildSettings)
                return;

            _ecsWorld.NewEntity().Get<YGInterstitialAdvShowEvent>();

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(nextSceneIndex);
        }
    }
}

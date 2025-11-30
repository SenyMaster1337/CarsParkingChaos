using Leopotam.Ecs;
using UnityEngine.SceneManagement;

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

            if (levelComponent.currentLevel < 0 || levelComponent.currentLevel > SceneManager.sceneCountInBuildSettings)
                return;

            _ecsWorld.NewEntity().Get<YGInterstitialAdvShowEvent>();

            if (levelComponent.currentLevel < SceneManager.sceneCountInBuildSettings)
                SceneManager.LoadScene(levelComponent.currentLevel);
        }
    }
}

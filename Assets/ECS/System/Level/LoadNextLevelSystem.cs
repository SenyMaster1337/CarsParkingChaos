using Leopotam.Ecs;
using UnityEngine.SceneManagement;

public class LoadNextLevelSystem : IEcsRunSystem
{
    private const string SceneLevelName = "Level";
    private const string EndGameSceneName = "EndGame";

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

            string sceneName = SceneLevelName + levelComponent.currentLevel;

            if (SceneExists(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                SceneManager.LoadScene(EndGameSceneName);
            }
        }
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string currentSceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (currentSceneName == sceneName)
            {
                return true;
            }
        }

        return false;
    }
}

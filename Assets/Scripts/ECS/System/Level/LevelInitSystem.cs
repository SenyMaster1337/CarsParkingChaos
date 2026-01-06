using Leopotam.Ecs;
using UnityEngine.SceneManagement;

public class LevelInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;

    public void Init()
    {
        InitLevel();
    }

    private void InitLevel()
    {
        var currentLevelEntity = _ecsWorld.NewEntity();

        ref var levelComponent = ref currentLevelEntity.Get<LevelComponent>();
        levelComponent.Entity = currentLevelEntity;
        levelComponent.CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        levelComponent.IsLevelCompleted = false;
    }
}
using Leopotam.Ecs;
using UnityEngine.SceneManagement;

public class LevelRestartSystem : IEcsRunSystem
{
    private EcsFilter<RestartLevelEvent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            var restartEvent = _filter.GetEntity(entity);
            Restart();
            restartEvent.Del<RestartLevelEvent>();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

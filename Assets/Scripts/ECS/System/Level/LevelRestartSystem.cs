using Leopotam.Ecs;
using UnityEngine.SceneManagement;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
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
}

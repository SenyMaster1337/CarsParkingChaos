using Leopotam.Ecs;

namespace CarParkingChaos.ECS.Components
{
    public struct LevelComponent
    {
        public EcsEntity Entity;
        public int CurrentLevel;
        public bool IsLevelCompleted;
    }
}

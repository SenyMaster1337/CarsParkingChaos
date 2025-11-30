
using Leopotam.Ecs;

public struct LevelComponent
{
    public EcsEntity entity;

    public int currentLevel;
    public int totalLevels;

    public bool isLevelCompleted;
    public float levelRestartTime;
}

using Leopotam.Ecs;

public struct LevelComponent
{
    public EcsEntity Entity;

    public int CurrentLevel;
    public int TotalLevels;

    public bool IsLevelCompleted;
    public float LevelRestartTime;
}
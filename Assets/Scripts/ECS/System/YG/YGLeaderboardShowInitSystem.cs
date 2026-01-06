using Leopotam.Ecs;

public class YGLeaderboardShowInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private LeaderboradShower _leaderboradShower;

    public YGLeaderboardShowInitSystem(LeaderboradShower leaderboradShower)
    {
        _leaderboradShower = leaderboradShower;
    }

    public void Init()
    {
        InitLeaderboard();
    }

    private void InitLeaderboard()
    {
        var leaderboardEntity = _ecsWorld.NewEntity();

        ref var leaderboradComponent = ref leaderboardEntity.Get<UILeaderboardComponent>();

        leaderboradComponent.LeaderboardShower = _leaderboradShower;
        leaderboradComponent.LeaderboardShower.WindowGroup.alpha = 0f;
        leaderboradComponent.LeaderboardShower.WindowGroup.interactable = false;
        leaderboradComponent.LeaderboardShower.WindowGroup.blocksRaycasts = false;
    }
}

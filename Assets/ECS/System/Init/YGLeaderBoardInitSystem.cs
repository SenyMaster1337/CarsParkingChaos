using Leopotam.Ecs;

public class YGLeaderBoardInitSystem : IEcsInitSystem
{
    private EcsWorld _ecsWorld;
    private LeaderboradShower _leaderboradShower;

    public YGLeaderBoardInitSystem(LeaderboradShower leaderboradShower)
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

        leaderboradComponent.leaderboardShower = _leaderboradShower;
        leaderboradComponent.leaderboardShower.WindowGroup.alpha = 0f;
        leaderboradComponent.leaderboardShower.WindowGroup.interactable = false;
        leaderboradComponent.leaderboardShower.WindowGroup.blocksRaycasts = false;
    }
}

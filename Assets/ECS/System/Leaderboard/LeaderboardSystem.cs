using Leopotam.Ecs;

public class LeaderboardSystem : IEcsRunSystem
{
    private EcsFilter<UILeaderboardComponent> _filter;
    private EcsFilter<OpenLeaderboardEvent> _openLeaderboard;
    private EcsFilter<CloseLeaderboardEvent> _closeLeaderboard;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var leaderboardComponent = ref _filter.Get1(entity);

            foreach (var openEntity in _openLeaderboard)
            {
                var eventOpenEntity = _openLeaderboard.GetEntity(openEntity);
                OpenSettings(leaderboardComponent);
                eventOpenEntity.Del<OpenLeaderboardEvent>();
            }

            foreach (var closeEntity in _closeLeaderboard)
            {
                var eventCloseEntity = _closeLeaderboard.GetEntity(closeEntity);
                CloseSettings(leaderboardComponent);
                eventCloseEntity.Del<CloseLeaderboardEvent>();
            }
        }
    }

    private void OpenSettings(UILeaderboardComponent leaderboardComponent)
    {
        leaderboardComponent.leaderboardShower.WindowGroup.alpha = 1.0f;
        leaderboardComponent.leaderboardShower.WindowGroup.interactable = true;
        leaderboardComponent.leaderboardShower.WindowGroup.blocksRaycasts = true;
    }

    private void CloseSettings(UILeaderboardComponent leaderboardComponent)
    {
        leaderboardComponent.leaderboardShower.WindowGroup.alpha = 0f;
        leaderboardComponent.leaderboardShower.WindowGroup.interactable = false;
        leaderboardComponent.leaderboardShower.WindowGroup.blocksRaycasts = false;
    }
}

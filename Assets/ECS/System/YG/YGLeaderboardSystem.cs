using Leopotam.Ecs;
using YG;

public class YGLeaderboardSystem : IEcsRunSystem
{
    private EcsFilter<AddPointsWinningLeaderboardEvent> _addPointsFilter;

    public void Run()
    {
        foreach (var addPointsEntity in _addPointsFilter) 
        {
            var addPointsEvent = _addPointsFilter.GetEntity(addPointsEntity);
            YG2.SetLeaderboard("LeaderboradYG2", 100);
            addPointsEvent.Del<AddPointsWinningLeaderboardEvent>();
        }
    }
}

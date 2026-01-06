using Leopotam.Ecs;
using YG;
using CarParkingChaos.ECS.Data;

namespace CarParkingChaos.ECS.Systems
{
    public class YGLeaderboardSystem : IEcsRunSystem
    {
        private EcsFilter<AddPointsWinningLeaderboardEvent> _addPointsFilter;
        private StaticData _staticData;

        public void Run()
        {
            foreach (var addPointsEntity in _addPointsFilter)
            {
                var addPointsEvent = _addPointsFilter.GetEntity(addPointsEntity);
                int score = _staticData.AddScoreWinningValueToLeaderboard;
                YG2.saves.LeaderboardScore += score;
                YG2.SetLeaderboard("LeaderboradYG2", YG2.saves.LeaderboardScore);
                addPointsEvent.Del<AddPointsWinningLeaderboardEvent>();
            }
        }
    }
}

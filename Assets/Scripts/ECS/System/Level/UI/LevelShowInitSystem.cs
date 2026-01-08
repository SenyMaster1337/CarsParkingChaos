using Leopotam.Ecs;
using CarParkingChaos.ECS.Data;
using CarParkingChaos.UI.Markers;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class LevelShowInitSystem : IEcsInitSystem
    {
        private EcsFilter<LevelComponent> _filter;

        private LevelCompleteShower _levelCompleteShower;
        private LevelLossShower _levelLossShower;
        private LevelCurrentShower _levelCurrentShower;
        private StaticData _staticData;

        public LevelShowInitSystem(
            LevelCompleteShower levelCompleteShower,
            LevelLossShower levelLossShower,
            LevelCurrentShower levelCurrentShower)
        {
            _levelCompleteShower = levelCompleteShower;
            _levelLossShower = levelLossShower;
            _levelCurrentShower = levelCurrentShower;
        }

        public void Init()
        {
            InitShowLevel();
        }

        private void InitShowLevel()
        {
            foreach (var entity in _filter)
            {
                ref var levelComponent = ref _filter.Get1(entity);

                ref var completeLevelComponent =
                    ref levelComponent.Entity.Get<UICompleteLevelComponent>();
                completeLevelComponent.LevelCompleteShower = _levelCompleteShower;
                completeLevelComponent.LevelCompleteShower.WindowGroup.alpha = 0f;
                completeLevelComponent.LevelCompleteShower.WindowGroup.interactable =
                    false;
                completeLevelComponent.LevelCompleteShower.WindowGroup.blocksRaycasts =
                    false;
                completeLevelComponent.LevelCompleteShower.CoinsNumberToWinText.Value
                    .SetText($"{_staticData.NumberCointAddedPerWin}");

                completeLevelComponent.LevelCompleteShower.BlackBackground
                    .WindowGroup.alpha = 0f;
                completeLevelComponent.LevelCompleteShower.BlackBackground
                    .WindowGroup.interactable = false;
                completeLevelComponent.LevelCompleteShower.BlackBackground
                    .WindowGroup.blocksRaycasts = false;

                ref var levelLossComponent =
                    ref levelComponent.Entity.Get<UILevelLossComponent>();
                levelLossComponent.LevelLossShower = _levelLossShower;
                levelLossComponent.LevelLossShower.WindowGroup.alpha = 0f;
                levelLossComponent.LevelLossShower.WindowGroup.interactable =
                    false;
                levelLossComponent.LevelLossShower.WindowGroup.blocksRaycasts =
                    false;

                ref var levelUIComponent =
                    ref levelComponent.Entity.Get<UILevelComponent>();
                levelUIComponent.LevelCurrentShower = _levelCurrentShower;
                levelUIComponent.LevelCurrentShower.CurrentLevelNumberText.Value
                    .SetText($"{levelComponent.CurrentLevel}");
            }
        }
    }
}
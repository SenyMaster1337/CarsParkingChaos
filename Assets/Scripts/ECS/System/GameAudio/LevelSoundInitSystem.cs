using Leopotam.Ecs;
using CarParkingChaos.Sounds;
using CarParkingChaos.ECS.Components;

namespace CarParkingChaos.ECS.Systems
{
    public class LevelSoundInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private GameSounds _gameSounds;

        public LevelSoundInitSystem(GameSounds gameSounds)
        {
            _gameSounds = gameSounds;
        }

        public void Init()
        {
            var soundsNewEntity = _ecsWorld.NewEntity();

            ref var audioComponent = ref soundsNewEntity.Get<GameAudioComponent>();
            audioComponent.WinSound = _gameSounds.WinSound;

            audioComponent.IsWinSoundEnable = false;
        }
    }
}

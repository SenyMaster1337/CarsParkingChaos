using Leopotam.Ecs;

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
        audioComponent.winSound = _gameSounds.WinSound;
        audioComponent.lossSound = _gameSounds.LossSound;

        audioComponent.isWinSoundEnable = false;
        audioComponent.isLossSoundEnable = false;
    }
}

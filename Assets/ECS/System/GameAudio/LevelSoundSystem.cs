using Leopotam.Ecs;
using UnityEngine;

public class LevelSoundSystem : IEcsRunSystem
{
    private EcsFilter<GameAudioComponent> _audioFilter;
    private EcsFilter<LevelComponent> _levelFilter;

    public void Run()
    {
        foreach (var audioEntity in _audioFilter)
        {
            ref var audioComponent = ref _audioFilter.Get1(audioEntity);

            foreach (var levelEntity in _levelFilter)
            {
                ref var levelComponent = ref _levelFilter.Get1(levelEntity);

                if (levelComponent.isLevelCompleted && audioComponent.isWinSoundEnable == false)
                {
                    audioComponent.winSound.AudioSource.Play();
                    audioComponent.isWinSoundEnable = true;
                }
            }
        }
    }
}

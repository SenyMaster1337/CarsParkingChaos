using Leopotam.Ecs;
using UnityEngine;

public class CarSoundSystem : IEcsRunSystem
{
    private EcsFilter<CarComponent, CarAudioComponent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var carComponent = ref _filter.Get1(entity);
            ref var carAudioComponent = ref _filter.Get2(entity);

            EnableSwitchCrashAudio(carComponent, ref carAudioComponent);
            EnableSwitchDriveAudio(carComponent, ref carAudioComponent);

            if (carComponent.isAllPassengersBoarded && carAudioComponent.isLeavingCarSoundEnable == false)
            {
                carAudioComponent.leavingSound.AudioSource.Play();
                carAudioComponent.isLeavingCarSoundEnable = true;
            }
        }
    }

    private void EnableSwitchCrashAudio(CarComponent carComponent, ref CarAudioComponent carAudioComponent)
    {
        if (carComponent.isCrashed && carAudioComponent.isCrashSoundEnable == false)
        {
            carAudioComponent.crashSound.AudioSource.Play();
            carAudioComponent.isCrashSoundEnable = true;
        }

        if (carComponent.isCrashed == false && carAudioComponent.isCrashSoundEnable)
        {
            carAudioComponent.isCrashSoundEnable = false;
        }
    }

    private void EnableSwitchDriveAudio(CarComponent carComponent, ref CarAudioComponent carAudioComponent)
    {
        if (carComponent.isParked == false && carAudioComponent.isDriveSoundEnable == false)
        {
            carAudioComponent.driveSound.AudioSource.Play();
            carAudioComponent.isDriveSoundEnable = true;
        }
    }
}

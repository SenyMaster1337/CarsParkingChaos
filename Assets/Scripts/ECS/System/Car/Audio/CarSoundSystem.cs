using Leopotam.Ecs;

namespace CarParkingChaos.ECS.Systems
{
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

                if (carComponent.IsAllPassengersBoarded && carAudioComponent.IsLeavingCarSoundEnable == false)
                {
                    carAudioComponent.LeavingSound.AudioSource.Play();
                    carAudioComponent.IsLeavingCarSoundEnable = true;
                }
            }
        }

        private void EnableSwitchCrashAudio(CarComponent carComponent, ref CarAudioComponent carAudioComponent)
        {
            if (carComponent.IsCrashed && carAudioComponent.IsCrashSoundEnable == false)
            {
                carAudioComponent.CrashSound.AudioSource.Play();
                carAudioComponent.IsCrashSoundEnable = true;
            }

            if (carComponent.IsCrashed == false && carAudioComponent.IsCrashSoundEnable)
            {
                carAudioComponent.IsCrashSoundEnable = false;
            }
        }

        private void EnableSwitchDriveAudio(CarComponent carComponent, ref CarAudioComponent carAudioComponent)
        {
            if (carComponent.IsParked == false && carAudioComponent.IsDriveSoundEnable == false)
            {
                carAudioComponent.DriveSound.AudioSource.Play();
                carAudioComponent.IsDriveSoundEnable = true;
            }
        }
    }
}

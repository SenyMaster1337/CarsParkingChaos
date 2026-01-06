using Leopotam.Ecs;

namespace CarParkingChaos.ECS.Systems
{
    public class CarEffectsSystem : IEcsRunSystem
    {
        private EcsFilter<CarComponent, CarMovableComponent, CarEffectsComponent> _carFilter;

        public void Run()
        {
            foreach (var effect in _carFilter)
            {
                ref var carComponent = ref _carFilter.Get1(effect);
                ref var carMovableComponent = ref _carFilter.Get2(effect);
                ref var carEffectComponent = ref _carFilter.Get3(effect);

                ToggleSwitchCrashEffect(carComponent, ref carEffectComponent);
                ToggleSwitchDriveEffect(carComponent, carMovableComponent, ref carEffectComponent);
            }
        }

        private void ToggleSwitchDriveEffect(CarComponent carComponent, CarMovableComponent carMovableComponent, ref CarEffectsComponent carEffectComponent)
        {
            if (carMovableComponent.IsMoving && carEffectComponent.IsDriveEffectActive == false && carComponent.CanCrashed == false)
            {
                carEffectComponent.CarDriveEffect.ParticleSystem.Play();
                carEffectComponent.IsDriveEffectActive = true;
            }

            if (carMovableComponent.IsMoving == false && carEffectComponent.IsDriveEffectActive == true && carComponent.IsAllPassengersBoarded == false)
            {
                carEffectComponent.CarDriveEffect.ParticleSystem.Stop();
                carEffectComponent.IsDriveEffectActive = false;
            }

            if (carComponent.IsAllPassengersBoarded && carEffectComponent.IsDriveEffectActive == false)
            {
                carEffectComponent.CarDriveEffect.ParticleSystem.Play();
                carEffectComponent.IsDriveEffectActive = true;
            }
        }

        private void ToggleSwitchCrashEffect(CarComponent carComponent, ref CarEffectsComponent carEffectComponent)
        {
            if (carComponent.IsAllPassengersBoarded && carEffectComponent.IsFilledPassengersEffectActive == false)
            {
                carEffectComponent.CarEffectFilledPassengers.ParticleSystem.Play();
                carEffectComponent.IsFilledPassengersEffectActive = true;
            }

            if (carComponent.IsCrashed && carEffectComponent.IsCrashEffectActive == false)
            {
                carEffectComponent.CarCrashEffect.ParticleSystem.Play();
                carEffectComponent.IsCrashEffectActive = true;
            }

            if (carComponent.IsCrashed == false && carEffectComponent.IsCrashEffectActive == true)
            {
                carEffectComponent.IsCrashEffectActive = false;
            }
        }
    }
}

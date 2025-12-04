using Leopotam.Ecs;
using UnityEngine;

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
        if (carMovableComponent.isMoving && carEffectComponent.isDriveEffectActive == false && carComponent.canCrashed == false)
        {
            carEffectComponent.carDriveEffect.ParticleSystem.Play();
            carEffectComponent.isDriveEffectActive = true;
        }

        if (carMovableComponent.isMoving == false && carEffectComponent.isDriveEffectActive == true && carComponent.isAllPassengersBoarded == false)
        {
            carEffectComponent.carDriveEffect.ParticleSystem.Stop();
            carEffectComponent.isDriveEffectActive = false;
        }

        if (carComponent.isAllPassengersBoarded && carEffectComponent.isDriveEffectActive == false)
        {
            carEffectComponent.carDriveEffect.ParticleSystem.Play();
            carEffectComponent.isDriveEffectActive = true;
        }
    }

    private void ToggleSwitchCrashEffect(CarComponent carComponent, ref CarEffectsComponent carEffectComponent)
    {
        if (carComponent.isAllPassengersBoarded && carEffectComponent.isFilledPassengersEffectActive == false)
        {
            carEffectComponent.carEffectFilledPassengers.ParticleSystem.Play();
            carEffectComponent.isFilledPassengersEffectActive = true;
        }

        if (carComponent.isCrashed && carEffectComponent.isCrashEffectActive == false)
        {
            carEffectComponent.carCrashEffect.ParticleSystem.Play();
            carEffectComponent.isCrashEffectActive = true;
        }

        if (carComponent.isCrashed == false && carEffectComponent.isCrashEffectActive == true)
        {
            carEffectComponent.isCrashEffectActive = false;
        }
    }
}

using Leopotam.Ecs;

public class CarEffectsSystem : IEcsRunSystem
{
    private EcsFilter<CarComponent, CarEffectsComponent> _carFilter;

    public void Run()
    {
        foreach (var effect in _carFilter)
        {
            ref var carComponent = ref _carFilter.Get1(effect);
            ref var carEffectComponent = ref _carFilter.Get2(effect);

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
}

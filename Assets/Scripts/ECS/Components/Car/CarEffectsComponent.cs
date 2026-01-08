using CarParkingChaos.Effects;

namespace CarParkingChaos.ECS.Components
{
    public struct CarEffectsComponent
    {
        public CarFilledPassengersEffect CarEffectFilledPassengers;
        public CarCrashEffect CarCrashEffect;
        public CarDriveEffect CarDriveEffect;
        public bool IsFilledPassengersEffectActive;
        public bool IsCrashEffectActive;
        public bool IsDriveEffectActive;
    }
}

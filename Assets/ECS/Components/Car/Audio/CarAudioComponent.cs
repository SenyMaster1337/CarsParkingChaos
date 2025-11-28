using UnityEngine;

public struct CarAudioComponent
{
    public CarDriveSound driveSound;
    public CarCrashSound crashSound;
    public CarLeavingSound leavingSound;

    public bool isDriveSoundEnable;
    public bool isCrashSoundEnable;
    public bool isLeavingCarSoundEnable;
}

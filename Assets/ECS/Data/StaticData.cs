using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Create new StaticData")]
public class StaticData : ScriptableObject
{
    [field: SerializeField] public float CarSpeed { get; private set; } = 2f;
    [field: SerializeField] public float MaxLinerCarSpeed { get; private set; } = 100f;
    [field: SerializeField] public float PassengerSpeed { get; private set; } = 4f;
    [field: SerializeField] public int DefaultCarSlots { get; private set; } = 4;
    [field: SerializeField] public int MinivanCarSlots { get; private set; } = 8;
    [field: SerializeField] public Quaternion RotationCarInParking { get; private set; } = Quaternion.Euler(0, -30, 0);
    [field: SerializeField] public float DistanceToDisableCrashHandler { get; private set; } = 16f;
    [field: SerializeField] public float DesktopCameraOrtograpgicSize { get; private set; } = 35;
    [field: SerializeField] public float MobileCameraOrtograpgicSize { get; private set; } = 45;
    [field: SerializeField] public float CooldownInputReaderToCar { get; private set; } = 0.7f;
    [field: SerializeField] public float MinMusicSoundValue { get; private set; } = -80;
    [field: SerializeField] public float MaxMusicSoundValue { get; private set; } = -25;
    [field: SerializeField] public float MinMasterSoundValue { get; private set; } = -80;
    [field: SerializeField] public float MaxMasterSoundValue { get; private set; } = -20;
    [field: SerializeField] public float TimeToLevelShowLevelComplete { get; private set; } = 1.5f;
    [field: SerializeField] public int NumberCointAddedPerWin { get; private set; } = 50;
}

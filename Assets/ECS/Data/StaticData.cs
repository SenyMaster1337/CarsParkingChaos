using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Create new StaticData")]
public class StaticData : ScriptableObject
{
    [field: SerializeField] public float CarSpeed { get; private set; } = 2f;
    [field: SerializeField] public float PassengerSpeed { get; private set; } = 4f;
    [field: SerializeField] public int DefaultCarSlots { get; private set; } = 4;
    [field: SerializeField] public int MinivanCarSlots { get; private set; } = 8;
    [field: SerializeField] public Quaternion RotationCarInParking { get; private set; } = Quaternion.Euler(0, -30, 0);
    [field: SerializeField] public  float DistanceToDisableCrashHandler { get; private set; } = 16f;
}

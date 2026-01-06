using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    [field: SerializeField] public PassengerCounter PassengerCounter { get; private set; }
    [field: SerializeField] public List<Transform> QueuePositions { get; private set; }
    [field: SerializeField] public bool TutorialEnabed { get; private set; } = false;
    [field: SerializeField] public bool RandomColorCarsEnabled { get; private set; } = false;
    [field: SerializeField] public int UnlockParkingSlotsCount { get; private set; } = 4;
    [field: SerializeField] public LevelCarsMaterial LevelCarsMaterial { get; private set; }
}

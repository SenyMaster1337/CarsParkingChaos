using UnityEngine;

public class GameSounds : MonoBehaviour
{
    [field: SerializeField] public WinSound WinSound { get; private set; }
    [field: SerializeField] public LossSound LossSound { get; private set; }
}

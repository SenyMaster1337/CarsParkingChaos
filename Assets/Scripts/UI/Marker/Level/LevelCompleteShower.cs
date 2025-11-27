using UnityEngine;

public class LevelCompleteShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public NextLevelButtonClickReader NextLevelButtonClickReader { get; private set; }
}

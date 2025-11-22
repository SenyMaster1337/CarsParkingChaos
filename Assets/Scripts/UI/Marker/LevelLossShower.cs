using UnityEngine;

public class LevelLossShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public RestartButtonClickReader RestartButtonClickReader { get; private set; }
}

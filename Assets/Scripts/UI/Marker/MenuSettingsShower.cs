using UnityEngine;
using UnityEngine.UI;

public class MenuSettingsShower : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup WindowGroup { get; private set; }
    [field: SerializeField] public SettingsOpenButtonClick SettingsOpenButtonClick { get; private set; }
    [field: SerializeField] public SettingsCloseButtonClick SettingsCloseButtonClick { get; private set; }
    [field: SerializeField] public SoundMuteToggle SoundMuteToggle { get; private set; }
    [field: SerializeField] public MusicMuteToggle MusicMuteToggle { get; private set; }
}

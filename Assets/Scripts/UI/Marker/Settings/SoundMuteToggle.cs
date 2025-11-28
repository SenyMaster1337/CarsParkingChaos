using UnityEngine;
using UnityEngine.Audio;

public class SoundMuteToggle : MonoBehaviour
{
    [field: SerializeField] public MuteSoundButtonClickReader MuteSoundButtonClickReader { get; private set; }
    [field: SerializeField] public UnmuteSoundButtonClickReader UnmuteSoundButtonClickReader { get; private set; }
    [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
}

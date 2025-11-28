using UnityEngine;
using UnityEngine.Audio;

public class MusicMuteToggle : MonoBehaviour
{
    [field: SerializeField] public MuteMusicButtonClickReader MuteMusicButtonClickReader { get; private set; }
    [field: SerializeField] public UnmuteMusicButtonClickReader UnmuteMusicButtonClickReader { get; private set; }
    [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
}

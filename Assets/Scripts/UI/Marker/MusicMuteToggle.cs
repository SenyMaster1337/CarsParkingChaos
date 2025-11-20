using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMuteToggle : MonoBehaviour
{
    [field: SerializeField] public MuteMusicButtonClickReader MuteMusicButtonClickReader { get; private set; }
    [field: SerializeField] public UnmuteMusicButtonClickReader UnuteMusicButtonClickReader { get; private set; }
}

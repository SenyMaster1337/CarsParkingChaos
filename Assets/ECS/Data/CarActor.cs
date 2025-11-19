using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarActor", menuName = "Create new CarActor")]
public class CarActor : ScriptableObject
{
    [field: SerializeField] public Animator LeavingAnimation;
}

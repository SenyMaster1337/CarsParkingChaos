using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMaterial", menuName = "Data/Create new CarsColor")]
public class LevelCarsMaterial : ScriptableObject
{
    [field: SerializeField] public List<Material> CarsMaterial;
}

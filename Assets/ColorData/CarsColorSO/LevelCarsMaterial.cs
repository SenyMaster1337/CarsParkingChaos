using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelMaterial", menuName = "LevelMaterial/Create new CarsColor")]
public class LevelCarsMaterial : ScriptableObject
{
    public List<Material> CarsMaterial;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!EditorApplication.isPlaying && !EditorApplication.isCompiling)
        {
            EditorUtility.SetDirty(this);
        }
    }
#endif
}

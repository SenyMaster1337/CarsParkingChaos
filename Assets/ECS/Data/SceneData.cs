using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    [field: SerializeField] public List<Transform> QueuePositions { get; private set; }
    [field: SerializeField] public bool TutorialEnabe { get; private set; } = false;

    [Header("Система сортировки пассажиров")]
    [Tooltip("1 - сортировка с конца / 2 - сортировка через 4/8 пассажиров")]
    [Range(1, 2)]
    [SerializeField] private int _variableSortingSystem = 1;

    public int VariableSortingSystem => _variableSortingSystem;
}

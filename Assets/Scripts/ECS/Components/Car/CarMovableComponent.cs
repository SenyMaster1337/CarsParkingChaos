using System.Collections.Generic;
using UnityEngine;

public struct CarMovableComponent
{
    public Vehicle Car;
    public Rigidbody Rigidbody;
    public Transform Transform;

    public Vector3 SpawnPosition;
    public Vector3 TargetPoint;

    public float MoveSpeed;

    public bool IsMoving;
    public bool IsReverseDirectionEnable;

    public List<CarRotate> CarRotates;
}

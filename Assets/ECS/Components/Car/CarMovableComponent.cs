using System.Collections.Generic;
using UnityEngine;

public struct CarMovableComponent
{
    public Vehicle car;
    public Rigidbody rigidbody;
    public Transform transform;

    public Vector3 spawnPosition;
    public Vector3 targetPoint;

    public float moveSpeed;

    public bool isMoving;
    public bool isReverseDirectionEnable;

    public List<CarRotate> carRotates;
}

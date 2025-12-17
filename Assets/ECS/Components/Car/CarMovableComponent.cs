using UnityEngine;

public struct CarMovableComponent
{
    public Transform currentTransform;

    public Vector3 spawnPosition;
    public Vector3 targetPoint;

    public float moveSpeed;

    public bool isSpeedUpEnable;
    public bool isMoving;
    public bool isReverseDirectionEnable;
}

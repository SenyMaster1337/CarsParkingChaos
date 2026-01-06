using UnityEngine;

public struct PassengerMovableComponent
{
    public Transform CurrentTransform;

    public Vector3 QueuePointPosition;
    public Vector3 StartQueuePosition;
    public Vector3 TargetCarPosition;

    public float MoveSpeed;

    public bool IsMoving;
    public bool IsPositionStartQueuePosition;
    public bool IsNeedShiftQueue;
}

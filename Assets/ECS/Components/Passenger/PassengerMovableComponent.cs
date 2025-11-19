using UnityEngine;

public struct PassengerMovableComponent
{
    public Transform currentTransform;

    public Vector3 currentQueuePointPosition;
    public Vector3 startQueuePosition;
    public Vector3 targetCarPosition;

    public float moveSpeed;

    public bool isMoving;
    public bool isPositionStartQueuePosition;
    public bool isNeedShiftQueue;
}

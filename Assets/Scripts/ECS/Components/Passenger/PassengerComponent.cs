using UnityEngine;

public struct PassengerComponent
{
    public Renderer Renderer;
    public Passenger Passenger;
    public CarComponent CarComponent;

    public Vector3 StartQueuePosition;

    public bool IsSorted;
}

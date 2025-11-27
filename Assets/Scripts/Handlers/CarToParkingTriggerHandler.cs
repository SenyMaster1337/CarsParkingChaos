using System;
using UnityEngine;

public class CarToParkingTriggerHandler : BoxTriggerHandler
{
    public event Action<Vehicle> CarEnterParking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Vehicle car))
        {
            CarEnterParking?.Invoke(car);
        }
    }
}

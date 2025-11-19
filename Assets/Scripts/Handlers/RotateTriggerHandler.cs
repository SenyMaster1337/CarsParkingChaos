using System;
using UnityEngine;

public class RotateTriggerHandler : BoxTriggerHandler
{
    public event Action<Vehicle, Quaternion> OnTriggerCar;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Vehicle car))
        {
            OnTriggerCar?.Invoke(car, this.gameObject.transform.rotation);
        }
    }
}

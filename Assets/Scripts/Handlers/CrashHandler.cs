using System;
using UnityEngine;

public class CrashHandler : BoxTriggerHandler
{
    public event Action<Vehicle, Vehicle> OnCollisionCar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Vehicle car))
        {
            Debug.Log("CrashHandler");
            OnCollisionCar?.Invoke(this.gameObject.GetComponentInParent<Vehicle>(), car);
        }
    }
}

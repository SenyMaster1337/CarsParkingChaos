using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterHandler : BoxTriggerHandler
{
    public event Action<Vehicle> OnCollisionCar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Vehicle car))
        {
            OnCollisionCar?.Invoke(car);
        }
    }
}

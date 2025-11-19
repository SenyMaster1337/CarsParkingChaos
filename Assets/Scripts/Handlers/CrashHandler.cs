using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CrashHandler : MonoBehaviour
{
    public BoxCollider BoxCollider { get; private set; }

    public event Action<Vehicle, Vehicle> OnCollisionCar;

    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();

        if (BoxCollider != null)
        {
            BoxCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Vehicle car))
        {
            Debug.Log("CrashHandler");
            OnCollisionCar?.Invoke(this.gameObject.GetComponentInParent<Vehicle>(), car);
        }
    }
}

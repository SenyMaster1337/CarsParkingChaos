using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    private BoxCollider _boxCollider;

    public event Action<Vehicle> OnCollisionCar;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        if (_boxCollider != null)
        {
            _boxCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Vehicle car))
        {
            OnCollisionCar?.Invoke(car);
        }
    }
}

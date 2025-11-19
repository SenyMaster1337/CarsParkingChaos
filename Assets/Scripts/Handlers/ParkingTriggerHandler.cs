using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ParkingTriggerHandler : MonoBehaviour
{
    private BoxCollider _boxCollider;

    public event Action<Vehicle> CarEnterParking;

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
            Debug.Log($"ParkingTriggerHandler: Машина заехала на парквоку {car.name}");
            CarEnterParking?.Invoke(car);
        }
    }
}

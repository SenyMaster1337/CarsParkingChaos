using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RotateTriggerHandler : MonoBehaviour
{
    private BoxCollider _boxCollider;

    public event Action<Vehicle, Quaternion> OnTriggerCar;

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
        if(other.gameObject.TryGetComponent(out Vehicle car))
        {
            OnTriggerCar?.Invoke(car, this.gameObject.transform.rotation);
        }
    }
}

using System;
using UnityEngine;

public class CrashHandler : MonoBehaviour
{
    public event Action<Vehicle, Vehicle> OnCollisionCar;

    private BoxCollider _boxCollider;
    private Vehicle _currentCar;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _currentCar = this.gameObject.GetComponentInParent<Vehicle>();

        if (_boxCollider != null)
        {
            _boxCollider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Vehicle car))
        {
            OnCollisionCar?.Invoke(_currentCar, car);
        }
    }

    public void DisableBoxCollider()
    {
        _boxCollider.enabled = false;
    }
}

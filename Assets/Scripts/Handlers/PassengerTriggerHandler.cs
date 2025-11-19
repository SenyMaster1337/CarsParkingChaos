using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PassengerTriggerHandler : MonoBehaviour
{
    private BoxCollider _boxCollider;

    public event Action<Passenger> OnTriggerPassenger;

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
        if (other.gameObject.TryGetComponent(out Passenger passenger))
        {
            Debug.Log(passenger.GetComponentInChildren<Renderer>().material);
            OnTriggerPassenger?.Invoke(passenger);
        }
    }
}

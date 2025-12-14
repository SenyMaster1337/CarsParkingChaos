using Leopotam.Ecs;
using System;
using UnityEngine;

public class Vehicle : BoxTriggerHandler
{
    public EcsEntity Entity;

    public event Action<Quaternion, Vehicle> OnTriggerCar;
    public event Action<CarParkingDirection, Vehicle> OnCollisionCar;
    public event Action<Vehicle> CarEnterParking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CarRotate rotateTriggerHandler))
        {
            OnTriggerCar?.Invoke(rotateTriggerHandler.gameObject.transform.rotation, this);
        }

        if (other.gameObject.TryGetComponent(out CarParkingDirection carEnter))
        {
            OnCollisionCar?.Invoke(carEnter, this);
        }

        if (other.gameObject.TryGetComponent(out CarParking carToParkingTriggerHandler))
        {
            CarEnterParking?.Invoke(this);
        }
    }
}

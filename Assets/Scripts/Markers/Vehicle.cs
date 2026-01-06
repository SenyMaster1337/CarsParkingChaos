using System;
using Leopotam.Ecs;
using UnityEngine;

namespace CarParkingChaos.Markers
{
    public class Vehicle : MonoBehaviour
    {
        public EcsEntity Entity;

        public event Action<CarRotate, Vehicle> OnTriggerCar;
        public event Action<CarParkingDirection, Vehicle> OnCollisionCar;
        public event Action<Vehicle> CarEnterParking;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CarRotate rotateTriggerHandler))
            {
                OnTriggerCar?.Invoke(rotateTriggerHandler, this);
            }

            if (other.gameObject.TryGetComponent(out CarParkingDirection carEnter))
            {
                OnCollisionCar?.Invoke(carEnter, this);
            }

            if (other.gameObject.TryGetComponent(out ParkingCars carToParkingTriggerHandler))
            {
                CarEnterParking?.Invoke(this);
            }
        }
    }
}

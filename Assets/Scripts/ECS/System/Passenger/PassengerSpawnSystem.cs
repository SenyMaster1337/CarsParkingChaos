using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class PassengerSpawnSystem : IEcsInitSystem
{
    private List<Vehicle> _cars;
    private Passenger _passengerPrefab;
    private List<Passenger> _passengers;

    public PassengerSpawnSystem(List<Vehicle> cars, Passenger passengerPrefab)
    {
        _cars = cars;
        _passengerPrefab = passengerPrefab;
        _passengers = new List<Passenger>();
    }

    public List<Passenger> Passengers => _passengers;

    public void Init()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            for (int j = 0; j < _cars[i].Entity.Get<CarComponent>().MaxPassengersSlots; j++)
            {
                _passengers.Add(GameObject.Instantiate(_passengerPrefab));
            }
        }
    }
}
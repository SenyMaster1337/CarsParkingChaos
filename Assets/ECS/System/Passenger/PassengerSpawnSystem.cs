using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawnSystem : IEcsInitSystem
{
    private List<Vehicle> _cars;
    private Passenger _passengerPrefab;
    private List<Passenger> _passengers;

    public List<Passenger> Passengers => _passengers;

    public PassengerSpawnSystem(List<Vehicle> cars, Passenger passengerPrefab)
    {
        _cars = cars;
        _passengerPrefab = passengerPrefab;
        _passengers = new List<Passenger>();
    }

    public void Init()
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            for (int j = 0; j < _cars[i].Entity.Get<CarComponent>().maxPassengersSlots; j++)
            {
                _passengers.Add(GameObject.Instantiate(_passengerPrefab));
            }
        }
    }
}
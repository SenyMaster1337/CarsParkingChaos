using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsRandomMaterialSystem : IEcsRunSystem
{
    private EcsFilter<CarsRandomColorComponent> _randomColorFilter;

    private System.Random _random;
    private StaticData _staticData;
    private SceneData _sceneData;

    public CarsRandomMaterialSystem()
    {
        _random = new System.Random();
    }

    public void Run()
    {
        foreach (var randomColorEntity in _randomColorFilter)
        {
            Debug.Log("‡‡‡");

            ref var randomColorComponent = ref _randomColorFilter.Get1(randomColorEntity);

            for (int i = 0; i < randomColorComponent.cars.Count; i++)
            {
                Material randomMaterial = _staticData.AllMaterialsUnit[_random.Next(0, _staticData.AllMaterialsUnit.Count)];
                randomColorComponent.cars[i].Entity.Get<CarComponent>().renderer.material = randomMaterial;
                _sceneData.LevelCarsMaterial.CarsMaterial.Add(randomMaterial);
            }

            _randomColorFilter.GetEntity(randomColorEntity).Del<CarsRandomColorComponent>();
        }
    }
}

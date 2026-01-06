using Leopotam.Ecs;
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
            ref var randomColorComponent = ref _randomColorFilter.Get1(randomColorEntity);

            for (int i = 0; i < randomColorComponent.Cars.Count; i++)
            {
                Material randomMaterial = _staticData.AllMaterialsUnit[_random.Next(0, _staticData.AllMaterialsUnit.Count)];
                randomColorComponent.Cars[i].Entity.Get<CarComponent>().Renderer.material = randomMaterial;
                _sceneData.LevelCarsMaterial.CarsMaterial.Add(randomMaterial);
            }

            _randomColorFilter.GetEntity(randomColorEntity).Del<CarsRandomColorComponent>();
        }
    }
}

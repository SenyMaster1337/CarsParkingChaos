using Leopotam.Ecs;
using UnityEngine;
using YG;

public class YGInitPlayerSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsFilter<InterstitialAdvShowEvent> _interstitialAdv;
    private Camera _mainCamera;

    public void Init()
    {
        if (YG2.envir.isDesktop)
        {
            _mainCamera.orthographicSize = 35;
        }
        else
        {
            _mainCamera.orthographicSize = 55;
        }
    }

    public void Destroy()
    {
        
    }

    public void Run()
    {
        foreach (var interstitialEntity in _interstitialAdv)
        {
            var interstitialEvent = _interstitialAdv.GetEntity(interstitialEntity);
            YG2.InterstitialAdvShow();
            interstitialEvent.Del<InterstitialAdvShowEvent>();
        }
    }
}

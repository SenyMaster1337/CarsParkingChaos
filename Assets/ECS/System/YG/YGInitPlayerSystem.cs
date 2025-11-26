using Leopotam.Ecs;
using UnityEngine;
using YG;

public class YGInitPlayerSystem : IEcsInitSystem, IEcsDestroySystem, IEcsRunSystem
{
    private EcsFilter<InterstitialAdvShowEvent> _interstitialAdv;
    private Camera _mainCamera;
    private StaticData _staticData;

    public void Init()
    {
        YG2.SetLeaderboard("LeaderboradYG2", 100);

        if (YG2.envir.isDesktop)
        {
            _mainCamera.orthographicSize = _staticData.DesktopCameraOrtograpgicSize;
        }
        else
        {
            _mainCamera.orthographicSize = _staticData.MobileCameraOrtograpgicSize;
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

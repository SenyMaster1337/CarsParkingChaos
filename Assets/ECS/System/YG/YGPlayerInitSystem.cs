using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YGPlayerInitSystem : IEcsInitSystem
{
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
}

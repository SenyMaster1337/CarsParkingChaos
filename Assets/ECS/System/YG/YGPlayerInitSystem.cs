using Leopotam.Ecs;
using UnityEngine;
using YG;

public class YGPlayerInitSystem : IEcsInitSystem
{
    private Camera _mainCamera;
    private SceneData _sceneData;

    public void Init()
    {
        if (YG2.envir.isDesktop)
        {
            _mainCamera.transform.position = new Vector3(4.4000001f, 88.5999985f, -38.5099983f);
            _mainCamera.transform.rotation = Quaternion.Euler(53.3000031f, 340.089966f, 1.42861074e-06f);
            _mainCamera.fieldOfView = 36;
            _sceneData.PassengerCounter.transform.position = _sceneData.PassengerCounter.DesktopPointPassengerCounter.transform.position;
        }
        else
        {
            _mainCamera.transform.position = new Vector3(4.4000001f, 88.5999985f, -38.5099983f);
            _mainCamera.transform.rotation = Quaternion.Euler(54.5200005f, 335.199982f, -1.47096137e-06f);
            _mainCamera.fieldOfView = 50;
            _sceneData.PassengerCounter.transform.position = _sceneData.PassengerCounter.MobilePointPassengerCounter.transform.position;
        }
    }
}

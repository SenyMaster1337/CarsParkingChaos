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
            _mainCamera.transform.position = new Vector3(6.20293236f, 69.6996765f, -42.8993835f);
            _mainCamera.transform.rotation = Quaternion.Euler(46.2000046f, 336.700012f, 0f);
            _mainCamera.fieldOfView = 38;
            _sceneData.PassengerCounter.transform.position = _sceneData.PassengerCounter.DesktopPointPassengerCounter.transform.position;
        }
        else
        {
            _mainCamera.transform.position = new Vector3(6.20293236f, 69.6996765f, -42.8993835f);
            _mainCamera.transform.rotation = Quaternion.Euler(48.4000053f, 336f, -1.2859465e-06f);
            _mainCamera.fieldOfView = 55;
            _sceneData.PassengerCounter.transform.position = _sceneData.PassengerCounter.MobilePointPassengerCounter.transform.position;
        }
    }
}

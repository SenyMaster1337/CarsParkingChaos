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
            _mainCamera.transform.rotation = Quaternion.Euler(45.9999962f, 338f, 0f);
            _mainCamera.fieldOfView = 39;
            _sceneData.PassengerCounter.transform.position = _sceneData.PassengerCounter.DesktopPointPassengerCounter.transform.position;
        }
        else
        {
            _mainCamera.transform.position = new Vector3(6.20293236f, 69.6996765f, -42.8993835f);
            _mainCamera.transform.rotation = Quaternion.Euler(48.4000053f, 336f, 0);
            _mainCamera.fieldOfView = 50;
            _sceneData.PassengerCounter.transform.position = _sceneData.PassengerCounter.MobilePointPassengerCounter.transform.position;
        }
    }
}

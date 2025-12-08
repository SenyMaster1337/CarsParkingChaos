using Leopotam.Ecs;
using UnityEngine;
using YG;

public class YGPlayerInitSystem : IEcsInitSystem
{
    private Camera _mainCamera;
    private StaticData _staticData;

    public void Init()
    {
        if (YG2.envir.isDesktop)
        {
            _mainCamera.transform.position = new Vector3(4.4000001f, 88.5999985f, -38.5099983f);
            _mainCamera.transform.rotation = Quaternion.Euler(54.5200005f, 335.199982f, -1.47096137e-06f);
            _mainCamera.fieldOfView = 40;
        }
        else
        {
            _mainCamera.transform.position = new Vector3(4.4000001f, 88.5999985f, -38.5099983f);
            _mainCamera.transform.rotation = Quaternion.Euler(54.5200005f, 335.199982f, -1.47096137e-06f);
            _mainCamera.fieldOfView = 54;
        }
    }
}

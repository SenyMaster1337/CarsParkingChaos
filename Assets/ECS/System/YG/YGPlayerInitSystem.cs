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
            //_mainCamera.transform.position = new Vector3(17.6000004f, 112.809998f, -52.8199997f);
            //_mainCamera.transform.rotation = Quaternion.Euler(52.9000053f, 334.999969f, 0f);
            //_mainCamera.fieldOfView = 30;
        }
        else
        {
            //_mainCamera.transform.position = new Vector3(-8.5f, 53.8199997f, -22.5200005f);
            //_mainCamera.transform.rotation = Quaternion.Euler(56.0000038f, 343f, 7.63397907e-07f);
            //_mainCamera.fieldOfView = 45;
        }
    }
}

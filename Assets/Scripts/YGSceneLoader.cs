using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YGSceneLoader : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private const string SceneName = "Level";

    private void Start()
    {
        if (YG2.envir.isDesktop)
        {
            _mainCamera.transform.position = new Vector3(6.20293236f, 69.6996765f, -42.8993835f);
            _mainCamera.transform.rotation = Quaternion.Euler(45.9999962f, 336.700012f, -1.2290551e-06f);
            _mainCamera.fieldOfView = 39;
        }
        else
        {
            _mainCamera.transform.position = new Vector3(6.20293236f, 69.6996765f, -42.8993835f);
            _mainCamera.transform.rotation = Quaternion.Euler(48.4000053f, 336f, -1.2859465e-06f);
            _mainCamera.fieldOfView = 50;
        }

        //SceneManager.LoadScene(SceneName + YG2.saves.level);
    }
}

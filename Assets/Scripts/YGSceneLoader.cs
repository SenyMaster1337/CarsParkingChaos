using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YGSceneLoader : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private const string SceneName = "Level";

    private void Start()
    {
        //if (YG2.envir.isDesktop)
        //{
        //    _mainCamera.transform.position = new Vector3(-1.11000001f, 45f, -43.0999985f));
        //    _mainCamera.transform.rotation = Quaternion.Euler(53.3000031f, 340.089966f, 1.42861074e-06f);
        //    _mainCamera.fieldOfView = 37;
        //}
        //else
        //{
        //    _mainCamera.transform.position = new Vector3(4.4000001f, 88.5999985f, -38.5099983f);
        //    _mainCamera.transform.rotation = Quaternion.Euler(54.5200005f, 335.199982f, -1.47096137e-06f);
        //    _mainCamera.fieldOfView = 54;
        //}

        //SceneManager.LoadScene(SceneName + YG2.saves.level);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YGPlayerLoader : MonoBehaviour
{
    private const string SceneName = "Level";

    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
        SetCameraSettings();
        LoadSavePlayerScene();
        YG2.GameReadyAPI();
    }

    private void SetCameraSettings()
    {
        if (YG2.envir.isDesktop)
        {
            _mainCamera.transform.position = new Vector3(6.20293236f, 69.6996765f, -42.8993835f);
            _mainCamera.transform.rotation = Quaternion.Euler(45.9999962f, 340f, 0f);
            _mainCamera.fieldOfView = 39;
        }
        else
        {
            _mainCamera.transform.position = new Vector3(6.20293236f, 69.6996765f, -42.8993835f);
            _mainCamera.transform.rotation = Quaternion.Euler(48.4000053f, 336f, 0);
            _mainCamera.fieldOfView = 50;
        }
    }

    private void LoadSavePlayerScene()
    {
        SceneManager.LoadScene(SceneName + YG2.saves.Level);
    }
}

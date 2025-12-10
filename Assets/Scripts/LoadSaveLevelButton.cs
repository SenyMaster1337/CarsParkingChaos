using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LoadSaveLevelButton : MonoBehaviour
{
    [SerializeField] public Button Button;
    private const string SceneName = "Level";

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(SceneName + YG2.saves.level);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YGSceneLoader : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(YG2.saves.level);
    }
}

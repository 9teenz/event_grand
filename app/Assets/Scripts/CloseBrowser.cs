using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseBrowser : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

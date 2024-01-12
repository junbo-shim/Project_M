using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void GoToLoading()
    {
        SceneManager.LoadSceneAsync("LoadingScene");
    }
}

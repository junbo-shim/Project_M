using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject[] systemSettingUI;
    bool activeCanvas = false;

    public void SystemSettingOnOff()
    {
        activeCanvas = !activeCanvas;
        systemSettingUI[0].SetActive(activeCanvas);
    }

    public void GoToLoading()
    {
        SceneManager.LoadSceneAsync("LoadingScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public GameObject[] systemSettingUI;
    bool activeCanvas = false;

    public void SystemSettingOnOff()
    {
        activeCanvas = !activeCanvas;
        systemSettingUI[0].SetActive(activeCanvas);
    }
}

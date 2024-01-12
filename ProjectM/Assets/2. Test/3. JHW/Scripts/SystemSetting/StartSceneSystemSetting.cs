using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneSystemSetting : MonoBehaviour
{

    public GameObject startSceneSystemSettingOnOff;

    public GameObject startSceneHelperUIOnOff;



    public void Open()
    {
        startSceneSystemSettingOnOff.SetActive(true);
    }

    public void Close()
    {
        startSceneSystemSettingOnOff.SetActive(false);
    }



    public void HelperOpen()
    {
        startSceneHelperUIOnOff.SetActive(true);
    }

    public void HelperClose()
    {
        startSceneHelperUIOnOff.SetActive(false);
    }
}

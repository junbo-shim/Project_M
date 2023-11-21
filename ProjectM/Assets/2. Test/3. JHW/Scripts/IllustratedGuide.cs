using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedGuide : MonoBehaviour
{
    public GameObject[] creaftingUIOnOff;


    public void FIreBallOnOff()
    {
        creaftingUIOnOff[0].SetActive(true);
        creaftingUIOnOff[1].SetActive(false);
        creaftingUIOnOff[2].SetActive(false);
        creaftingUIOnOff[3].SetActive(false);

    }

    public void RazerOnOff()
    {
        creaftingUIOnOff[1].SetActive(true);
        creaftingUIOnOff[0].SetActive(false);
        creaftingUIOnOff[2].SetActive(false);
        creaftingUIOnOff[3].SetActive(false);

    }

    public void IceBulletOnOff()
    {
        creaftingUIOnOff[2].SetActive(true);
        creaftingUIOnOff[0].SetActive(false);
        creaftingUIOnOff[1].SetActive(false);
        creaftingUIOnOff[3].SetActive(false);
    }

    public void PoisonOnOff()
    {
        creaftingUIOnOff[3].SetActive(true);
        creaftingUIOnOff[0].SetActive(false);
        creaftingUIOnOff[1].SetActive(false);
        creaftingUIOnOff[2].SetActive(false);
    }
}

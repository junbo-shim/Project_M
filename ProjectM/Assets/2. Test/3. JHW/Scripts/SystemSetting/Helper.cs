using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public GameObject ControlKeyUI;
    public GameObject AttackUI;

   public void ControlKeyUIOnOff()
    {
        ControlKeyUI.SetActive(true);
        AttackUI.SetActive(false);
    }

    public void AttackUIOnOff()
    {
        ControlKeyUI.SetActive(false);
        AttackUI.SetActive(true);  
    }
}

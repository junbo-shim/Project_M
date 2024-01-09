using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowDrawPattern : MonoBehaviour
{
    public GameObject[] drawPattern;

    #region FireBallPattern
    public void ShowFireBallPattern()
    {
        drawPattern[0].SetActive(true);
    }

    public void CloseFireballPattern()
    {
        drawPattern[0].SetActive(false);
    }
    #endregion


    #region ProtectPattern
    public void ShowProtectPattern()
    {
        drawPattern[1].SetActive(true);
    }

    public void CloseProtectPattern()
    {
        drawPattern[1].SetActive(false);
    }
    #endregion


    #region IceBalltPattern
    public void ShowIceBallPattern()
    {
        drawPattern[2].SetActive(true);
    }

    public void CloseIceBallPattern()
    {
        drawPattern[2].SetActive(false);
    }
    #endregion


    #region PoisonPattern
    public void ShowPoisonPattern()
    {
        drawPattern[3].SetActive(true);
    }

    public void ClosePoisonPattern()
    {
        drawPattern[3].SetActive(false);
    }
    #endregion


    #region JumpPattern
    public void ShowJumpPattern()
    {
        drawPattern[4].SetActive(true);
    }

    public void CloseJumpPattern()
    {
        drawPattern[4].SetActive(false);
    }
    #endregion


    #region HealPattern
    public void ShowHealPattern()
    {
        drawPattern[5].SetActive(true);
    }

    public void CloseHealPattern()
    {
        drawPattern[5].SetActive(false);
    }
    #endregion
}

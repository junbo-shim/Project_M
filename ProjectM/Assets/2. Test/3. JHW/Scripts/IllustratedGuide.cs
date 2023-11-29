using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IllustratedGuide : MonoBehaviour
{
    public GameObject[] creaftingUIOnOff;
    public GameObject[] SkillList;

    #region OnOff
    public void FIreBallOnOff()
    {
        for(int i = 0; i < 6; i++)
        {
            if(i == 0)
            {
                creaftingUIOnOff[i].SetActive(true);
            }
            else
            {
                creaftingUIOnOff[i].SetActive(false);
            }
        }

    }

    public void RazerOnOff()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 1)
            {
                creaftingUIOnOff[i].SetActive(true);
            }
            else
            {
                creaftingUIOnOff[i].SetActive(false);
            }
        }

    }

    public void IceBulletOnOff()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 2)
            {
                creaftingUIOnOff[i].SetActive(true);
            }
            else
            {
                creaftingUIOnOff[i].SetActive(false);
            }
        }
    }

    public void PoisonOnOff()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 3)
            {
                creaftingUIOnOff[i].SetActive(true);
            }
            else
            {
                creaftingUIOnOff[i].SetActive(false);
            }
        }
    }

    public void JumpOnOff()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 4)
            {
                creaftingUIOnOff[i].SetActive(true);
            }
            else
            {
                creaftingUIOnOff[i].SetActive(false);
            }
        }
    }

    public void HillOnOff()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 5)
            {
                creaftingUIOnOff[i].SetActive(true);
            }
            else
            {
                creaftingUIOnOff[i].SetActive(false);
            }
        }
    }
    #endregion


    #region ChangeColor
    public void FireBallChangeColor()
    {
        Image image = SkillList[0].GetComponent<Image>();

        if (image != null)
        {
            image.color = new Color(255 , 233 , 0);
        }
    }

    public void ProtectChangeColor()
    {
        Image image = SkillList[1].GetComponent<Image>();

        if (image != null)
        {
            image.color = new Color(255 , 233 , 0);
        }
    }


    public void IceBallChangeColor()
    {
        Image image = SkillList[2].GetComponent<Image>();

        if (image != null)
        {
            image.color = new Color(255, 233, 0);
        }
    }

    public void PoisonChangeColor()
    {
        Image image = SkillList[3].GetComponent<Image>();

        if (image != null)
        {
            image.color = new Color(255, 233, 0);
        }
    }

    
        public void JumpChangeColor()
    {
        Image image = SkillList[4].GetComponent<Image>();

        if (image != null)
        {
            image.color = new Color(255, 233, 0);
        }
    }



    public void HillChangeColor()
    {
        Image image = SkillList[5].GetComponent<Image>();

        if (image != null)
        {
            image.color = new Color(255, 233, 0);
        }
    }

    #endregion
}

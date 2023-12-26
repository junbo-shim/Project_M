using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IllustratedGuide : MonoBehaviour
{
    public GameObject[] creaftingUIOnOff;
    public Image[] changeImage;


    #region OnOff
    public void FIreBallOnOff()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
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
        Image image = changeImage[0].GetComponent<Image>();

        // 여기에 이미지를 변경할 소스를 추가하세요
        // 예시: Resources 폴더에 있는 "new_image" 스프라이트를 사용
        Sprite newSprite = Resources.Load<Sprite>("Bookmark_Blue");

        // 이미지 컴포넌트의 소스 변경
        if (changeImage != null)
        {
            changeImage[0].sprite = newSprite;
        }

    }

    public void ProtectChangeColor()
    {
        Image image = changeImage[1].GetComponent<Image>();

        // 여기에 이미지를 변경할 소스를 추가하세요
        // 예시: Resources 폴더에 있는 "new_image" 스프라이트를 사용
        Sprite newSprite = Resources.Load<Sprite>("Bookmark_Blue");

        // 이미지 컴포넌트의 소스 변경
        if (changeImage != null)
        {
            changeImage[1].sprite = newSprite;
        }
    }


    public void IceBallChangeColor()
    {
        Image image = changeImage[2].GetComponent<Image>();

        // 여기에 이미지를 변경할 소스를 추가하세요
        // 예시: Resources 폴더에 있는 "new_image" 스프라이트를 사용
        Sprite newSprite = Resources.Load<Sprite>("Bookmark_Blue");

        // 이미지 컴포넌트의 소스 변경
        if (changeImage != null)
        {
            changeImage[2].sprite = newSprite;
        }
    }

    public void PoisonChangeColor()
    {
        Image image = changeImage[3].GetComponent<Image>();

        // 여기에 이미지를 변경할 소스를 추가하세요
        // 예시: Resources 폴더에 있는 "new_image" 스프라이트를 사용
        Sprite newSprite = Resources.Load<Sprite>("Bookmark_Blue");

        // 이미지 컴포넌트의 소스 변경
        if (changeImage != null)
        {
            changeImage[3].sprite = newSprite;
        }
    }


    public void JumpChangeColor()
    {
        Image image = changeImage[4].GetComponent<Image>();

        // 여기에 이미지를 변경할 소스를 추가하세요
        // 예시: Resources 폴더에 있는 "new_image" 스프라이트를 사용
        Sprite newSprite = Resources.Load<Sprite>("Bookmark_Blue");

        // 이미지 컴포넌트의 소스 변경
        if (changeImage != null)
        {
            changeImage[4].sprite = newSprite;
        }
    }



    public void HillChangeColor()
    {
        Image image = changeImage[5].GetComponent<Image>();

        // 여기에 이미지를 변경할 소스를 추가하세요
        // 예시: Resources 폴더에 있는 "new_image" 스프라이트를 사용
        Sprite newSprite = Resources.Load<Sprite>("Bookmark_Blue");

        // 이미지 컴포넌트의 소스 변경
        if (changeImage != null)
        {
            changeImage[5].sprite = newSprite;
        }
    }

    #endregion
}

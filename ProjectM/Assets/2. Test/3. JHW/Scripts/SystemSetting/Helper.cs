using UnityEngine;

public class Helper : MonoBehaviour
{

    public GameObject[] onOffInfoUI;

    public GameObject[] inControlUI;

    public GameObject[] inCreafingUI;

    int controlUIPage = 0;




    private void Start()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            onOffInfoUI[i].SetActive(false);
        }
    }



    public void ControlKeyUIOnOff()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            if (i == 0)
            {
                onOffInfoUI[i].SetActive(true);
            }
            else
            {
                onOffInfoUI[i].SetActive(false);
            }
        }
    }

    public void DayNightUIOnOff()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            if (i == 1)
            {
                onOffInfoUI[i].SetActive(true);
            }
            else
            {
                onOffInfoUI[i].SetActive(false);
            }
        }
    }


    public void SaveSkipUIOnOff()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            if (i == 2)
            {
                onOffInfoUI[i].SetActive(true);
            }
            else
            {
                onOffInfoUI[i].SetActive(false);
            }
        }
    }



    public void MagicUseUIOnOff()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            if (i == 3)
            {
                onOffInfoUI[i].SetActive(true);
            }
            else
            {
                onOffInfoUI[i].SetActive(false);
            }
        }
    }



    public void CollectionUIOnOff()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            if (i == 4)
            {
                onOffInfoUI[i].SetActive(true);
            }
            else
            {
                onOffInfoUI[i].SetActive(false);
            }
        }
    }



    public void CreaftingUIOnOff()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            if (i == 5)
            {
                onOffInfoUI[i].SetActive(true);
            }
            else
            {
                onOffInfoUI[i].SetActive(false);
            }
        }
    }



    public void ItemUseUIOnOff()
    {
        for (int i = 0; i < onOffInfoUI.Length; i++)
        {
            if (i == 6)
            {
                onOffInfoUI[i].SetActive(true);
            }
            else
            {
                onOffInfoUI[i].SetActive(false);
            }
        }
    }


    public void NextControlUI()
    {
        if(controlUIPage != 2)
        {
            for (int i = 0; i < inControlUI.Length; i++)
            {
                inControlUI[i].SetActive(false);
            }
        }


        if (controlUIPage >= 0 && controlUIPage <= 1)
        {
            controlUIPage = controlUIPage + 1;
            inControlUI[controlUIPage].SetActive(true);
        }
    }

    public void BackControlUI()
    {
        if(controlUIPage != 0)
        {
            for (int i = 0; i < inControlUI.Length; i++)
            {
                inControlUI[i].SetActive(false);
            }
        }


        if (controlUIPage >= 1 && controlUIPage <= 2)
        {
            controlUIPage--;
            inControlUI[controlUIPage].SetActive(true);
        }
    }



    public void NextCreaftingUI()
    {
        inCreafingUI[0].SetActive(true);
        inControlUI[1].SetActive(false);
    }

    public void BackCreaftingUI()
    {
        inCreafingUI[0].SetActive(false);
        inControlUI[1].SetActive(true);
    }
}

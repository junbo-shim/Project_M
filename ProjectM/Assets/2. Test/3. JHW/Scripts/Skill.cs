using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    CreaftingUI creaftingUI;
    Inventory inventory;

    public GameObject[] EnhanceButton;


    private void Start()
    {
        creaftingUI = FindAnyObjectByType<CreaftingUI>();
        inventory = FindAnyObjectByType<Inventory>();


        for(int i = 0; i < 4; i ++)
        {
            EnhanceButton[i].SetActive(false);
        }
    }


    public void FireBallSkill()
    {
        EnhanceButton[0].SetActive(true);
        creaftingUI.creaftingButton[0].SetActive(false) ;

        

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "Battery")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 4;
            }
        }
    }


    public void EnhanceFireBallSkill()
    {
        EnhanceButton[0].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "Battery")
            {
                inventory.items[i].itemCount = 0;
            }
        }
    }



    public void RazerSkill()
    {
        EnhanceButton[1].SetActive(true);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "Ladder")
            {
                inventory.items[i].itemCount = inventory.items[i].itemCount - 4;
            }
        }
    }

    public void IceBulletSkill()
    {
        EnhanceButton[2].SetActive(true);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "SleepBag")
            {
                inventory.items.RemoveAt(i);
                i--;
            }
        }
    }

    public void PosionSkill()
    {
        EnhanceButton[3].SetActive(true);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "Supply")
            {
                inventory.items.RemoveAt(i);
                i--;
            }
        }
    }

}
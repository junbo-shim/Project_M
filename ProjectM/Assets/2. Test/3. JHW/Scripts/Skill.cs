using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    CreaftingUI creaftingUI;
    Inventory inventory;

    public GameObject[] mySkill;

    public GameObject skillPanel;
    bool activeSkill = false;


    private void Start()
    {
        creaftingUI = FindAnyObjectByType<CreaftingUI>();
        inventory = FindAnyObjectByType<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            activeSkill = !activeSkill;
            skillPanel.SetActive(activeSkill);
        }
    }

    public void FireBallSkill()
    {
        mySkill[0].SetActive(true);
        creaftingUI.creaftingUI[0].SetActive(false);

        for(int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "Battery")
            {
                inventory.items.RemoveAt(i);
                i--;
            }
        }
    }

    public void RazerSkill()
    {
        mySkill[1].SetActive(true);
        creaftingUI.creaftingUI[1].SetActive(false);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "Ladder")
            {
                inventory.items.RemoveAt(i);
                i--;
            }
        }
    }

    public void IceBulletSkill()
    {
        mySkill[2].SetActive(true);
        creaftingUI.creaftingUI[2].SetActive(false);

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
        mySkill[3].SetActive(true);
        creaftingUI.creaftingUI[3].SetActive(false);

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
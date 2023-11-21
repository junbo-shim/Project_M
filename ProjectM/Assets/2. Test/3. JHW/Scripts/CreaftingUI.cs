using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreaftingUI : MonoBehaviour
{
    Inventory inventory;
    Skill skill;

    public Text[] text;
    public GameObject[] creaftingButton;

    public Transform slotHolder;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        skill = FindAnyObjectByType<Skill>();


        for (int i = 0; i < 4; i++)
        {
            creaftingButton[i].SetActive(false);
        }
    }


    // Text에 현재 숫자를 업데이트하는 메소드
    private void UpdateNumberText()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].itemName == "Battery")
            {
                text[0].text = inventory.items[i].itemCount.ToString() + "/4";
                text[1].text = inventory.items[i].itemCount.ToString() + "/4";
            }
            else if (inventory.items[i].itemName == "Ladder")
            {
                text[2].text = inventory.items[i].itemCount.ToString() + "/4";
                text[3].text = inventory.items[i].itemCount.ToString() + "/4";
            }
            else if (inventory.items[i].itemName == "SleepBag")
            {
                text[4].text = inventory.items[i].itemCount.ToString() + "/4";
                text[5].text = inventory.items[i].itemCount.ToString() + "/4";
            }
            else if (inventory.items[i].itemName == "Supply")
            {
                text[6].text = inventory.items[i].itemCount.ToString() + "/4";
                text[7].text = inventory.items[i].itemCount.ToString() + "/4";
            }
        }
    }



    private void Update()
    {
        UpdateNumberText();
        CreafringItem("Battery");
        CreafringItem("Ladder");
        CreafringItem("SleepBag");
        CreafringItem("Supply");
    }

    private void CreafringItem(string itemName)
    {

        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);

        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[itemIndex].itemCount >= 4 )
            {
                creaftingButton[i].SetActive(true);
            }

        }

    }
}

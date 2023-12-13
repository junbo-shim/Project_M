using BNG;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;



public class CreaftingItem : MonoBehaviour
{
    public Inventory inventory;

    public GameObject[] creaftingItemButton;

    public Text[] countNeedItem;


    public void ScareCrowFindNeedItem(string itemName, string itemName2, int buttonIndex)
    {
        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);
        int itemIndex2 = inventory.items.FindIndex(item => item.itemName == itemName2);
        Debug.Log(itemIndex);
        Debug.Log(itemIndex2);

        if (itemIndex != -1 && itemIndex2 != -1)
        {
            if (inventory.items[itemIndex].itemCount >= 2 && inventory.items[itemIndex2].itemCount >= 4)
            {
                creaftingItemButton[buttonIndex].SetActive(true);
            }
            else if (inventory.items[itemIndex].itemCount < 2 || inventory.items[itemIndex2].itemCount < 4)
            {
                creaftingItemButton[buttonIndex].SetActive(false);
            }
        }
        else
        {
            return;
        }
    }


    public void TrabFindNeedItem(string itemName, string itemName2, int buttonIndex)
    {


        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);
        int itemIndex2 = inventory.items.FindIndex(item => item.itemName == itemName2);
        Debug.Log(itemIndex);
        Debug.Log(itemIndex2);

        if (itemIndex != -1 && itemIndex2 != -1)
        {
            if (inventory.items[itemIndex].itemCount >= 2 && inventory.items[itemIndex2].itemCount >= 4)
            {
                creaftingItemButton[buttonIndex].SetActive(true);
            }
            else if (inventory.items[itemIndex].itemCount < 2 || inventory.items[itemIndex2].itemCount < 4)
            {
                creaftingItemButton[buttonIndex].SetActive(false);
            }
        }
        else
        {
            return;
        }
    }



    public void ScareCrowDisCountNeedItem(string itemName, string itemName2)
    {
        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);
        int itemIndex2 = inventory.items.FindIndex(item => item.itemName == itemName2);

        inventory.items[itemIndex].itemCount = inventory.items[itemIndex].itemCount - 2;
        inventory.items[itemIndex2].itemCount = inventory.items[itemIndex2].itemCount - 4;

        UpdateNumberText("MagicEssence", countNeedItem[0]);
        UpdateNumberText("Wood", countNeedItem[1]);
        UpdateNumberText("MagicEssence", countNeedItem[2]);
        UpdateNumberText("Steel", countNeedItem[3]);


        ScareCrowFindNeedItem("MagicEssence", "Wood", 0);
        TrabFindNeedItem("MagicEssence", "Steel", 1);
    }


    public void TrabDisCountNeedItem(string itemName, string itemName2)
    {
        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);
        int itemIndex2 = inventory.items.FindIndex(item => item.itemName == itemName2);

        inventory.items[itemIndex].itemCount = inventory.items[itemIndex].itemCount - 2;
        inventory.items[itemIndex2].itemCount = inventory.items[itemIndex2].itemCount - 4;

        UpdateNumberText("MagicEssence", countNeedItem[0]);
        UpdateNumberText("Wood", countNeedItem[1]);
        UpdateNumberText("MagicEssence", countNeedItem[2]);
        UpdateNumberText("Steel", countNeedItem[3]);

        ScareCrowFindNeedItem("MagicEssence", "Wood", 0);
        TrabFindNeedItem("MagicEssence", "Steel", 1);
    }

    public void CreaftingScareCrowButton()
    {
        ScareCrowDisCountNeedItem("MagicEssence", "Wood");
    }

    public void CreaftingTrabButton()
    {
        TrabDisCountNeedItem("MagicEssence", "Steel");
    }



    // Text에 현재 숫자를 업데이트하는 메소드
    public void UpdateNumberText(string itemName, Text text1)
    {
        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);

        Item item = inventory.items.Find(i => i.itemName == itemName);

        if (item == null)
        {
            return;
        }

        else
        {
            if (inventory.items[itemIndex].itemName != "MagicEssence")
            {
                text1.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 4;
            }
            else if (inventory.items[itemIndex].itemName == "MagicEssence")
            {
                text1.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 2;
            }
        }
    }
}

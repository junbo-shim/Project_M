using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreaftingUI : MonoBehaviour
{
    Inventory inventory;


    public List<string> haveItem;

    public Transform slotHolder;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }


    // Text에 현재 숫자를 업데이트하는 메소드
    public void UpdateNumberText(string itemName, Text text1, Text text2)
    {
        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);
        Item item = inventory.items.Find(i => i.itemName == itemName);

        if (item == null)
        {
            return;
        }

        else
        {
            if(item.itemName == "JumpRecipe")
            {
                text1.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 2;
                text2.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 2;
            }
            else
            {
                text1.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 3;
                text2.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 3;
            }
        }


    }

}

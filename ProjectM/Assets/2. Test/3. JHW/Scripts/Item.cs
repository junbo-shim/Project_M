using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Etc

}


[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public int itemCount;

    //Inventory inventory;
    //private int[] currentNumbers; // 각 아이템에 대한 개수 배열

    public bool Use()
    {
        return false;
    }

}

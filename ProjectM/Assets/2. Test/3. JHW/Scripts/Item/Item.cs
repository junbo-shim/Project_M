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
    public GameObject dropItem;
    public int itemCount;


    public bool Use()
    {
        return false;
    }

}

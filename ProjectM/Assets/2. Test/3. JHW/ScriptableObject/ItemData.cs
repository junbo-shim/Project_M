using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDataType
{
    public int ID;
    public string itemName;
    public int count;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = 4)]

public class ItemData : ScriptableObject
{
    public List<ItemDataType> ItemDataList = new List<ItemDataType>();

}


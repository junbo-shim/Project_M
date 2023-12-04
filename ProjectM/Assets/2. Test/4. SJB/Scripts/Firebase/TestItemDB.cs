using System.Collections.Generic;
using UnityEngine;

public class TestItemDB : MonoBehaviour
{
    public Dictionary<string, Item> testItemDB;

    private Item apple;
    private Item sword;
    private Item shield;
    private Item bearDoll;


    private void Start()
    {
        InitDB();
    }

    private void InitDB() 
    {
        testItemDB = new Dictionary<string, Item>();

        apple = new Item();
        apple.itemName = "Apple";
        apple.itemType = ItemType.Consumables;

        testItemDB.Add(apple.itemName, apple);

        sword = new Item();
        sword.itemName = "Sword";
        sword.itemType = ItemType.Equipment;

        testItemDB.Add(sword.itemName, sword);

        shield = new Item();
        shield.itemName = "Shield";
        shield.itemType = ItemType.Equipment;

        testItemDB.Add(shield.itemName, shield);

        bearDoll = new Item();
        bearDoll.itemName = "Bear Doll";
        bearDoll.itemType = ItemType.Etc;

        testItemDB.Add(bearDoll.itemName, bearDoll);
    }
}

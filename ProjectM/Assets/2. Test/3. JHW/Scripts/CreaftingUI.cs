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
    public List<string> haveItem;

    public Transform slotHolder;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        skill = FindAnyObjectByType<Skill>();
    }


    // Text에 현재 숫자를 업데이트하는 메소드
    private void UpdateNumberText(string itemName, Text text1, Text text2)
    {
        int itemIndex = inventory.items.FindIndex(item => item.itemName == itemName);
        Item item = inventory.items.Find(i => i.itemName == itemName);

        if (item == null)
        {
            return;
        }

        else
        {
            text1.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 4;
            text2.text = inventory.items[itemIndex].itemCount.ToString() + "/" + 4;
        }


    }

    private void FindItemName()
    {
        UpdateNumberText("FIreBallRecipe", text[0], text[1]);
        UpdateNumberText("ProtectRecipe", text[2], text[3]);
        UpdateNumberText("IceBallRecipe", text[4], text[5]);
        UpdateNumberText("PoisonRecipe", text[6], text[7]);
        UpdateNumberText("JumpRecipe", text[8], text[9]);
        UpdateNumberText("HillRecipe", text[10], text[11]);
        //UpdateNumberText("SymbolStone_A", text[12], text[13]);
        //UpdateNumberText("SymbolStone_B", text[14], text[15]);
        //UpdateNumberText("MedicinalHerb", text[16], text[17]);
        //UpdateNumberText("Steel", text[18], text[19]);
        //UpdateNumberText("Wood", text[20], text[21]);
        //UpdateNumberText("MagicEssence", text[22], text[23]);
        //UpdateNumberText("ScareCrow", text[24], text[25]);
        //UpdateNumberText("Trap", text[26], text[27]);
    }



    private void Update()
    {
        FindItemName();
    }
}

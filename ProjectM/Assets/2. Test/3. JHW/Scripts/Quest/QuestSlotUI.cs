using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestSlotUI : MonoBehaviour
{
    Quest quest;
    public Text[] text;

    public Slot[] slots;

    public Transform slotHolder;

    private void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();
        quest.onSlotCountChange += SlotChange;
        quest.onChangeItem += RedrawSlotUI;
    }

    private void Update()
    {
        RedrawSlotUI();
    }


    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < quest.QuestSlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }



    void RedrawSlotUI()
    {

        //for (int i = 0; i < slots.Length; ++i)
        //{                     
        //    slots[i].RemoveSlot();
        //}

        //for (int i = 0; i < quest.items.Count; i++)
        //{
        //    slots[i].item = quest.items[i];
        //    slots[i].UpdateSlotUI();

        //}

    }
}

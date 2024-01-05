using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Text[] text;
    public Text[] InvItemName;


    public Slot[] slots;

    public Transform slotHolder;

    private void Start()
    {
        inventory = Inventory.Instance;

        slots = slotHolder.GetComponentsInChildren<Slot>();
        inventory.onSlotCountChange += SlotChange;
        inventory.onChangeItem += RedrawSlotUI;
    }

    private void Update()
    {
        RedrawSlotUI();
    }


    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.SlotCnt)
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

        for (int i = 0; i < slots.Length; ++i)
        {
            for (int j = 0; j < inventory.items.Count; j++)
            {
                if (inventory.items[j].itemCount == 0)
                {
                    inventory.items.Remove(inventory.items[j]);
                }
            }
            slots[i].RemoveSlot();

        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();

            //슬롯에 해당하는 아이템의 itemCount를 표시
            UpdateNumberText(i, inventory.items[i].itemCount);
            UpdateItemName(i, inventory.items[i].itemName);
        }

    }



    // Text에 현재 숫자를 업데이트
    private void UpdateNumberText(int index, int count)
    {
        if (text[index] != null)
        {
            text[index].text = count.ToString();
        }
    }

    private void UpdateItemName(int index, string name)
    {
        if (InvItemName[index] != null)
        {
            InvItemName[index].text = name;
        }
    }
}

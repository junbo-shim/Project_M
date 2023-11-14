using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

    public CanvasGroup canvasGroup;
    private bool openIvUI = true;


    private void Start()
    {
        inventory = Inventory.Instance;

        slots = slotHolder.GetComponentsInChildren<Slot>();
        inventory.onSlotCountChange += SlotChange;
        inventory.onChangeItem += RedrawSlotUI;
        //inventoryPanel.SetActive(activeInventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //activeInventory = !activeInventory;
            //inventoryPanel.SetActive(activeInventory);

            if(openIvUI == true)
            {
                inventoryPanel.GetComponent<CanvasGroup>().alpha = 1;
                openIvUI = false;
            }
            else
            {
                inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
                openIvUI = true;
            }
            
        }

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

    public void AddSlot()
    {
        inventory.SlotCnt++;
    }


    void RedrawSlotUI()
    {

        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}

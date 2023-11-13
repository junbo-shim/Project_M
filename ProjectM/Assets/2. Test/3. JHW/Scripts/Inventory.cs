using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance;
    public delegate void OnSlotCountChange(int val);  //대리자 정의
    public OnSlotCountChange onSlotCountChange;   //대리자 인스턴스화

    private int slotCnt;

    public List<Item> items = new List<Item>();

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }



    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        SlotCnt = 4;
    }

    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCnt)
        {
            items.Add(_item);
            if (onChangeItem != null)
            {
                onChangeItem.Invoke();
            }
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FieldItem"))
        {
            FieldItem fIeldItem = other.GetComponent<FieldItem>();
            if (AddItem(fIeldItem.GetItem()))
            {
                fIeldItem.DestroyItem();
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance;
    public delegate void OnSlotCountChange(int val);  //대리자 정의
    public OnSlotCountChange onSlotCountChange;   //대리자 인스턴스화
    public Skill skill;

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


    void Start()
    {
        SlotCnt = 32;
    }

    public bool AddItem(Item _item)
    {

        if (items.Count < SlotCnt)
        {
            // 인벤토리에 이미 해당 아이템이 있는지 확인
            Item existingItem = items.Find(item => item.itemName == _item.itemName);

            if (existingItem != null)
            {
                // 아이템이 이미 존재하면 itemCount를 증가시킴
                existingItem.itemCount++;
            }
            else
            {
                // 아이템이 존재하지 않으면 itemCount를 1로 설정하고 인벤토리에 추가함
                _item.itemCount = 1; // 초기 개수를 1로 설정
                items.Add(_item);
            }

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
                skill.CreafringSkill(fIeldItem.item.itemName);
                fIeldItem.DestroyItem();
            }
        }
    }


}

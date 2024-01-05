using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public CreaftingUI creaftingUI;
    public static Inventory Instance;
    public delegate void OnSlotCountChange(int val);  //대리자 정의
    public OnSlotCountChange onSlotCountChange;   //대리자 인스턴스화
    public Skill skill;
    public CreaftingItem creaftingItem;
    public Text[] countNeedItem;
    public Text[] text;

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

                Debug.Log("준보형 이거 이상해");

                skill.CreafringSkill(fIeldItem.item.itemName);

                creaftingItem.ScareCrowFindNeedItem("MagicEssence", "Wood", 0);
                creaftingItem.TrabFindNeedItem("MagicEssence", "Steel", 1);

                creaftingItem.UpdateNumberText("MagicEssence", countNeedItem[0]);
                creaftingItem.UpdateNumberText("Wood", countNeedItem[1]);
                creaftingItem.UpdateNumberText("MagicEssence", countNeedItem[2]);
                creaftingItem.UpdateNumberText("Steel", countNeedItem[3]);


                creaftingUI.UpdateNumberText("FIreBallRecipe", text[0], text[1]);
                creaftingUI.UpdateNumberText("ProtectRecipe", text[2], text[3]);
                creaftingUI.UpdateNumberText("IceBallRecipe", text[4], text[5]);
                creaftingUI.UpdateNumberText("PoisonRecipe", text[6], text[7]);
                creaftingUI.UpdateNumberText("JumpRecipe", text[8], text[9]);
                creaftingUI.UpdateNumberText("HillRecipe", text[10], text[11]);

                fIeldItem.DestroyItem();
            }
        }
    }
}

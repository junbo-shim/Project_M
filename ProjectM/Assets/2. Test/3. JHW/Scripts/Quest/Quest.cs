using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour
{
    public static Quest Instance;
    public delegate void OnSlotCountChange(int val);  //대리자 정의
    public OnSlotCountChange onSlotCountChange;   //대리자 인스턴스화
    private int questSlotCnt;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;


    public List<Item> myQuest = new List<Item>();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }



    public int QuestSlotCnt
    {
        get => questSlotCnt;
        set
        {
            questSlotCnt = value;
        }
    }


    void Start()
    {
        questSlotCnt = 32;
    }
}

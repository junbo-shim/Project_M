using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestSlot : MonoBehaviour
{
    public Item item;
    public Text questText;

    public void Start()
    {
        //itemIcon.gameObject.SetActive(false);

    }


    public void UpdateSlotUI()
    {
        //itemIcon.sprite = item.itemImage;
        //itemIcon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        //item = null;
        //itemIcon.gameObject.SetActive(false);
    }
}

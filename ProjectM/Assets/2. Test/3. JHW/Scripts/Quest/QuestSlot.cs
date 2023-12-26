using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestSlot : MonoBehaviour
{

    public Text questText;

    public void Start()
    {
        questText.gameObject.SetActive(false);

    }


    public void UpdateSlotUI()
    {
        var asd = QuestMananger.instance.playerQuest;
        foreach (var data in asd)
        {
            questText.text = data.Value.QuestNameKey;
            questText.gameObject.SetActive(true);
        }
    }

    public void RemoveSlot()
    {
        questText = null;
        questText.gameObject.SetActive(false);
    }
}

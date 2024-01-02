using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour
{

    public List<Text> questTextList = new List<Text>();
    public List<GameObject> questSlotList = new List<GameObject>();


    public void Start()
    {
        for (int i = 0; i < questTextList.Count; i++)
        {
            questTextList[i].gameObject.SetActive(false);
        }

    }

    public void Update()
    {
        UpdateSlotUI(QuestMananger.instance.playerQuest.Keys);
    }

    public void UpdateSlotUI(IEnumerable<string> questKeys)
    {



        // 현재의 questTextList와 questSlotList를 모두 비활성화
        foreach (var questText in questTextList)
        {
            questText.gameObject.SetActive(false);
        }

        foreach (var questSlot in questSlotList)
        {
            questSlot.SetActive(false);
        }


        // 완료된 퀘스트를 저장할 임시 리스트
        List<string> completedQuests = new List<string>();

        // 모든 퀘스트를 순회하면서 완료된 퀘스트는 completedQuests에 추가
        foreach (var questKey in questKeys)
        {
            if (QuestMananger.instance.playerQuest[questKey].State == QuestState.QuestStatus.Completed)
            {
                completedQuests.Add(questKey);
            }
        }

        // 완료된 퀘스트를 현재 리스트에서 제거하고 맨 끝에 추가
        foreach (var completedQuest in completedQuests)
        {
            questKeys = questKeys.Where(key => key != completedQuest);
            questKeys = questKeys.Append(completedQuest);
        }



        int i = 0;


        foreach (var questKey in questKeys)
        {
            if (i < questTextList.Count)
            {
                questTextList[i].text = QuestMananger.instance.playerQuest[questKey].QuestNameKey;
                questSlotList[i].GetComponent<QuestButton>().questID = questKey;



                // 완료되지 않은 퀘스트에는 투명도를 설정
                if (!completedQuests.Contains(questKey))
                {
                    Color changeColor = questTextList[i].color;
                    changeColor.a = 1.0f;
                    questTextList[i].color = changeColor;
                }

                if (QuestMananger.instance.playerQuest[questKey].State == QuestState.QuestStatus.Completed)
                {
                    Color ChangeColor = questTextList[i].color;
                    ChangeColor.a = 0.3f;
                    questTextList[i].color = ChangeColor;

                    Button button = questTextList[i].transform.parent.GetComponent<Button>();
                    if (button != null)
                    {
                        Color buttonColor = button.image.color;
                        buttonColor.a = 0.3f;
                        button.image.color = buttonColor;
                    }
                }

                questTextList[i].gameObject.SetActive(true);
                questSlotList[i].SetActive(true);

                i++;
            }
        }
    }
}
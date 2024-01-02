using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class QuestButton : MonoBehaviour
{
    public Text questContents;
    public Text questGoal;
    public Text questInfo;



    public string questID;
    private void Start()
    {
        // 각 버튼에 클릭 이벤트 핸들러 등록
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    // 각 버튼이 클릭되었을 때 호출될 함수
    public void OnButtonClick()
    {

        questGoal.text = QuestMananger.instance.playerQuest[questID].QuestExplainKey;
        questInfo.text = QuestMananger.instance.playerQuest[questID].QuestType;
        questContents.text = QuestMananger.instance.playerQuest[questID].CompletionCondition_ID;
    }

}
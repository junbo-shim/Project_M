using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestMananger : MonoBehaviour // 저장할 스크립트
{

    public Dictionary<string, BasicQuest> playerQuest;  // 플레이어가 수락한 퀘스트 정보

    private static QuestMananger Instance;

    public static QuestMananger instance
    {
        get
        {
            if (Instance == null)
            {

                GameObject singletonObject = new GameObject("QuestMananger");
                Instance = singletonObject.AddComponent<QuestMananger>();
            }
            return Instance;
        }
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
        playerQuest = new Dictionary<string, BasicQuest>();
    }
    
    public void AddPlayerQuest(string str) // 퀘스트 수주
    {
        if(!playerQuest.ContainsKey(str))
        {
            if(CSVRead.instance.QuestDatas.ContainsKey(str))
            {
                var questInstance = CSVRead.instance.QuestDatas[str];
                BasicQuest basicQuest = new BasicQuest(questInstance.Id , questInstance.QuestType, questInstance.QuestNameKey, questInstance.QuestExplainKey,questInstance.CompletionConditionID
                    ,questInstance.QuestNPCSuccessID ,questInstance.PrecedeQuestID , questInstance.Reward_ID);
                playerQuest.Add(str, basicQuest);
                playerQuest[str].Start();
            }
            Debug.Log(playerQuest[str].State);
        }
       // Debug.Log(playerQuest.First().Value.QuestProgressDialogue);
    }

    public void QuestComplete(string str) // 퀘스트 완료
    {
        if (!playerQuest.ContainsKey(str))
        {
            playerQuest[str].Complete();
    
        }
        
    }
    public void QuestCompleteChk(string str) // 퀘스트 조건확인
    {
        if (!playerQuest.ContainsKey(str))
        {
            playerQuest[str].Complete();

        }

    }

}

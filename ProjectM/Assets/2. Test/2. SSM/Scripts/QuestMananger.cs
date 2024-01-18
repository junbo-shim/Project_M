using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class QuestMananger : MonoBehaviour // 저장할 스크립트
{

    public Dictionary<string, BasicQuest> playerQuest;  // 플레이어가 수락한 퀘스트 정보
    public Dictionary<string, bool> intetactionQuest;
    private string[] fruits; // 자른문자저장용
    public GameObject gameobj1;
    public GameObject gameobj2;
    private StringBuilder fruitsSb;                       // 자른문자 합치는용도 스트링 빌드
    private static QuestMananger Instance;
    Quest quest;

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
      
        intetactionQuest = new Dictionary<string, bool>();
        playerQuest = new Dictionary<string, BasicQuest>();
        fruitsSb = new StringBuilder();



        quest = FindAnyObjectByType<Quest>();
    }

    public void intetactionQuestAdd(string str)
    {
        if (!intetactionQuest.ContainsKey(str))
        {
            intetactionQuest.Add(str, true);

          
        }
    }
    public void AddPlayerQuest(string str) // 퀘스트 수주
    {
        if (!playerQuest.ContainsKey(str))
        {

            if (CSVRead.instance.QuestDatas.ContainsKey(str))
            {
                var questInstance = CSVRead.instance.QuestDatas[str];
                BasicQuest basicQuest = new BasicQuest(questInstance.Id, questInstance.QuestType, questInstance.QuestNameKey, questInstance.QuestExplainKey, questInstance.QuestGoalKey, questInstance.CompletionConditionID
                    , questInstance.QuestNPCSuccessID, questInstance.PrecedeQuestID, questInstance.Reward_ID); // CompletionConditionID는 타입 : vlaue(코드 id)로 구성되어서 string임
                playerQuest.Add(str, basicQuest);
                playerQuest[str].Start(); // 상태를 퀘스트 수락상태로 변경
                quest.UpdateSlotUI(QuestMananger.instance.playerQuest.Keys);

            }
            if(str == "100005")
            {
                gameobj1.SetActive(true);
                ParticleSystem particleSystem = gameobj1.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                    gameobj2.gameObject.SetActive(true);
                }
            }
        



            // Debug.Log(playerQuest[str].QuestNameKey);
            // Debug.Log(playerQuest[str].State);
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
    public void QusetCompletionConditionChk(string str, int npcId) // 퀘스트 조건확인
    {

        if (playerQuest.ContainsKey(str))
        {

            if (playerQuest[str].State == QuestState.QuestStatus.InProgress)
            {

                if (QusetCompletionCondition(str, npcId))
                {

                    playerQuest[str].Complete();
                    quest.UpdateSlotUI(QuestMananger.instance.playerQuest.Keys);


                }

            }
        }
    }

    public bool QusetCompletionCondition(string str, int npcId)
    {
        fruitsSb.Clear();
        fruitsSb.Append(playerQuest[str].CompletionCondition_ID);
        Debug.Log(fruitsSb.ToString());
        fruits = fruitsSb.ToString().Split(":"); // 스트링빌드 대화 태그순서 , 대화 순서 분리

        switch (fruits[0])
        {
            case "NPC_Talk":
                if (fruits[1].Equals(npcId.ToString()))
                {
                   

                    return true;
                }
               

                        break;
            case "Level":



                if (intetactionQuest.ContainsKey(CSVRead.instance.CompletionConditionDatas[CSVRead.instance.QuestDatas[str].CompletionConditionID.ToString()].Value))
                {

                    return true;
                }


                break;
        }
        return false;
    }

}

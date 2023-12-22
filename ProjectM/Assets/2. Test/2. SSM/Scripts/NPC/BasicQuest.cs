using System;
using System.Collections.Generic;

public class BasicQuest : QuestState
{
    public BasicQuest(int id, string questType , string quest_Name_Key, string quest_Explain_Key, int completionCondition_ID, int successNPC_ID, int precedeQuest_ID, int reward_ID)
    {
        ID = id;
        QuestType = questType;
        QuestNameKey = quest_Name_Key;
        QuestExplainKey = quest_Explain_Key;
        CompletionCondition_ID = completionCondition_ID;                             
        SuccessNPC_ID = successNPC_ID;
        PrecedeQuest_ID = precedeQuest_ID;
        Reward_ID = reward_ID;
    }

    public override void Start()
    {
        // 퀘스트가 시작되었음을 알립니다.
        _state = QuestStatus.InProgress;
    }

    public override void Complete()
    {
        // 퀘스트가 완료되었음을 알립니다.
        _state = QuestStatus.Completed;
    }                          

    public int ID; // id

    public string QuestType; //퀘스트 타입

    public string QuestNameKey;//퀘스트 이름

    public string QuestExplainKey; // 퀘스트 목표 상세

    public int CompletionCondition_ID; // 완료조건

    public int SuccessNPC_ID; //완료NPC

    public int PrecedeQuest_ID; // 선행퀘스트

    public int Reward_ID; //보상

}
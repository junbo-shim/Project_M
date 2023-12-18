using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class CSVRead : MonoBehaviour
{
    #region 프로퍼티 및 변수
  
    // 
    private static CSVRead Instance;

    public static CSVRead instance
    {
        get
        {
            if (Instance == null)
            {

                GameObject singletonObject = new GameObject("CSVRead");
                Instance = singletonObject.AddComponent<CSVRead>();
            }
            return Instance;
        }
    }

    public Dictionary<string,NPCData> nPCDatas; //데이터 저장용 딕셔너리

    public Dictionary<string, NPCSelectTalkData> npcSelectTalkDatas; // npc 선택지 저장용 딕셔너리

    public Dictionary<string, QuestData> QuestDatas;

    #endregion   
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
        nPCDatas = new Dictionary<string, NPCData>();
        npcSelectTalkDatas = new Dictionary<string, NPCSelectTalkData>();
        QuestDatas = new Dictionary<string, QuestData>();
        //
        csvRead();
    
    }
    #region csv읽는용
    public void csvRead()
    {
        
        // NPCcsv 읽어오기
        List<Dictionary<string, object>> csv_Data = CSVReader.Read("NPC"); 
        // 로그로 출력
     
        foreach (Dictionary<string, object> data in csv_Data) // 딕셔너리를 읽어옴
        {            
            NPCData npcData = new NPCData();
          
            setDict(data); //npc 데이터 딕셔너리에 set
            
        }
        // NPC_Select_Csv_Data.csv 읽어오기
        List<Dictionary<string, object>> NPC_Select_Csv_Data = CSVReader.Read("NPC_Select_Csv_Data");
        // 로그로 출력

        foreach (Dictionary<string, object> data in NPC_Select_Csv_Data) //딕셔너리 읽어옴
        {
           
            NpcSelectSetDict(data); //npc 선택지데이터 딕셔너리에 set

        }
        List<Dictionary<string, object>> QuestDataCSV = CSVReader.Read("QuestDataCSV");
        // 로그로 출력

        foreach (Dictionary<string, object> data in QuestDataCSV) //딕셔너리 읽어옴
        {

            QuestDict(data); //npc 선택지데이터 딕셔너리에 set

        }
        // ListPrint();
    }
    #endregion
    #region 딕션너리 데이터 확인용
    public void ListPrint() // 데이터 확인용
    {
        foreach (var data in nPCDatas)
        {
            Debug.Log(data.Key);
            Debug.Log(data.Value.Description);
        }
    }
    #endregion
    #region npc데이터 딕셔너리저장
    public void setDict(Dictionary<string, object> dict) // 딕셔너리 데이터 넣기 용
    {

    
        string id = GetValue<int>(dict, "NPC_ID").ToString();
        // 딕셔너리에 해당 키에 대응하는 NPCData 객체가 없으면 생성
        if (!nPCDatas.ContainsKey(id))
        {
            nPCDatas[id] = new NPCData();
        } 
        nPCDatas[id].NPC_ID = GetValue<int>(dict, "NPC_ID");
        nPCDatas[id].Description = GetValue<string>(dict, "Description");
        nPCDatas[id].Type = GetValue<string>(dict, "Type");
        nPCDatas[id].Name = GetValue<string>(dict, "Name");
        nPCDatas[id].Hp = GetValue<int>(dict, "Hp");
        nPCDatas[id].CatchPossibility = GetValue<bool>(dict,"CatchPossibility");
        nPCDatas[id].Icon = GetValue<string>(dict, "Icon");
        

    }
    #endregion

    #region npc선택지 데이터 딕셔너리저장
    public void NpcSelectSetDict(Dictionary<string, object> dict) // 딕셔너리 데이터 넣기 용
    {

        string id = GetValue<int>(dict, "ID").ToString();
        // 딕셔너리에 해당 키에 대응하는 NPCSelectTalkData 객체가 없으면 생성
        if (!npcSelectTalkDatas.ContainsKey(id))
        {
            npcSelectTalkDatas[id] = new NPCSelectTalkData();
        }
        npcSelectTalkDatas[id].Id = GetValue<int>(dict, "ID");
        npcSelectTalkDatas[id].NPCId = GetValue<int>(dict, "NPC_ID");
        npcSelectTalkDatas[id].NextChoice_ID = GetValue<int>(dict, "NextChoice_ID");
        npcSelectTalkDatas[id].Quest_ID = GetValue<int>(dict, "Quest_ID");
        npcSelectTalkDatas[id].Choice_Before_Dialogue = GetValue<string>(dict, "Choice_Before_Dialogue");
        npcSelectTalkDatas[id].Choice_Order_Number = GetValue<int>(dict, "Choice_Order_Number");
        npcSelectTalkDatas[id].NextChoice_ID = GetValue<int>(dict, "NextChoice_ID");
        npcSelectTalkDatas[id].Choice_Text1 = GetValue<string>(dict, "Choice_Text1");
        npcSelectTalkDatas[id].Choice_Text2 = GetValue<string>(dict, "Choice_Text2");
        npcSelectTalkDatas[id].Choice_Text3 = GetValue<string>(dict, "Choice_Text3");
        npcSelectTalkDatas[id].Choice_Text4 = GetValue<string>(dict, "Choice_Text4");
        npcSelectTalkDatas[id].Choice_Text1_Answer = GetValue<string>(dict, "Choice_Text1_Answer");
        npcSelectTalkDatas[id].Choice_Text2_Answer = GetValue<string>(dict, "Choice_Text2_Answer");
        npcSelectTalkDatas[id].Choice_Text3_Answer = GetValue<string>(dict, "Choice_Text3_Answer");
        npcSelectTalkDatas[id].Choice_Text4_Answer = GetValue<string>(dict, "Choice_Text4_Answer");
        npcSelectTalkDatas[id].Mbti1_ID = GetValue<int>(dict, "MBTI1_ID");
        npcSelectTalkDatas[id].Mbti2_ID = GetValue<int>(dict, "MBTI2_ID");
        npcSelectTalkDatas[id].Mbti3_ID = GetValue<int>(dict, "MBTI3_ID");
        npcSelectTalkDatas[id].Mbti4_ID = GetValue<int>(dict, "MBTI4_ID");
        npcSelectTalkDatas[id].Talk_Priority = GetValue<int>(dict, "Talk_Priority");
    }
    #endregion
    #region 딕션너리 값을 변환할려는 데이터타입이 맞는지 확인용
    private T GetValue<T>(Dictionary<string, object> dict, string key) // 변환할려는 데이터타입이 맞는지 확인용
    {
        if (dict.TryGetValue(key, out object value) && value is T)
        {
            return (T)value;
        }
        return default(T);
    }
    #endregion

    public void QuestDict(Dictionary<string, object> dict)
    {
        string id = GetValue<int>(dict, "ID").ToString();
        // 딕셔너리에 해당 키에 대응하는 NPCSelectTalkData 객체가 없으면 생성
        if (!QuestDatas.ContainsKey(id))
        {
            QuestDatas[id] = new QuestData();
        }
      
        QuestDatas[id].Id = GetValue<int>(dict, "ID");
        QuestDatas[id].QuestType = GetValue<string>(dict, "QuestType");
        QuestDatas[id].QuestNameKey = GetValue<string>(dict, "QuestNameKey");
        QuestDatas[id].QuestGoalKey = GetValue<string>(dict, "QuestGoalKey");
        QuestDatas[id].QuestExplainKey = GetValue<string>(dict, "QuestExplainKey");
        QuestDatas[id].QuestBackgroundType = GetValue<string>(dict, "QuestBackgroundType");
        QuestDatas[id].QuestAreaType = GetValue<string>(dict, "QuestAreaType");
        QuestDatas[id].CompletionConditionID = GetValue<int>(dict, "CompletionConditionID");
        QuestDatas[id].QuestNPCSuccessID = GetValue<int>(dict, "QuestNPCSuccessID");
        QuestDatas[id].PrecedeQuestID = GetValue<int>(dict, "PrecedeQuestID");
        QuestDatas[id].LinkQuestID = GetValue<int>(dict, "LinkQuestID");
        QuestDatas[id].QuestProgressDialogue = GetValue<string>(dict, "QuestProgressDialogue");
        QuestDatas[id].Reward_ID = GetValue<int>(dict, "Reward_ID");
    
    }
}

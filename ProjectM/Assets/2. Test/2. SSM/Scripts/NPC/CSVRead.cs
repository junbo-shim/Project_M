using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public Dictionary<string, QuestData> QuestDatas; // 퀘스트 저장용 

    public Dictionary<string, CompletionConditionData> CompletionConditionDatas; // 완료 조건 저장용

    public Dictionary<string, MBTIData> MBTIDatas; // MBTI 저장용

    public Dictionary<string, QuestTitleData> QuestTitleDatas; //퀘스트 제목

    public Dictionary<string, MbtTitleData> MbtTitleDatas;// mbti 결과 제목
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
        CompletionConditionDatas = new Dictionary<string, CompletionConditionData>();
        MBTIDatas = new Dictionary<string, MBTIData>();
        QuestTitleDatas = new Dictionary<string, QuestTitleData>();
        MbtTitleDatas = new Dictionary<string, MbtTitleData>();
      //  MbtiText = new Dictionary<string,>
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

            QuestDict(data); //npc 퀘스트 딕셔너리에 set

        }

        List<Dictionary<string, object>> CompletionConditionCSV = CSVReader.Read("CompletionCondition");
        // 로그로 출력

        foreach (Dictionary<string, object> data in CompletionConditionCSV) //딕셔너리 읽어옴
        {

            CompletionConditionDict(data); //npc 완료조건 딕셔너리에 set

        }

        List<Dictionary<string, object>> MBTIDataCSV = CSVReader.Read("MBTIDB");
        // 로그로 출력

        foreach (Dictionary<string, object> data in MBTIDataCSV) //딕셔너리 읽어옴
        {

            MBTIDict(data); //npc mbti 딕셔너리에 set

        }

        List<Dictionary<string, object>> QuestTileCSV = CSVReader.Read("QuestTitleDB");
        // 로그로 출력

        foreach (Dictionary<string, object> data in QuestTileCSV) //딕셔너리 읽어옴
        {

            QuestTileDict(data); //npc 퀘스트 딕셔너리에 set

        }

        List<Dictionary<string, object>> MBTITitleCsv = CSVReader.Read("MBTITitleData");
        // 로그로 출력

        foreach (Dictionary<string, object> data in MBTITitleCsv) //딕셔너리 읽어옴
        {

            MBTITitle(data); //npc 퀘스트 딕셔너리에 set

        }
        //  ListPrint();
    }
    #endregion
    #region 딕션너리 데이터 확인용
    public void ListPrint() // 데이터 확인용
    {
        foreach (var data in QuestTitleDatas)
        {
            Debug.Log(data.Key);
            Debug.Log(data.Value.Detail);
        }
    }
    #endregion
    #region npc데이터 딕셔너리저장
    public void setDict(Dictionary<string, object> dict) // 딕셔너리 데이터 넣기 용
    {

    
        string id = GetValue<int>(dict, "ID").ToString();
     
        // 딕셔너리에 해당 키에 대응하는 NPCData 객체가 없으면 생성
        if (!nPCDatas.ContainsKey(id))
        {
        
            nPCDatas[id] = new NPCData();
        } 
        nPCDatas[id].NPC_ID = GetValue<int>(dict, "ID");
        nPCDatas[id].Description = GetValue<string>(dict, "Description");
        nPCDatas[id].Type = GetValue<string>(dict, "Type1");
        nPCDatas[id].Name = GetValue<string>(dict, "Name");
        nPCDatas[id].Hp = GetValue<int>(dict, "HP");
        nPCDatas[id].CatchPossibility = GetValue<bool>(dict,"CatchPossibility");


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
     //   Debug.Log(GetValue<int>(dict, "NPC_ID"));
        npcSelectTalkDatas[id].NextChoice_ID = GetValue<int>(dict, "NextChoice_ID");
        npcSelectTalkDatas[id].Quest_ID = GetValue<int>(dict, "Quest_ID");
        npcSelectTalkDatas[id].Choice_Before_Dialogue = GetValue<string>(dict, "Dialogue");   
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
        npcSelectTalkDatas[id].Choice_Bundle_Tag = GetValue<int>(dict, "Choice_Bundle_Tag");
   
    }
    #endregion

    public void QuestDict(Dictionary<string, object> dict)
    {
        string id = GetValue<int>(dict, "Quest_ID").ToString();
        // 딕셔너리에 해당 키에 대응하는 NPCSelectTalkData 객체가 없으면 생성
        if (!QuestDatas.ContainsKey(id))
        {
            QuestDatas[id] = new QuestData();
        }
      
        QuestDatas[id].Id = GetValue<int>(dict, "Quest_ID");
 
        QuestDatas[id].QuestType = GetValue<string>(dict, "QuestType");
        QuestDatas[id].QuestNameKey = GetValue<string>(dict, "Quest_Name_Key");
        QuestDatas[id].QuestGoalKey = GetValue<string>(dict, "Quest_Goal_Key");
        QuestDatas[id].QuestExplainKey = GetValue<string>(dict, "Quest_Explain_Key");
        QuestDatas[id].QuestBackgroundType = GetValue<string>(dict, "Quest_Background_Type");
        QuestDatas[id].QuestAreaType = GetValue<string>(dict, "Quest_Area_Type");
        QuestDatas[id].CompletionConditionID = GetValue<int>(dict, "CompletionCondition_ID");
        QuestDatas[id].QuestNPCSuccessID = GetValue<int>(dict, "SuccessNPC_ID");
        QuestDatas[id].PrecedeQuestID = GetValue<int>(dict, "PrecedeQuest_ID");
        QuestDatas[id].LinkQuestID = GetValue<int>(dict, "LinkQuest_ID");
        QuestDatas[id].QuestProgressDialogue = GetValue<string>(dict, "Quest_Progress_Dialogue");
        QuestDatas[id].Reward_ID = GetValue<int>(dict, "Reward_ID");
   
    }
    #region 딕션너리 값을 변환할려는 데이터타입이 맞는지 확인용
    // 시스템 기본 인코딩 사용
    private T GetValue<T>(Dictionary<string, object> dict, string key)
    {
        string encodedKey = Encoding.Default.GetBytes(key).ToString();
        object value;
        bool isFound = dict.TryGetValue(encodedKey, out value);

        if (!isFound)
        {
            // 원래 키로도 다시 한 번 시도 (일부 경우 인코딩으로 해결되지 않을 수 있음)
            if (!dict.TryGetValue(key, out value))
            {
                return default(T);
            }
        }

        if (value is T)
        {
            if (value is string)
            {
                return (T)Convert.ChangeType((string)value, typeof(T));
            }

            return (T)value;
        }

        else
        {
          
            if (value is int && typeof(T) != typeof(bool))
            {
                return (T)Convert.ChangeType((int)value, typeof(T));
            }
            return default(T);
        }
    }
    #endregion

    public void CompletionConditionDict(Dictionary<string, object> dict)
    {
        string id = GetValue<int>(dict, "ID").ToString();

        // 딕셔너리에 해당 키에 대응하는 NPCSelectTalkData 객체가 없으면 생성
        if (!CompletionConditionDatas.ContainsKey(id))
        {
            CompletionConditionDatas[id] = new CompletionConditionData();
        }

        CompletionConditionDatas[id].Id = GetValue<int>(dict, "ID");
        CompletionConditionDatas[id].ConditionType = GetValue<string>(dict, "Condition Type");
        CompletionConditionDatas[id].Value = GetValue<string>(dict, "Value1");
        

    }


    public void MBTIDict(Dictionary<string, object> dict)
    {
        string id = GetValue<int>(dict, "ID").ToString();
        // 딕셔너리에 해당 키에 대응하는 NPCSelectTalkData 객체가 없으면 생성
        if (!MBTIDatas.ContainsKey(id))
        {
            MBTIDatas[id] = new MBTIData();
        }

        MBTIDatas[id].Id = GetValue<int>(dict, "ID");
        MBTIDatas[id].MBTiScore_E = GetValue<int>(dict, "MBTIScore_E");    
        MBTIDatas[id].MBTiScore_I = GetValue<int>(dict, "MBTIScore_I");
        MBTIDatas[id].MBTiScore_S = GetValue<int>(dict, "MBTIScore_S");
        MBTIDatas[id].MBTiScore_N = GetValue<int>(dict, "MBTIScore_N");
        MBTIDatas[id].MBTiScore_F = GetValue<int>(dict, "MBTIScore_F");
        MBTIDatas[id].MBTiScore_T = GetValue<int>(dict, "MBTIScore_T");
        MBTIDatas[id].MBTiScore_J = GetValue<int>(dict, "MBTIScore_J");
        MBTIDatas[id].MBTiScore_P = GetValue<int>(dict, "MBTIScore_P");
    }

    public void QuestTileDict(Dictionary<string, object> dict)
    {
        string id = GetValue<string>(dict, "Key");
        // 딕셔너리에 해당 키에 대응하는 NPCSelectTalkData 객체가 없으면 생성
        if (!QuestTitleDatas.ContainsKey(id))
        {
            QuestTitleDatas[id] = new QuestTitleData();
        }
        QuestTitleDatas[id].Detail = GetValue<string>(dict, "Detail");
   
    }

    public void MBTITitle(Dictionary<string,object> dict)
    {
        string id = GetValue<int>(dict, "ID").ToString();
        // 딕셔너리에 해당 키에 대응하는 NPCSelectTalkData 객체가 없으면 생성
        if (!MbtTitleDatas.ContainsKey(id))
        {
            MbtTitleDatas[id] = new MbtTitleData();
        }
        MbtTitleDatas[id].Id = GetValue<int>(dict, "ID");
        MbtTitleDatas[id].MBtiStep = GetValue<int>(dict, "MBtiStep");
        MbtTitleDatas[id].Category1 = GetValue<string>(dict, "Category1");
        MbtTitleDatas[id].Script = GetValue<string>(dict, "Script");



    }
}

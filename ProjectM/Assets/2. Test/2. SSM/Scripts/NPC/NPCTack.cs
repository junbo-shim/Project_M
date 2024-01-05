using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class NPCTack : MonoBehaviour
{
    [SerializeField] private string npcID;                      // 내가 지정할 현재 npcId
    private Dictionary<string, List<string>> npcWoadDict;       // 50글자씩 자른 npc woard
    private List<string> npcWoad;
    private string dictFirst;                                   //npcWoadDict 첫번째 키값 저장용
    private StringBuilder dictProgressSb;                       //npcWoadDict 진행중 키값 저장용
    [SerializeField] private TextMeshProUGUI npcTextMeshPro; // npctext박스
    public Image textimage; // 대화 박스 이미지
    [SerializeField] private Image textimageReady;// 대화 박스 준비 이미지
    [SerializeField] private Image scrollbar;//스크롤바 위치찾기용
    public int textLV = 0; // 보여주는 대화 선택지 레벨 (이거에 따라 대화 변화함)
    private int dialogueLV = 0;// 퀘스트 Lv
    private bool NPCEnd= false;// 대화 종료
    private bool prerequisiteQuest=false;//선행퀘스트 완료 여부
    NPC newNPC; // npc 정보
    private string[] fruits; // 자른문자저장용
    public List<ChoiceScripts> choiceScripts;
    private List<TextMeshProUGUI> choiceObjText; // 선택지TextMeshProUGUI 리스트로 저장용
    private NPCChildSet npcChildSet; // 저장 Obj를 캐싱해서 가지고있는 클레스;
    private List<NPCSelectTalkData> dialogueData; // npc 대사,선택지 저장용
    private NpcAction npcAction; //npcAction 스크립터
    private List<int> mbtis;
    private int[] scores; // 전역 변수 선언

    public void Start()
    {
        mbtis = new List<int>();
        SetComponent(); // Component 및 기타 정보 초기화및 세팅
        SetNpcData(); // npc데이터 세팅
        NpcWord();
        
        dictFirst = npcWoadDict.First().Key; //첫번쨰 키 저장
        dictProgressSb.Append(dialogueLV == 0 ? npcWoadDict.First().Key : npcWoadDict.Keys.ElementAt(dialogueLV)); // 진행중 키값 저장용
        npcTextMeshPro.text = npcWoadDict[dictFirst][0]; // npc 대사 설정  dialogueData[j].Id.ToString() +"_"+ dialogueData[j].Choice_Order_Number .id _ 글등장 순서

    }

    private void SetComponent() // 시작 컴포넌트및 정보 세팅
    {
        npcAction = transform.GetComponent<NpcAction>();
        dictProgressSb = new StringBuilder();
        npcWoadDict = new Dictionary<string, List<string>>();
        choiceScripts = new List<ChoiceScripts>();
        choiceObjText = new List<TextMeshProUGUI>();
        npcChildSet = transform.parent.GetComponent<NPCChildSet>(); // npc 아이콘 정보 캐싱해가지고있는 스크립트
        for (int i = 0; i <= 3; i++)
        {
            choiceScripts.Add(npcChildSet.ChoiceTransform.transform.GetChild(i).GetComponent<ChoiceScripts>());
            choiceObjText.Add(npcChildSet.ChoiceTransform.transform.GetChild(i).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>());
        }

    }
    #region NPC 데이터 세팅
    private void SetNpcData() // NPC 데이터 세팅
    {

        if (CSVRead.instance.nPCDatas.ContainsKey(npcID) && CSVRead.instance.nPCDatas != null)
        {
            var npcData = CSVRead.instance.nPCDatas[npcID];// CSVRead.instance.nPCDatas[npcID]캐싱
            if (npcData != null)
            {
                NPCBase npcBase = new NPCBase();
                newNPC = npcBase.CreateNPC(npcData.NPC_ID, IconSet(npcData.Type), npcData.Name, npcData.Hp, npcData.CatchPossibility, CSVRead.instance.npcSelectTalkDatas); // npc 정보 newNPC에 세팅

            }
        }


    }

    #endregion
    public void DictLastNumberAdd() // 텍스트 순서 체인지
    {
        fruits = dictProgressSb.ToString().Split("_"); // 스트링빌드 대화 태그순서 , 대화 순서 분리
     
        if (npcWoadDict.ContainsKey(fruits[0] + "_" + (Convert.ToInt32(fruits[fruits.Length - 1]) + 1).ToString())) // npc mbti대화 textLv상승용
        {
            
              
            if (!NPCEnd) // npc 대화완료 여부체크
            {
                textLV++; // 다음 대화 
                dictProgressSb.Clear();
                npcAction.TalkiZero();// 대화 처음으로
                dictProgressSb.Append(fruits[0] + "_" + (Convert.ToInt32(fruits[fruits.Length - 1]) + 1).ToString()); // 다음 대화로
            }
            
            
            
        }
        else if (npcWoadDict.ContainsKey(fruits[0] + "_" + "99")) // 마지막 대화 존재 여부 체크
        {
            if (dictProgressSb.Equals(fruits[0] + "_" + "99")) // 마지막 대화 체크
            {
                for (int i = 0; i < mbtis.Count-1; i++)
                {
           
                    GetMBTIDatas(mbtis[i]);
                }
               
                QuestInput();
                mbtis.Clear();
                if (npcWoadDict.ContainsKey((Convert.ToInt32(fruits[0]) + 1).ToString() + "_" + "1")) // 다음 퀘스트 대화 여부 체크
                {
                    dialogueLV++;
                    dictProgressSb.Clear();// 스트링 빌드 초기화
                    npcAction.TalkiZero(); // 대화 처음으로
                    dialogueData = newNPC.ContinueDialogue(dialogueLV);
                    textLV = 0;
                    dictProgressSb.Append((Convert.ToInt32(fruits[0]) + 1).ToString() + "_" + "1"); // 다음 퀘스트 대화 스트링 빌드에 셋
                    npcAction.TalkiEnd(); // 대화창 닫기
                }
                else //없으면 대화 종료 bool true로 변경
                {
                    NPCEnd = true; 
                }
            }
            else
            {
                textLV++;
                dictProgressSb.Clear();
                dictProgressSb.Append(fruits[0].ToString() + "_" + "99"); //다음 대회 id 저장
                npcAction.TalkiZero();
              

            }
           //  npcAction.TalkiEnd();
        }



    }
    public void QuestInput() // 플레이어에게 퀘스트 발급
    {
        
        if(CSVRead.instance.QuestDatas.ContainsKey(dialogueData[textLV].Quest_ID.ToString())) // 퀘스트 id 존재여부체크 
        {
            if (!CSVRead.instance.QuestDatas[dialogueData[textLV].Quest_ID.ToString()].Equals("-1"))
            {
                QuestMananger.instance.AddPlayerQuest(dialogueData[textLV].Quest_ID.ToString());
            }
        }
       
    } 
    public void ChoiseClick(int number, string str ,int mbtiID)
    {
        for (int i = 0; i <= 3; i++)
        {
            if (npcChildSet.ChoiceTransform.transform.GetChild(i).gameObject.activeSelf)
            {
                npcChildSet.ChoiceTransform.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (str.Equals("-1"))
        {
            npcTextMeshPro.text = CSVRead.instance.QuestDatas[dialogueData[textLV].Quest_ID.ToString()].QuestProgressDialogue;
            TalkOff();
        }
        else
        {
            npcTextMeshPro.text = str;
        }
        mbtis.Add(mbtiID);
        DictLastNumberAdd(); 
    }

    public void GetMBTIDatas(int mbti_ID) // mbti 에 점수 추가
    {
      
        var mbtiDatas = CSVRead.instance.MBTIDatas[mbti_ID.ToString()];
        var playerMBTI = MBTIScripts.Instance;
        scores = new[] { mbtiDatas.MBTiScore_I, mbtiDatas.MBTiScore_N,
                         mbtiDatas.MBTiScore_S, mbtiDatas.MBTiScore_J,
                         mbtiDatas.MBTiScore_P, mbtiDatas.MBTiScore_E,
                         mbtiDatas.MBTiScore_T, mbtiDatas.MBTiScore_F };

        for (int i = 0; i < scores.Length; i++)
        {

            if (scores[i] > 0)
            {

                switch (i)
                {
                    case 0:
                        playerMBTI.MBTI_I_Add(scores[i]);
                        break;
                    case 1:
                        playerMBTI.MBTI_N_Add(scores[i]);
                        break;
                    case 2:
                        playerMBTI.MBTI_S_Add(scores[i]);
                        break;
                    case 3:
                        playerMBTI.MBTI_J_Add(scores[i]);
                        break;
                    case 4:
                        playerMBTI.MBTI_P_Add(scores[i]);
                        break;
                    case 5:
                        playerMBTI.MBTI_E_Add(scores[i]);
                        break;
                    case 6:
                        playerMBTI.MBTI_T_Add(scores[i]);
                        break;
                    case 7:
                        playerMBTI.MBTI_F_Add(scores[i]);
                        break;
                    default:
                        Debug.LogError(i + "MBTI Value Error");
                        break;
                }


            }
        }
    }


    //ss
    #region NPC 선택지
    private void SetChoiceText() // npc 선택지 세팅
    {
        if(NPCEnd == true)
        {
            return;
        }
        if (dialogueData.Count == 0)
        {
            return;
        }

        if (!dialogueData[textLV].Choice_Text1.Equals("-1"))
        {
            choiceObjText[0].text = dialogueData[textLV].Choice_Text1;// choiceObjText[0 : 몇번째?] .text =  dialogueData[0 :몇번째 대화 클래스].Choice_Text
            choiceScripts[0].id = dialogueData[textLV].Id;
            choiceScripts[0].Choice_Text_Answer = dialogueData[textLV].Choice_Text1_Answer;
            choiceScripts[0].number = 0;
            choiceScripts[0].mbti_ID = dialogueData[textLV].Mbti1_ID;
            npcChildSet.ChoiceTransform.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            npcChildSet.ChoiceTransform.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (!dialogueData[textLV].Choice_Text2.Equals("-1"))
        {
            choiceObjText[1].text = dialogueData[textLV].Choice_Text2;// choiceObjText[0 : 몇번째?] .text =  dialogueData[0 :몇번째 대화 클래스].Choice_Text1
            choiceScripts[1].id = dialogueData[textLV].Id;
            choiceScripts[1].Choice_Text_Answer = dialogueData[textLV].Choice_Text2_Answer;
            choiceScripts[1].number = 1;
            choiceScripts[1].mbti_ID = dialogueData[textLV].Mbti2_ID;
            npcChildSet.ChoiceTransform.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            npcChildSet.ChoiceTransform.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (!dialogueData[textLV].Choice_Text3.Equals("-1"))
        {
            choiceObjText[2].text = dialogueData[textLV].Choice_Text3;// choiceObjText[0 : 몇번째?] .text =  dialogueData[0 :몇번째 대화 클래스].Choice_Text1
            choiceScripts[2].id = dialogueData[textLV].Id;
            choiceScripts[2].Choice_Text_Answer = dialogueData[textLV].Choice_Text3_Answer;
            choiceScripts[2].number = 2;
            choiceScripts[2].mbti_ID = dialogueData[textLV].Mbti3_ID;
            npcChildSet.ChoiceTransform.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            npcChildSet.ChoiceTransform.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (!dialogueData[textLV].Choice_Text4.Equals("-1"))
        {
            choiceObjText[3].text = dialogueData[textLV].Choice_Text4;// choiceObjText[0 : 몇번째?] .text =  dialogueData[0 :몇번째 대화 클래스].Choice_Text1
            choiceScripts[3].id = dialogueData[textLV].Id;
            choiceScripts[3].Choice_Text_Answer = dialogueData[textLV].Choice_Text4_Answer;
            choiceScripts[3].number = 3;
            choiceScripts[3].mbti_ID = dialogueData[textLV].Mbti4_ID;
            npcChildSet.ChoiceTransform.transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            npcChildSet.ChoiceTransform.transform.GetChild(3).gameObject.SetActive(false);
        }
    }



    public void ExitTalk() // 대화종료 시 선택지 비활성화
    {
        textLV = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (npcChildSet.ChoiceTransform.transform.GetChild(i).gameObject.activeSelf)
            {
                npcChildSet.ChoiceTransform.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    #endregion
    #region npc글세팅
    public int WordChange(int i) // 글 교체 및 npc대화
    {
       
    
        if (newNPC.NPC_ID.Equals(null)) // npcid 가 널이면 대화안함
        {
            return 0;
        }
        PrecedeQuest_IDChk();// 선행퀘스트 조건 체크 여기서 선행퀘가 없거나 달성시 prerequisiteQuest = true 미달성시 prerequisiteQuest = false로 바꿈

       
       

     
        if (i == -1)
        {
   
            TalkOff();
            return i + 1;
        }
     
        if (i < npcWoadDict[dictProgressSb.ToString()].Count)
        {
            if (i == 0)
            {
                TalkOn();
            }
            if (NPCEnd) // 선행 퀘스트 완료 하지않으면 말안함
            {

                if (QuestMananger.instance.playerQuest.ContainsKey(dialogueData[textLV].Quest_ID.ToString())) // 퀘스트 수락후 대사 출력
                {
                    npcTextMeshPro.text = CSVRead.instance.QuestDatas[dialogueData[textLV].Quest_ID.ToString()].QuestProgressDialogue;
                }


            }
            else
            {
                npcTextMeshPro.text = npcWoadDict[dictProgressSb.ToString()][i];
            }


            if (i == npcWoadDict[dictProgressSb.ToString()].Count - 1)
            {
                SetChoiceText();
            }
        }
     
     
      

        return i + 1;
    }
    private string reCutString(string str) // 재귀로 50글자 초과시 word컷처리
    {
        if (str.Length > 50)
        {

            npcWoad.Add(str.Substring(0, 50));
            return reCutString(str.Substring(50));
        }
        else
        {
          
            npcWoad.Add(str);
        }

        return null;
    }
    public string NpcWord() //csv에서 Description 읽어옴
    {
        if (npcID.Equals(newNPC.NPC_ID.ToString()))
        {
            for (int i = 0; i <= newNPC.ContinueDialogueCount; i++)
            {
                dialogueData = newNPC.ContinueDialogue(i);

                for (int j = 0; j < dialogueData.Count; j++)
                {
                   
                    npcWoad = new List<string>(); // npc 대사 세팅 50글자씪 자를리스트 초기화
                    reCutString(dialogueData[j].Choice_Before_Dialogue);

                    npcWoadDict.Add(dialogueData[j].Choice_Bundle_Tag.ToString() + "_" + dialogueData[j].Choice_Order_Number.ToString(), npcWoad);

                }

            }
            dialogueData = newNPC.ContinueDialogue(0);

        }

        return "대화종료";
    }
    #endregion
    #region 글보여주기 닫기
    public void IconOn()
    {

        if (newNPC.Type ==-1)
        {
            return;
        }

        if (!npcChildSet.targetOBj[newNPC.Type].gameObject.activeSelf)
        {
            npcChildSet.targetOBj[newNPC.Type].gameObject.SetActive(true);
        }
    }
    public void TalkOn() // 글보여주기
    {
  
        if (newNPC.Type.Equals("-1"))
        {
            Debug.LogError("Npc Text Type :-1"); //npc 텍스트가 널일경우 
            return;
        }
      
        if ( prerequisiteQuest == false )
        {
            return;
        }

        if (!npcChildSet.targetOBj[4].gameObject.activeSelf)
        {
            npcChildSet.targetOBj[newNPC.Type].gameObject.SetActive(false);
            npcChildSet.targetOBj[4].gameObject.SetActive(true);
        }

    }
    private void PrecedeQuest_IDChk() // 퀘스트 선행여부체크
    {
     
        if (dialogueData[0].Quest_ID.ToString().Equals("-1"))
        {          
            prerequisiteQuest = true;
        }
        else if (QuestMananger.instance.playerQuest.ContainsKey(dialogueData[0].Quest_ID.ToString())) // 퀘스트 id 존재여부체크 
        {
           
            QuestMananger.instance.QusetCompletionConditionChk(dialogueData[0].Quest_ID.ToString(), newNPC.NPC_ID);// 퀘스트 완료조건 체큰
           
            if (QuestMananger.instance.playerQuest[dialogueData[0].Quest_ID.ToString()].State == QuestState.QuestStatus.Completed)
            {            
                prerequisiteQuest = true;
            }
        }
       
    }



    public void TalkOff() // 글닫기
    {
   
        if (newNPC.Type==-1)
        {
            return;
        }

        npcChildSet.targetOBj[4].gameObject.SetActive(false);
        npcChildSet.targetOBj[newNPC.Type].gameObject.SetActive(true);


    }

    public void TalkExit() // 범위 밖으로 나가 대화종료
    {
    
        if (newNPC.Type==-1)
        {
            return;
        }
        if (!NPCEnd)
        {
          
            fruits = dictProgressSb.ToString().Split("_"); // 스트링빌드 대화 태그순서 , 대화 순서 분리
            textLV = 0;
            dictProgressSb.Clear();
            // npcAction.TalkiZero();// 대화 처음으로
            dictProgressSb.Append(fruits[0] + "_1"); // 다음 대화로
        }
        mbtis.Clear();



        npcChildSet.targetOBj[4].gameObject.SetActive(false);

        npcChildSet.targetOBj[newNPC.Type].gameObject.SetActive(false);
    }
    #endregion
    #region 아이콘 번호로 부여
    public int IconSet(string Type) // 아이콘
    {

        switch (Type) // 타입에 따라 값을 int값으로 만들기
        {
            case "Main":
                return 0; // 메인

            case "Sub":
                return 1; //서브

            case "Talk":
                return 2; // 대화

            case "Save":
                return 3; //저장

            default:
                return -1; // 해당없음

        }

    }
    #endregion
}

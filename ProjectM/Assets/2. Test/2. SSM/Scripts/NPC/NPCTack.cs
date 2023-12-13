using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NPCTack : MonoBehaviour
{
    [SerializeField] private string npcID; // 내가 지정할 현재 npcId
    List<string> npcWoad; // 50글자씩 자른 npc woard
    [SerializeField] private TextMeshProUGUI npcTextMeshPro; // npctext박스
    [SerializeField] private Image textimage; // 대화 박스 이미지
    [SerializeField] private Image textimageReady;// 대화 박스 준비 이미지
    [SerializeField] private Image scrollbar;//스크롤바 위치찾기용


    public GameObject selectGameObj; // SelectText박스프리펩
    #region npc 변수
    // npc정보 변수
    int NPC_ID; // npc id
    string description; // npc 설명
    int type; // npc 타입
    string name; // npc 이름
    int hp; // npc hp 
    bool catchPossibility; // npc 잡기 가능여부
    string icon; // npc icon
    // npc 정보 변수 끝

    // npc 선택지 정보 
    int selectId; // 선택지 id
    int selectNPCId; // 선택지 npcid

    private List<string> npcSelectText;// npc 선택지 텍스트
    private List<string> npcSelectText_Answer; //npc 서택지 텍스트 대답
    // npc 선택지 정보 끝
    #endregion


    private List<GameObject> npcSelectObject; // npc 선택지 프리팹 생성한거 저장용리스트
    private NPCChildSet nPCChildSet; // 저장 Obj를 캐싱해서 가지고있는 클레스;
    private void Start()
    {

        nPCChildSet = transform.parent.GetComponent<NPCChildSet>(); // npc 아이콘 정보 캐싱해가지고있는 스크립트
        npcSelectText = new List<string>(); // npc 선택지 리스트
        npcSelectText_Answer = new List<string>(); // npc 선택지 대답 리스트

        NpcDicSet(); // npc데이터 세팅
        selectBoxSet(); // npc선택지 데이터 세팅
        npcWoad = new List<string>(); // npc 대사 세팅 50글자씪 자를리스트 초기화


        reCutString(NpcWord());
        npcTextMeshPro.text = npcWoad[0];// npc 대사 설정 
        npcSelectTextCreate();
    }

    private void npcSelectTextCreate()
    {
        int idex = 1;
        for (int i = 0; i < npcSelectText.Count; i++)
        {
            if (npcSelectText[i] != null && npcSelectText[i] != "-1")
            {
                GameObject instantiatedObject = Instantiate(selectGameObj, nPCChildSet.targetOBj[4].transform.GetChild(1).transform);

                instantiatedObject.name = idex.ToString();
                TextMeshProUGUI instantiatedObject_TMP = instantiatedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                if (instantiatedObject_TMP != null)
                {
                    instantiatedObject_TMP.text = npcSelectText[i];
                }
            }
            idex++;
        }


    }
    #region npc 선택지
    public void selectBoxSet() // 선택지 정보 셋
    {
        if (CSVRead.instance.nPCSelectTalkDatas.ContainsKey(npcID))
        {
            selectId = CSVRead.instance.nPCSelectTalkDatas[npcID].Id;
            selectNPCId = CSVRead.instance.nPCSelectTalkDatas[npcID].NPCId;
            npcSelectText.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text1);
            npcSelectText.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text2);
            npcSelectText.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text3);
            npcSelectText.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text4);
            npcSelectText_Answer.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text1_Answer);
            npcSelectText_Answer.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text2_Answer);
            npcSelectText_Answer.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text3_Answer);
            npcSelectText_Answer.Add(CSVRead.instance.nPCSelectTalkDatas[npcID].Choice_Text4_Answer);

        }
    }
    #endregion
    #region 글교체
    public int wordChange(int i) // 글 교체
    {
        if (npcWoad.Count - 1 > i)
        {
            i = i + 1;
            npcTextMeshPro.text = npcWoad[i];

            return i;
        }
        return i;
    }
    #endregion
    #region 50 글자씩 컷팅해서 리스트에 담는함수
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
    #endregion
    #region npc글세팅
    public string NpcWord() //csv에서 Description 읽어옴
    {

        if (npcID.Equals(NPC_ID.ToString()))
        {

            return description;
        }

        return "x? :";
    }
    #endregion
    #region csv에서 클레스 가져와서 세팅
    private void NpcDicSet() 
    {
        if (CSVRead.instance.nPCDatas.ContainsKey(npcID))
        {
            NPC_ID = CSVRead.instance.nPCDatas[npcID].NPC_ID;
            description = CSVRead.instance.nPCDatas[npcID].Description;
            name = CSVRead.instance.nPCDatas[npcID].Name;
            type = IconSet(CSVRead.instance.nPCDatas[npcID].Type);
            hp = CSVRead.instance.nPCDatas[npcID].Hp;
            catchPossibility = CSVRead.instance.nPCDatas[npcID].CatchPossibility;
            icon = CSVRead.instance.nPCDatas[npcID].Icon;

        }

    }

    #endregion
    #region 글보여주기 닫기
    public void IconOn()
    {


        if (type == -1)
        {
            return;
        }

        if (!nPCChildSet.targetOBj[type].gameObject.activeSelf)
        {
            nPCChildSet.targetOBj[type].gameObject.SetActive(true);
        }
    }
    public void TalkOn() // 글보여주기
    {
        if (type == -1)
        {
            Debug.LogError("Npc Text Type :-1"); //npc 텍스트가 널일경우 
            return;
        }
        if (!textimage.gameObject.activeSelf)
        {
            nPCChildSet.targetOBj[type].gameObject.SetActive(false);
            nPCChildSet.targetOBj[4].gameObject.SetActive(true);
        }
    }
    public void TalkOff() // 글닫기
    {
        if (type == -1)
        {
            return;
        }

        nPCChildSet.targetOBj[4].gameObject.SetActive(false);
        nPCChildSet.targetOBj[type].gameObject.SetActive(true);


    }

    public void TalkExit() // 범위 밖으로 나가 대화종료
    {
        if (type == -1)
        {
            return;
        }
        nPCChildSet.targetOBj[4].gameObject.SetActive(false);

        nPCChildSet.targetOBj[type].gameObject.SetActive(false);
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

            case "Tack":
                return 2; // 대화

            case "Save":
                return 3; //저장

            default:
                return -1; // 해당없음

        }

    }
    #endregion
}

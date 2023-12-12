 using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class NPCTack : MonoBehaviour
{
    public string npcID;
    List<string> npcWoad;
    [SerializeField] private TextMeshProUGUI npcTextMeshPro; // npctext박스
    [SerializeField] private Image textimage; // 대화 박스 이미지
    [SerializeField] private Image textimageReady;// 대화 박스 준비 이미지
    [SerializeField] private Image scrollbar;//스크롤바 위치찾기용

    [SerializeField] private List<TextMeshProUGUI> selectTextMeshProsList; // SelectText박스리스트
    [SerializeField] private TextMeshProUGUI selectTextMeshPro; // SelectText박스

   
    private void Start()
    {
        npcWoad = new List<string>();
        int selectNumber = 1;
        selectTextMeshProsList = new List<TextMeshProUGUI>();

        //db 데이터 만큼 선택지 추가 

        //{지문 숫자만큼 생성  (만들어야함)
        selectTextMeshProsList.Add(selectTextMeshPro); // 데이터를 List 에추가 
        //{지문 숫자만큼 생성 end

        //생성한 selectTextMeshProsList에 택스트 세팅 (만들어야함)
        selectTextMeshPro.text = selectNumber +". Yes";
        //생성한 selectTextMeshProsList에 택스트 세팅 end

        foreach (TextMeshProUGUI tmpText in selectTextMeshProsList) // 데이터를 scrollbar 자식으로 생성
        {
            TextMeshProUGUI InstanTmpText = Instantiate(tmpText, scrollbar.transform);
           // InstanTmpText.text = tmpText.text;
        }
        //db 데이터 만큼 선택지 추가 end
        reCutString(NpcWord());
        npcTextMeshPro.text = npcWoad[0];// npc 대사 설정 
      
    }
    #region 글교체
    public int wordChange(int i) // 글 교체
    {
        if(npcWoad.Count-1 > i)
        {
            i = i + 1;
            npcTextMeshPro.text = npcWoad[i];

            return i;
        }
        return i;
    }
    #endregion

    

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
    #region csv에서 값 읽어오기
    public string NpcWord() //csv에서 Description 읽어옴
    {
        foreach(var str in CSVRead.instance.nPCDatas)
        {
            Debug.Log(str.Key);
           if(str.Key.ToString().Equals(npcID))
            {
                Debug.Log(str.Value.Description);
                return str.Value.Description;
            }
        }    
        return "x?";
    }
    #endregion
    #region 글보여주기 닫기
    public void TalkOn() // 글보여주기
    {
        if (!textimage.gameObject.activeSelf)
        {
            textimageReady.gameObject.SetActive(false);
            textimage.gameObject.SetActive(true);
        }      
    }
    public void TalkOff() // 글닫기
    {
        textimage.gameObject.SetActive(false);
        textimageReady.gameObject.SetActive(true);
    }
    #endregion
}

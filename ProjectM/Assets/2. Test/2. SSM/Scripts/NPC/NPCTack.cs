using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NPCTack : MonoBehaviour
{
    [SerializeField] private string npcTalkText; // npc 대사
    [SerializeField] private TextMeshProUGUI npcTextMeshPro; // npctext박스
    [SerializeField] private Image textimage; // 대화 박스 이미지
    [SerializeField] private Image textimageReady;// 대화 박스 준비 이미지
    [SerializeField] private Image scrollbar;//스크롤바 위치찾기용

    [SerializeField] private List<TextMeshProUGUI> selectTextMeshProsList; // SelectText박스리스트
    [SerializeField] private TextMeshProUGUI selectTextMeshPro; // SelectText박스

   
    private void Start()
    {
        selectTextMeshProsList = new List<TextMeshProUGUI>();

        //db 데이터 만큼 선택지 추가 

        //{지문 숫자만큼 생성  (만들어야함)
        selectTextMeshProsList.Add(selectTextMeshPro); // 데이터를 List 에추가 
        //{지문 숫자만큼 생성 end

        //생성한 selectTextMeshProsList에 택스트 세팅 (만들어야함)
        selectTextMeshPro.text = "Asd";
        //생성한 selectTextMeshProsList에 택스트 세팅 end

        foreach (TextMeshProUGUI tmpText in selectTextMeshProsList) // 데이터를 scrollbar 자식으로 생성
        {
            TextMeshProUGUI InstanTmpText = Instantiate(tmpText, scrollbar.transform);
           // InstanTmpText.text = tmpText.text;
        }
        //db 데이터 만큼 선택지 추가 end

        npcTextMeshPro.text = npcTalkText.ToString();// npc 대사 설정 
    }

    public void TalkOn() // 글보여주기
    {
        textimageReady.gameObject.SetActive(false);
        textimage.gameObject.SetActive(true);
    }
    public void TalkOff() // 글닫기
    {
        textimage.gameObject.SetActive(false);
        textimageReady.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSelectTalkData 
{
    //npc데이터
    private int id; // 대사 id
    private int npcid; // npc id
    private int next_Choice_ID; // 다음 선택지 ID 
    private int quest_ID; // 퀘스트 iD
    private string choice_Before_Dialogue; // 선택전 대사
    private int choice_Order_Number; // 선택지 등장 순서
    private string choice_Text1; // 선택지 1
    private string choice_Text2; // 선택지 2
    private string choice_Text3; // 선택지 3
    private string choice_Text4; // 선택지 4
    private string choice_Text1_Answer; // 선택지 1 대답
    private string choice_Text2_Answer; // 선택지 2 대답 
    private string choice_Text3_Answer; // 선택지 3 대답 
    private string choice_Text4_Answer; // 선택지 4 대답
    private int mbti1_ID; // mbti1 값
    private int mbti2_ID; // mbti2 값
    private int mbti3_ID; // mbti3 값
    private int mbti4_ID; // mbti4 값
    private int choice_Bundle_Tag;//대화 우선순위 태그
    // 

    public int Id
    {
        get { return id; } 
        set { id = value; } 
    }
    public int NPCId
    {
        get { return npcid; }
        set { npcid = value; }
    }
    public int NextChoice_ID
    {
        get { return next_Choice_ID; }
        set { next_Choice_ID = value; }
    }
    public int Quest_ID
    {
        get { return quest_ID; }
        set { quest_ID = value; }
    }
    public string Choice_Before_Dialogue
    {
        get { return choice_Before_Dialogue; }
        set { choice_Before_Dialogue = value; }
    }
    public int Choice_Order_Number
    {
        get { return choice_Order_Number; }
        set { choice_Order_Number = value;}
    }

    public string Choice_Text1
    {
        get { return choice_Text1; }
        set { choice_Text1 = value; }
    }
    public string Choice_Text2
    {
        get { return choice_Text2; }
        set { choice_Text2 = value; }
    }
    public string Choice_Text3
    {
        get { return choice_Text3; }
        set { choice_Text3 = value; }
    }
    public string Choice_Text4
    {
        get { return choice_Text4; }
        set { choice_Text4 = value; }
    }
    public string Choice_Text1_Answer
    {
        get { return choice_Text1_Answer; }
        set { choice_Text1_Answer = value;}
    }
    public string Choice_Text2_Answer
    {
        get { return choice_Text2_Answer; }
        set { choice_Text2_Answer = value; }
    }
    public string Choice_Text3_Answer
    {
        get { return choice_Text3_Answer; }
        set { choice_Text3_Answer = value; }
    }
    public string Choice_Text4_Answer
    {
        get { return choice_Text4_Answer; }
        set { choice_Text4_Answer = value; }
    }
    public int Mbti1_ID
    {
        get { return mbti1_ID; }
        set { mbti1_ID = value;}
    }
    public int Mbti2_ID
    {
        get { return mbti2_ID; }
        set { mbti2_ID = value; }
    }
    public int Mbti3_ID
    {
        get { return mbti3_ID; }
        set { mbti3_ID = value; }
    }
    public int Mbti4_ID
    {
        get { return mbti4_ID; }
        set { mbti4_ID = value; }
    }
    public int Choice_Bundle_Tag
    {
        get { return choice_Bundle_Tag; }
        set { choice_Bundle_Tag = value; }
    }
}

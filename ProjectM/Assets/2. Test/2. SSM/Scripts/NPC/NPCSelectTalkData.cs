using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSelectTalkData 
{
    //npc데이터
    private int id;
    private int nPCId;
    private string choice_Text1;
    private string choice_Text2;
    private string choice_Text3;
    private string choice_Text4;
    private string choice_Text1_Answer;
    private string choice_Text2_Answer;
    private string choice_Text3_Answer;
    private string choice_Text4_Answer;

    public int Id
    {
        get { return id; } 
        set { id = value; } 
    }
    public int NPCId
    {
        get { return nPCId; }
        set { nPCId = value; }
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
}

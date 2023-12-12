using System.Collections;
using System.Collections.Generic;


public class NPCData
{
    private int id; // 고유번호
    private string description; // 설명
    private int type; //퀘스트 타입
    private string name; // npc 이름 
    private int hp; // 체력
    private bool catchPossibility; // pc가 해당 npc를 잡을 수 있는지 여부
    private string icon; // 상호 작용 아이콘 이름
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }
 
    public int Type
    {
        get { return type; }
        set { type = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public bool CatchPossibility
    {
        get { return catchPossibility; }
        set { catchPossibility = value; }

    }
    public string Icon
    {
        get { return icon; }
        set { icon = value; }
    }

}

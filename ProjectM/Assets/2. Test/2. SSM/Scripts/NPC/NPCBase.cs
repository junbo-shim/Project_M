using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// NPC의 기본 클래스
public class NPC
{
    // NPC 정보 {
    // npcid
    public int NPC_ID { get; set; }
    // npc 타입
    public int Type { get; set; }
    // 이름 
    public string Name { get; set; }
    //Hp
    public int Hp { get; set; }
    // 잡기 가능 여부
    public bool CatchPossibility { get; set; }
    // 대화  저장용
    public Func<int, List<NPCSelectTalkData>> ContinueDialogue { get; set; }
    // NPC 정보 }

    //대화 갯수
    public int ContinueDialogueCount { get; set; }
}

public class NPCBase
{ // NPC를 생성하는 메서드
    public NPC CreateNPC(int npcID, int type, string name, int hp, bool catchPossibility, Dictionary<string, NPCSelectTalkData> npcSelectTalkDatas)
    {
        
        int dialogueID_ = 1; //  Func<int, List<NPCSelectTalkData>> ContinueDialogue { get; set; } 의 int 값
        int save = 1;
        
        NPC newNPC = new NPC
        {
            NPC_ID = npcID,
            
            Type = type,
            Name = name,
            Hp = hp,
            CatchPossibility = catchPossibility,
            ContinueDialogueCount = 0
            // 대화 데이터 초기화

            // 다른 속성들도 필요에 따라 초기화
        };
      
        for (; dialogueID_ <= save; dialogueID_++)
        {
            newNPC.ContinueDialogue = (int dialogueID) =>
            {
                
                dialogueID += 1;
      
                List<NPCSelectTalkData> talks = new List<NPCSelectTalkData>();
                // 대화 데이터 초기화 로직을 구현
                foreach (var kvp in npcSelectTalkDatas)
                {
                  

                    if (kvp.Value.NPCId == npcID)
                    {
                    
                        save = kvp.Value.Choice_Bundle_Tag >= save ? kvp.Value.Choice_Bundle_Tag : save;
                        if (kvp.Value.Choice_Bundle_Tag == dialogueID)
                        {
                         
                            talks.Add(kvp.Value);                         
                        }
                    }
                }
                
               
        
                talks.Sort((x, y) => x.Choice_Order_Number.CompareTo(y.Choice_Order_Number));
                return talks;
            };

            newNPC.ContinueDialogueCount = newNPC.ContinueDialogueCount + 1;
        }

    


        // 생성된 NPC 반환
        return newNPC;
    }

    
}

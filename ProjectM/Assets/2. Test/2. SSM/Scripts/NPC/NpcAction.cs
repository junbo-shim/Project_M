using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NpcAction : MonoBehaviour
{
    private NPCTack nPCTack;
   
    int i= 0;
    public void Start()
    {
       
        nPCTack = GetComponent<NPCTack>();
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player")) // 플레이어에게 대사 보여주기 바라보기
        {
     
            nPCTack.TalkOn();//대화창on
            NpcLook(other.gameObject);//플레이어 처다보기
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player")) // npc 대기상태로
        {
            NpcLookClear();
        }
    }
    #region npc처다보는 함수
    public void NpcLook(GameObject gameObject)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            i = nPCTack.wordChange(i); // text교체
        }
        Vector3 vector3 = new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z);
        transform.parent.LookAt(vector3); // 플레이어 처다보기
    }
    #endregion
    #region npc처다보기 종료 함수
    public void NpcLookClear()
    {
        i = 0;
        nPCTack.wordChange(i);// text교체
        nPCTack.TalkOff();//대화창off
    }
    #endregion
}

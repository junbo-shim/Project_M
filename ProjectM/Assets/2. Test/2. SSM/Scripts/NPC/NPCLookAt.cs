using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCLookAt : MonoBehaviour
{
    private NPCTack nPCTack;
    private NPCManager nPCManager;
    public void Start()
    {
        nPCManager = GetComponent<NPCManager>();
        nPCTack = GetComponent<NPCTack>();
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player")) // 플레이어에게 대사 보여주기 바라보기
        {
            nPCManager.NPCCrash();//npc충돌
            nPCTack.TalkOn();//대화창on
            Vector3 vector3 = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.parent.LookAt(vector3); // 플레이어 처다보기
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player")) // npc 대기상태로
        {
            nPCTack.TalkOff();//대화창off
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCLookAt : MonoBehaviour
{
    private NPCTack nPCTack;

    public void Start()
    {
        nPCTack = GetComponent<NPCTack>();
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player")) // 플레이어에게 대사 보여주기 바라보기
        {
            nPCTack.TalkOn();//대화창on
            transform.LookAt(other.gameObject.transform);
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

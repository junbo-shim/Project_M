using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monster_Detect : MonsterState
{
    public override void OnStateEnter()
    {
        Debug.LogWarning("���� ����");
    }

    public override void OnStateStay()
    {
        Debug.Log("���� ��");
    }

    public override void OnStateExit()
    {
        Debug.LogError("���� Ż��");
    }
}

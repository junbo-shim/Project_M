using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Engage : MonsterState
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

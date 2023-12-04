using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monster_Detect : MonsterState
{
    public override void OnStateEnter()
    {
        Debug.LogWarning("감지 시작");
    }

    public override void OnStateStay()
    {
        Debug.Log("감지 중");
    }

    public override void OnStateExit()
    {
        Debug.LogError("감지 탈출");
    }
}

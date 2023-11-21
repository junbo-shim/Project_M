using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Engage : MonsterState
{
    public override void OnStateEnter()
    {
        Debug.LogWarning("전투 시작");
    }

    public override void OnStateStay()
    {
        Debug.Log("전투 중");
    }

    public override void OnStateExit()
    {
        Debug.LogError("전투 탈출");
    }
}

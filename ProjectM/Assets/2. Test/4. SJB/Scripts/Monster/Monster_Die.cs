using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Die : MonsterState
{
    public override void OnStateEnter()
    {
        Debug.LogWarning("Á×À½ ½ÃÀÛ");
    }

    public override void OnStateStay()
    {
        Debug.Log("Á×À½ Áß");
    }

    public override void OnStateExit()
    {
        Debug.LogError("Á×À½ Å»Ãâ");
    }
}

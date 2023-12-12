using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Runaway : MonsterState
{
    public override void OnStateEnter()
    {
        Debug.LogWarning("µµ∏¡ Ω√¿€");
    }

    public override void OnStateStay()
    {
        Debug.Log("µµ∏¡ ¡ﬂ");
    }

    public override void OnStateExit()
    {
        Debug.LogError("µµ∏¡ ≈ª√‚");
    }
}

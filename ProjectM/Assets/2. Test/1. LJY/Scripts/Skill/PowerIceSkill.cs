using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIceSkill : SkillAction
{
    private NoDamage powerIceInfo;
    private float duration; // 스킬 지속시간
    void Start()
    {
        powerIceInfo = ReturnInfo("_IceBall") as NoDamage;
        duration = powerIceInfo.SkillDuration;  // 스킬 지속시간
        statusEffId = powerIceInfo.Value2;  // 상태이상 Id
        CheckSkill();   // 스킬 분류를 위한 부모스크립트 함수 사용
    }

    void Update()
    {
        
    }
}

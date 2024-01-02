using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPoison : SkillAction
{
    private Damage poisonInfo;
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        poisonInfo = ReturnInfo("_Poison") as Damage;
        damage = poisonInfo.skillDamage;
        duration = poisonInfo.Value1;
        statusEffId = poisonInfo.Value2;
        CheckSkill();      // 스킬 분류 확인
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

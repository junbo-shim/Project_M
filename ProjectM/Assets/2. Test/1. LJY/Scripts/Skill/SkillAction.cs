using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    public float damage = default;    // 데미지 스킬이 사용할 변수 
    public bool isDamage;  // 데미지 스킬인지 구별할 bool변수
    public bool isStatusEff;    // 상태이상 스킬인지 구별할 bool변수
    public float statusEffId = default;     // 상태이상 ID

    public void CheckSkill()
    {
        if(damage > 0f)
        {
            isDamage = true;
        }
        if(statusEffId != default)
        {
            isStatusEff = true;
        }
    }

    protected SkillParent ReturnInfo(string key)
    {
        if(CSVConverter_JHW.Instance.skillDic[key] as Damage != null)
        {
            return CSVConverter_JHW.Instance.skillDic[key] as Damage;
        }
        else if(CSVConverter_JHW.Instance.skillDic[key] as NoDamage != null)
        {
            return CSVConverter_JHW.Instance.skillDic[key] as NoDamage;
        }

        return null;
        
    }
}

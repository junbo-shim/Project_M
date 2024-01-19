using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrigger : MagicBase
{
    public GameObject ehancedEff;   // 강화면 다른 투사체가 나감
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void CastSkill()
    {
        if (SkillManager.Instance.HasSkillEnhancedByName(skillName))
        {
            GameObject iceProjectile = Instantiate(ehancedEff, transform.position, transform.parent.rotation);
            iceProjectile.transform.SetParent(transform.parent);
        }   // if : 강화 스킬이라면
        else
        {
            GameObject iceProjectile = Instantiate(magicEffect, transform.position, magicEffect.transform.rotation);
            iceProjectile.transform.SetParent(transform.parent);
        }   // else : 강화 스킬이 아니라면
        base.CastSkill();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrigger : MagicBase
{
    public GameObject ehancedEff;   // 강화 스킬 오브젝트
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
            GameObject basicPoison = Instantiate(ehancedEff, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.identity);
            basicPoison.transform.SetParent(transform.parent);
        }
        else
        {
            GameObject poisonProjectile = Instantiate(magicEffect, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), magicEffect.transform.rotation);
            poisonProjectile.transform.SetParent(transform.parent);
            Debug.Log(transform.parent);
        }
        
        base.CastSkill();
    }
}

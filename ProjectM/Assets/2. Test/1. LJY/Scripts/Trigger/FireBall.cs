using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MagicBase
{

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
        if(SkillManager.Instance.HasSkillEnhancedByName(skillName))
        {
            GameObject fireballOne = Instantiate(magicEffect, new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z), transform.parent.rotation);
            fireballOne.transform.SetParent(transform.parent);
            GameObject fireballTwo = Instantiate(magicEffect, transform.position, transform.parent.rotation);
            fireballTwo.transform.SetParent(transform.parent);
            GameObject fireballThree = Instantiate(magicEffect, new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z), transform.parent.rotation);
            fireballThree.transform.SetParent(transform.parent);
        }
        else
        {
            GameObject fireball = Instantiate(magicEffect, transform.position, transform.parent.rotation);
            fireball.transform.SetParent(transform.parent);
        }
        
        // TODO : 방향 에임 방향으로 수정해야함
        base.CastSkill();
    }
}


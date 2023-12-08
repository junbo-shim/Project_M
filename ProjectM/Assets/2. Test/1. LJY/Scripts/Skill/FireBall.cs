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
        GameObject fireball = Instantiate(magicEffect, transform.position, transform.parent.rotation);
        fireball.transform.SetParent(transform.parent);
        // TODO : 방향 에임 방향으로 수정해야함
        base.CastSkill();
    }
}


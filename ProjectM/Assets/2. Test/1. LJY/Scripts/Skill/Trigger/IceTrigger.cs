using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrigger : MagicBase
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
        GameObject iceProjectile = Instantiate(magicEffect, transform.position, magicEffect.transform.rotation);
        iceProjectile.transform.SetParent(transform.parent);
        base.CastSkill();
    }
}

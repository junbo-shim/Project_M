using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrigger : MagicBase
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
        GameObject poisonProjectile = Instantiate(magicEffect, new Vector3(transform.position.x, transform.position.y + 3f , transform.position.z), Quaternion.identity);
        poisonProjectile.transform.SetParent(transform.parent);
        base.CastSkill();
    }
}

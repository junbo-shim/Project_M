using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrigger : MagicBase
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
        magicEffect.SetActive(true);
        base.CastSkill();
    }
}

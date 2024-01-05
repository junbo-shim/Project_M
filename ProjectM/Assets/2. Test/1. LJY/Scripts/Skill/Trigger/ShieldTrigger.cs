using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTrigger : MagicBase
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
        if(!magicEffect.activeSelf)
        {
            magicEffect.SetActive(true);
        }   // if : 실드가 켜진 상태가 아니라면
        else
        {
            magicEffect.SetActive(false);
            magicEffect.SetActive(true);
        }
        base.CastSkill();
    }
}

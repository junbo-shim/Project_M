using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MagicBase
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
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;    // 플레이어컨트롤러
        Transform playerCamTransform = player.GetChild(0);  // 이펙트가 보이는 PC의 카메라 트랜스폼

        // 이펙트 보여주기
        GameObject eff = Instantiate(magicEffect, player.position, magicEffect.transform.rotation);
        eff.transform.SetParent(playerCamTransform);
        eff.transform.localPosition = Vector3.zero;

        base.CastSkill();         
    }
}

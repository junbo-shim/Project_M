using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MagicBase
{
    private Transform player;
    private PlayerHealth playerHealth;
    private NoDamage healInfo;
    private float healValue;
    
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
        if(healInfo == null)    // 스킬정보가 없을 시 받아오기
        {
            healInfo = CSVConverter_JHW.Instance.skillDic["Heal"] as NoDamage;
            healValue = healInfo.Value1;
            //Debug.Log("힐 정보 받아옴");
        }

        if (player == null)     // 플레이어 체력 정보가 없을 시 받아오기
        {
            player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);    // 플레이어컨트롤러
            playerHealth = player.GetComponent<PlayerHealth>();
            //Debug.Log("플레이어 정보 받아옴");
        }

        if (playerHealth.playerHp + healValue > playerHealth.maxPlayerHp)
        {
            playerHealth.playerHp = playerHealth.maxPlayerHp;           
            //Debug.Log("풀피");
        }
        else
        {
            playerHealth.playerHp = playerHealth.playerHp + healValue;
            //Debug.LogFormat("힐하고난 체력 -> {0} ",playerHealth.playerHp);
        }
        playerHealth.ChangeHpGauge();
        //Transform playerCamTransform = player.GetChild(0);  // 이펙트가 보이는 PC의 카메라 트랜스폼

        //// 이펙트 보여주기
        //GameObject eff = Instantiate(magicEffect, player.position, magicEffect.transform.rotation);
        //eff.transform.SetParent(playerCamTransform);
        //eff.transform.localPosition = Vector3.zero;

        magicEffect.SetActive(true);


        base.CastSkill();         
    }
}

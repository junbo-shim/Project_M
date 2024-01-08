using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerHp;
    public float maxPlayerHp = 100f;

    private float maxShield = default;
    private float maxEShield = default;

    public Slider playerHpSlider;
    public Slider playerShieldSlider;

    public Image playerHpImg;
    public Image playerShieldImg;

    public GameObject shield;
    private ShieldMagic shieldMagic;

    // Start is called before the first frame update
    void Start()
    {
        if(shield != null)
        {
            shieldMagic = shield.GetComponent<ShieldMagic>();
            maxShield = shieldMagic.basicInfo.Value1;
            maxEShield = shieldMagic.enhanceInfo.Value1;
        }
        playerHp = maxPlayerHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.U))
        {
            GetHit(15f);
        }
    }

    public void GetHit(float damage)
    {

        if(playerHp > 100f)//;(shield.activeSelf)
        {
            // 방어도에서 데미지 감산
            shieldMagic.shieldGauge -= damage;
            if(shieldMagic.isEnhanced == false)
            {
                // 바로 줄어들 게이지
                playerShieldSlider.value = shieldMagic.shieldGauge / maxShield;
                // 천천히 줄어들 게이지
                playerShieldImg.DOFillAmount(shieldMagic.shieldGauge / maxShield, 1.0f);
            }
            else
            {
                // 바로 줄어들 게이지
                playerShieldSlider.value = shieldMagic.shieldGauge / maxEShield;
                // 천천히 줄어들 게이지
                playerShieldImg.DOFillAmount(shieldMagic.shieldGauge / maxEShield, 1.0f);
            }
            
        }
        else
        {
            // 체력에서 데미지 감산
            playerHp -= damage;
            // 바로 줄어들 체력게이지
            playerHpSlider.value = playerHp / maxPlayerHp;
            // 천천히 줄어들 체력게이지
            playerHpImg.DOFillAmount(playerHp / maxPlayerHp, 1.0f);
        }

    }
}

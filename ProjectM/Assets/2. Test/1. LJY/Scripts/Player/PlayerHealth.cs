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

    public ParticleSystem hitParticle;

    // Start is called before the first frame update
    void Start()
    {
        if (shield != null)
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
        if (Input.GetKeyUp(KeyCode.U))
        {
            GetHit(15f);
        }
    }


    public void GetHit(float damage)
    {

        if (shield.activeSelf)
        {
            // 방어도에서 데미지 감산
            shieldMagic.shieldGauge -= damage;
            if (shieldMagic.shieldGauge <= 0)
            {
                shieldMagic.gameObject.SetActive(false);
            }
            hitParticle.Play(); // 피격 이펙트 재생
            ChangeShieldGauge();// 실드 게이지 변경 함수
        }
        else
        {
            // 체력에서 데미지 감산
            if((playerHp - damage) <= 0)
            {
                // TODO : 플레이어 사망 함수 추가
            }
            else
            {
                playerHp -= damage;
                hitParticle.Play(); // 피격 이펙트 재생
                ChangeHpGauge();    // 체력 게이지 변경 함수
            }
        }

    }

    public void ChangeHpGauge()
    {
        // 바로 줄어들 체력게이지
        playerHpSlider.value = playerHp / maxPlayerHp;
        // 천천히 줄어들 체력게이지
        playerHpImg.DOFillAmount(playerHp / maxPlayerHp, 1.0f);
    }

    public void ChangeShieldGauge()
    {
        if (shieldMagic.isEnhanced == false)
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



    // 몬스터에게 피격당할 때 적용할 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("MonsterATK")))
        {
            // 몬스터의 데미지 값을 불러와 GetHit에 적용
            GetHit(other.gameObject.GetComponent<MonsterATK>().damage);
        }
    }
}

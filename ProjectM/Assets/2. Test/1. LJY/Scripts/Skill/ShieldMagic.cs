using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMagic : SkillAction
{
    public float shieldGauge;
    public NoDamage basicInfo;
    public NoDamage enhanceInfo;
    private float duration;

    private void Start()
    {
        if (basicInfo == null)
        {
            basicInfo = ReturnInfo("Protect") as NoDamage;
        }
        if (enhanceInfo == null)
        {
            enhanceInfo = ReturnInfo("_Protect") as NoDamage;
        }
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (basicInfo == null)
        { return; }

        StartCoroutine(StartDuration());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 시간이 모두 지나고 꺼지지 않았을 경우
    private IEnumerator StartDuration()
    {
        if (isEnhanced == false)
        {
            InputValue(basicInfo);
        }
        else
        {
            InputValue(enhanceInfo);
        }
        yield return new WaitForSeconds(duration);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void InputValue(NoDamage info)
    {
        duration = info.SkillDuration;
        shieldGauge = info.Value1;
    }
}

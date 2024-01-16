using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject magicEffect;
    [SerializeField]
    protected float triggerDuration = 5f;

    public string skillName;
    public GameObject magicUi;

    protected void OnEnable()
    {
        StartCoroutine(TriggerTimer());
    }

    // 모든 스킬은 시전 함수가 들어가야한다.
    protected virtual void CastSkill()
    {
        gameObject.SetActive(false);
        magicUi.SetActive(true);
    }

    // 스킬 트리거 오브젝트가 마법봉에 닿으면 스킬 시전함수를 실행한다.
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wand"))
        {
            CastSkill();
        }
    }

    protected IEnumerator TriggerTimer()
    {
        yield return new WaitForSeconds(triggerDuration);

        magicUi.SetActive(true);
        gameObject.SetActive(false);
    }
}

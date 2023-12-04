using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicBase : MonoBehaviour
{
    // 모든 스킬은 시전 함수가 들어가야한다.
    public abstract void CastSkill();

    // 스킬 트리거 오브젝트가 마법봉에 닿으면 스킬 시전함수를 실행한다.
    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wand"))
        {
            CastSkill();
        }
    }
}

using UnityEngine;
using System.Collections;

public class Test1 : MonoBehaviour
{
    public int speed;
    public int hp;

    private float delayTime;
    private WaitForSecondsRealtime repeatTime;

    // Start is called before the first frame update
    void Start()
    {
        speed = 50;
        hp = 100;
        delayTime = 0.5f;
        repeatTime = new WaitForSecondsRealtime(delayTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerATK")))
        {
            if (other.GetComponent<SkillAction>().isDamage == true
                && other.GetComponent<SkillAction>().isStatusEff == false)
            {
                hp -= (int)other.GetComponent<SkillAction>().damage;
            }
            else if (other.GetComponent<SkillAction>().isDamage == true
                && other.GetComponent<SkillAction>().isStatusEff == true)
            {
                hp -= (int)other.GetComponent<SkillAction>().damage;
                //MonsterDebuff.Instance.GetToxic(gameObject);

                GetToxic(gameObject);
            }
            else if (other.GetComponent<SkillAction>().isDamage == false
                && other.GetComponent<SkillAction>().isStatusEff == true)
            {
                //MonsterDebuff.Instance.GetToxic(gameObject);

                GetSlow(gameObject);
            }
        }
    }


    #region 중독상태
    public void GetToxic(GameObject monster_)
    {
        StartCoroutine(ToxicEffect(monster_));
    }

    public IEnumerator ToxicEffect(GameObject monster_)
    {
        float duration = default;
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Duration;
        int damage = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Value1;

        while (duration < maxDuration && hp > 0)
        {
            yield return repeatTime;

            // 체력 감소
            monster_.GetComponent<Test1>().hp -= damage;
            // 타이머 증가
            duration += delayTime;
        }
    }
    #endregion

    #region 슬로우상태
    public void GetSlow(GameObject monster_)
    {
        StartCoroutine(SlowEffect(monster_));
    }

    public IEnumerator SlowEffect(GameObject monster_)
    {
        float duration = default;
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Duration;
        int slowValue = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Value1;
        Debug.LogWarning(slowValue);

        monster_.GetComponent<Test1>().speed -= slowValue;

        while (duration < maxDuration
            && hp > 0)
        {
            yield return repeatTime;
            duration += delayTime;
        }

        monster_.GetComponent<Test1>().speed += slowValue;
    }
    #endregion
}

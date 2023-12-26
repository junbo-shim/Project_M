using UnityEngine;
using System.Collections;

public class DebuffData
{
    public int DebuffID { get; private set; }
    public string Description { get; private set; }
    public string Type { get; private set; }
    public int Duration { get; private set; }
    public int Value1 { get; private set; }

    public DebuffData(string csvData_, int optionCount_)
    {
        CreateDebuffData(csvData_, optionCount_);
    }

    private DebuffData CreateDebuffData(string csvData_, int optionCount_)
    {
        string[] splitData = default;

        splitData = csvData_.Split(",");

        for (int i = 0; i < optionCount_; i++)
        {
            DebuffID = int.Parse(splitData[i]);
            Description = splitData[i + 1];
            Type = splitData[i + 2];
            Duration = int.Parse(splitData[i + 3]);
            Value1 = int.Parse(splitData[i + 4]);
        }

        return this;
    }
}

public class MonsterDebuff : MonoBehaviour
{
    private float delayTime = 0.5f;
    private WaitForSecondsRealtime repeatTime;

    private void Awake()
    {
        repeatTime = new WaitForSecondsRealtime(delayTime);
    }


    #region 중독상태
    public void GetToxic(GameObject monster_)
    {
        StartCoroutine(ToxicEffect(monster_));
    }

    public IEnumerator ToxicEffect(GameObject monster_)
    {
        int duration = default;
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Duration;
        int damage = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Value1;

        while (duration < maxDuration)
        {
            yield return repeatTime;

            // 체력 감소 로직
            monster_.GetComponent<Monster>().monsterHP -= damage;

            duration += (int)delayTime;
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
        int duration = default;
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Duration;
        int slowValue = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Value1;

        while (duration < maxDuration)
        {
            yield return repeatTime;

            // 이동속도 감소 로직
            //monster_.GetComponent<Monster>().monsterData.MonsterMoveSpeed -= 

            duration += (int)delayTime;
        }
    }
    #endregion

    #region 빙결상태
    public void GetFrozen()
    {
        StartCoroutine(FrozenEffect());
    }

    public IEnumerator FrozenEffect()
    {
        int duration = default;
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Freeze].Duration;

        while (duration < maxDuration)
        {
            yield return repeatTime;

            // 정지 로직

            duration += (int)delayTime;
        }
    }
    #endregion

    #region 속박상태
    public void GetBind()
    {
        StartCoroutine(BindEffect());
    }

    public IEnumerator BindEffect()
    {
        int duration = default;
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Bind].Duration;

        while (duration < maxDuration)
        {
            yield return repeatTime;

            // 정지 로직

            duration += (int)delayTime;
        }
    }
    #endregion
}

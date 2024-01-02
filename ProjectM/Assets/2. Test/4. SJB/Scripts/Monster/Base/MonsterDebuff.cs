using UnityEngine;
using System.Collections;

public class DebuffData
{
    public int DebuffID { get; private set; }
    public string Description { get; private set; }
    public string Type { get; private set; }
    public int Duration { get; private set; }
    public int Value1 { get; private set; }

    public DebuffData(string csvData_)
    {
        CreateDebuffData(csvData_);
    }

    private DebuffData CreateDebuffData(string csvData_)
    {
        string[] splitData = default;

        splitData = csvData_.Split(",");

        DebuffID = int.Parse(splitData[0]);
        Description = splitData[1];
        Type = splitData[2];
        Duration = int.Parse(splitData[3]);
        Value1 = int.Parse(splitData[4]);

        return this;
    }
}

public class MonsterDebuff : MonoBehaviour
{
    #region Singleton
    private static MonsterDebuff instance;
    // 싱글턴으로 만들기
    public static MonsterDebuff Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MonsterDebuff>();
            }
            return instance;
        }
    }
    #endregion

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

    // 중독 코루틴
    public IEnumerator ToxicEffect(GameObject monster_)
    {
        float duration = default;
        // MonsterDebuffDic 에 있는 중독 지속시간
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Duration;
        // MonsterDebuffDic 에 있는 중독 데미지
        int damage = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Value1;
       

        // 타이머 값이 지속시간 이하일 경우 && 몬스터의 HP 가 0 초과일 경우만 코루틴 반복
        while (duration < maxDuration 
            && monster_.GetComponent<Monster>().monsterHP > 0)
        {
            yield return repeatTime;

            // 체력 감소
            monster_.GetComponent<Monster>().monsterHP -= damage;
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

    // 슬로우 코루틴
    public IEnumerator SlowEffect(GameObject monster_)
    {
        float duration = default;
        // MonsterDebuffDic 에 있는 슬로우 지속시간
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Duration;
        // MonsterDebuffDic 에 있는 이동속도 감소 값
        int slowValue = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Value1;


        // 슬로우 시작
        monster_.GetComponent<Monster>().monsterMoveSpeed -= slowValue;

        // 타이머 값이 지속시간 이하일 경우 && 몬스터의 HP 가 0 초과일 경우만 코루틴 반복
        while (duration < maxDuration
            && monster_.GetComponent<Monster>().monsterHP > 0)
        {
            yield return repeatTime;

            // 타이머 증가
            duration += delayTime;
        }

        // 슬로우 끝
        monster_.GetComponent<Monster>().monsterMoveSpeed += slowValue;
    }
    #endregion

    #region 빙결상태
    public void GetFrozen(GameObject monster_)
    {
        StartCoroutine(FrozenEffect(monster_));
    }

    // 빙결 코루틴
    public IEnumerator FrozenEffect(GameObject monster_)
    {
        float duration = default;
        // MonsterDebuffDic 에 있는 빙결 지속시간
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Freeze].Duration;
        // 몬스터 이동속도 캐싱
        float monsterSpeed = monster_.GetComponent<Monster>().monsterMoveSpeed;


        // 빙결 시작
        monster_.GetComponent<Monster>().monsterMoveSpeed = 0f;

        // 타이머 값이 지속시간 이하일 경우 && 몬스터의 HP 가 0 초과일 경우만 코루틴 반복
        while (duration < maxDuration
            && monster_.GetComponent<Monster>().monsterHP > 0)
        {
            yield return repeatTime;

            // 타이머 증가
            duration += delayTime;
        }

        // 빙결 끝
        monster_.GetComponent<Monster>().monsterMoveSpeed = monsterSpeed;
    }
    #endregion

    #region 속박상태
    public void GetBind(GameObject monster_)
    {
        StartCoroutine(BindEffect(monster_));
    }

    // 속박 코루틴
    public IEnumerator BindEffect(GameObject monster_)
    {
        float duration = default;
        // MonsterDebuffDic 에 있는 속박 지속시간
        int maxDuration = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Bind].Duration;
        // 몬스터 이동속도 캐싱
        float monsterSpeed = monster_.GetComponent<Monster>().monsterMoveSpeed;


        // 속박 시작
        monster_.GetComponent<Monster>().monsterMoveSpeed = 0f;

        // 타이머 값이 지속시간 이하일 경우 && 몬스터의 HP 가 0 초과일 경우만 코루틴 반복
        while (duration < maxDuration
            && monster_.GetComponent<Monster>().monsterHP > 0)
        {
            yield return repeatTime;

            // 타이머 증가
            duration += delayTime;
        }

        // 속박 끝
        monster_.GetComponent<Monster>().monsterMoveSpeed = monsterSpeed;
    }
    #endregion
}

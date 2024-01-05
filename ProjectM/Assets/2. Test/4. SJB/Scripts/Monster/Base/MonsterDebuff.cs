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

    // 각종 디버프 value
    private int toxicVal;
    private int slowVal;
    private int frozenVal;
    private int bindVal;


    // 각종 디버프의 지속시간
    private float toxicMaxTime;
    private float slowMaxTime;
    private float frozenMaxTime;
    private float bindMaxTime;

    // 디버프 간격
    private float delayTime = 0.5f;
    private WaitForSeconds repeatTime;


    private void Awake()
    {
        // 각종 수치 캐싱
        toxicVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Value1;
        slowVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Value1;
        frozenVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Frozen].Value1;
        bindVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Bind].Value1;

        // 디버프 지속시간 설정
        toxicMaxTime = 10f;
        slowMaxTime = 10f;
        frozenMaxTime = 10f;
        bindMaxTime = 10f;

        repeatTime = new WaitForSeconds(delayTime);
    }
    

    #region 상태이상 구분 메서드
    public void DoDebuff(Monster monster_, int debuffID_)
    {
        // DebuffID 를 읽어와서 케이스별로 로직을 작동시킨다
        switch (debuffID_)
        {
            case (int)Monster.DebuffState.Toxic:
                StartCoroutine(ToxicRoutine(monster_));
                break;

            case (int)Monster.DebuffState.Slow:
                StartCoroutine(SlowRoutine(monster_));
                break;

            case (int)Monster.DebuffState.Frozen:
                StartCoroutine(FrozenRoutine(monster_));
                break;

            case (int)Monster.DebuffState.Bind:
                StartCoroutine(BindRoutine(monster_));
                break;
        }
    }
    #endregion


    #region Toxic 루틴
    private IEnumerator ToxicRoutine(Monster monster_) 
    {
        yield return repeatTime;
    }
    #endregion
    

    #region Slow 루틴
    private IEnumerator SlowRoutine(Monster monster_) 
    {
        // 디버프 타이머 생성
        float debuffTime = default;

        // 몬스터 본래 속도 캐싱
        float originalSpeed = GetMonsterSpeedValue(monster_);

        // 이펙트 풀에서 이펙트를 가져온다

        // 만약 처음 슬로우 걸리는 경우
        if (monster_.slowCount == 0) 
        {
            // 속도 값을 줄인다
            monster_.monsterMoveSpeed -= slowVal * 0.02f;
            // 애니메이션 속도도 줄인다
            monster_.monsterAnimator.speed -= slowVal * 0.01f;
            // 이펙트 실행
        }

        // 슬로우 카운트를 올려준다
        monster_.slowCount += 1;

        // 본격적인 반복문 : 슬로우 카운트가 0 이상 && 지속시간 동안 상태를 지속
        while (monster_.slowCount > 0 && debuffTime < slowMaxTime) 
        {
            yield return repeatTime;

            // 중간에 데미지로 인해 몬스터 죽을 경우 체크
            CheckMonsterHP(monster_);

            // 이펙트를 이동시킨다

            // 타이머 증가
            debuffTime += delayTime;
        }
        // 반복문 끝난 후에는 슬로우 카운트 감소
        monster_.slowCount -= 1;

        // 만약 슬로우 카운트가 0 이 될 경우 -> 슬로우 상태가 완전히 끝
        if (monster_.slowCount == 0) 
        {
            // 속도 값 복구
            monster_.monsterMoveSpeed = originalSpeed;
            // 애니메이션 속도도 복구
            monster_.monsterAnimator.speed = 1;
        }
    }
    #endregion


    #region Frozen 루틴
    private IEnumerator FrozenRoutine(Monster monster_)
    {
        // 디버프 타이머 생성
        float debuffTime = default;

        // 몬스터 본래 속도 캐싱
        float originalSpeed = GetMonsterSpeedValue(monster_);

        // 만약 처음 빙결 걸리는 경우
        if (monster_.frozenCount == 0)
        {
            // 속도 값 = 0
            monster_.monsterMoveSpeed = 0;
            // 애니메이션 속도 = 0
            monster_.monsterAnimator.speed = 0;
        }

        // 빙결 카운트를 올려준다
        monster_.frozenCount += 1;

        // 본격적인 반복문 : 빙결 카운트가 0 이상 && 지속시간 동안 상태를 지속
        while (monster_.frozenCount > 0 && debuffTime < frozenMaxTime)
        {
            yield return repeatTime;

            // 중간에 데미지로 인해 몬스터 죽을 경우 체크
            CheckMonsterHP(monster_);

            // 타이머 증가
            debuffTime += delayTime;
        }

        // 반복문 끝난 후에는 빙결 카운트 감소
        monster_.frozenCount -= 1;

        // 만약 빙결 카운트가 0 이 될 경우 -> 빙결 상태가 완전히 끝
        if (monster_.frozenCount == 0)
        {
            // 속도 값 복구
            monster_.monsterMoveSpeed = originalSpeed;
            // 애니메이션 속도도 복구
            monster_.monsterAnimator.speed = 1;
        }
    }
    #endregion


    #region Bind 루틴
    private IEnumerator BindRoutine(Monster monster_)
    {
        // 디버프 타이머 생성
        float debuffTime = default;

        // 몬스터 본래 속도 캐싱
        float originalSpeed = GetMonsterSpeedValue(monster_);

        // 만약 처음 속박 걸리는 경우
        if (monster_.bindCount == 0)
        {
            // 속도 값을 줄인다
            monster_.monsterMoveSpeed = 0;
        }

        // 속박 카운트를 올려준다
        monster_.bindCount += 1;

        // 본격적인 반복문 : 속박 카운트가 0 이상 && 지속시간 동안 상태를 지속
        while (monster_.bindCount > 0 && debuffTime < bindMaxTime)
        {
            yield return repeatTime;

            // 중간에 데미지로 인해 몬스터 죽을 경우 체크
            CheckMonsterHP(monster_);

            // 타이머 증가
            debuffTime += delayTime;
        }

        // 반복문 끝난 후에는 속박 카운트 감소
        monster_.bindCount -= 1;

        // 만약 속박 카운트가 0 이 될 경우 -> 속박 상태가 완전히 끝
        if (monster_.bindCount == 0)
        {
            // 속도 값 복구
            monster_.monsterMoveSpeed = originalSpeed;
        }
    }
    #endregion



    #region 죽었는지 확인하는 메서드
    private void CheckMonsterHP(Monster monster_) 
    {
        // 만약 몬스터 체력이 0 이하이면
        if (monster_.monsterHP <= 0) 
        {
            // 만약 이미 죽음 상태일 경우 메서드 탈출
            if (monster_.monsterFSM.currentState.Equals(MonsterStateMachine.State.Die)) 
            {
                return;
            }
            // 몬스터 상태를 죽음으로 변경
            monster_.monsterFSM.ChangeState(MonsterStateMachine.State.Die);
        }   
    }
    #endregion



    #region 몬스터 속도를 상황에 따라 다르게 찾아오는 메서드 (csv 설정 값 원본을 불러옴)
    private float GetMonsterSpeedValue(Monster monster_) 
    {
        int type = monster_.monsterData.MonsterType;

        switch (type) 
        {
            // 낮 몬스터일 경우
            case 1:
                return monster_.monsterData.MonsterMoveSpeed;
            // 밤 몬스터일 경우
            case 2:
                // 교전 상태일 경우
                if (monster_.monsterFSM.currentState.Equals(MonsterStateMachine.State.Engage)) 
                {
                    // 뛰는 속도로 설정
                    return monster_.monsterData.MonsterRunSpeed;
                }
                // 아니라면 
                else 
                {
                    // 일반 속도로 설정
                    return monster_.monsterData.MonsterMoveSpeed;
                }
            default:
                return 0f;
        }
    }
    #endregion
}

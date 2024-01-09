using UnityEngine;
using System.Collections;
using System.Diagnostics;

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
    //private int frozenVal;
    //private int bindVal;


    // 각종 디버프의 지속시간
    private float toxicMaxTime;
    private float slowMaxTime;
    private float frozenMaxTime;
    private float bindMaxTime;


    // 각종 디버프 이펙트 풀
    private ObjectPool toxicPool;
    private ObjectPool slowPool;
    private ObjectPool frozenPool;


    // 이펙트 회전값
    private Vector3 slowEffectRot;
    private Vector3 frozenEffectRot;

    // 빙결 이펙트 일시정지값
    private float frozenPauseTime = 1.8f;
    private WaitForSeconds frozenPause;
    // 빙결 이펙트 다시재생값
    private float frozenPlayBackTime = 1f;
    private WaitForSeconds frozenPlayBack;

    // 디버프 간격
    private float delayTime = 0.5f;
    private WaitForSeconds repeatTime;


    private void Start()
    {
        // 각종 수치 캐싱
        toxicVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Toxic].Value1;
        slowVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Slow].Value1;
        //frozenVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Frozen].Value1;
        //bindVal = MonsterCSVReader.Instance.MonsterDebuffDic[Monster.DebuffState.Bind].Value1;

        // 디버프 지속시간 설정
        toxicMaxTime = 10f;
        slowMaxTime = 10f;
        frozenMaxTime = 10f;
        bindMaxTime = 10f;

        // 디버프 이펙트 풀 캐싱
        toxicPool = GameObject.Find("Pool_Toxic").GetComponent<ObjectPool>();
        slowPool = GameObject.Find("Pool_Slow").GetComponent<ObjectPool>();
        frozenPool = GameObject.Find("Pool_Frozen").GetComponent<ObjectPool>();

        slowEffectRot = new Vector3(-74, 0, 0);
        frozenEffectRot = new Vector3(-90, 0, 0);

        repeatTime = new WaitForSeconds(delayTime);
        frozenPause = new WaitForSeconds(frozenPauseTime);
        frozenPlayBack = new WaitForSeconds(frozenPlayBackTime);
    }
    

    #region 상태이상 구분 메서드
    public void DoDebuff(Monster monster_, int debuffID_)
    {
        // DebuffID 를 읽어와서 케이스별로 로직을 작동시킨다
        switch (debuffID_)
        {
            case (int)Monster.DebuffState.Toxic:

                // 처음 맞았을 때만 코루틴 실행
                if (monster_.toxicDuration <= 0) 
                {
                    // 지속시간을 더해준다
                    monster_.toxicDuration += toxicMaxTime;
                    StartCoroutine(ToxicRoutine(monster_));
                }
                else 
                {
                    // 지속시간을 더해준다
                    monster_.toxicDuration += toxicMaxTime;
                }
                break;

            case (int)Monster.DebuffState.Slow:

                if (monster_.slowDuration <= 0) 
                {
                    monster_.slowDuration += slowMaxTime;
                    StartCoroutine(SlowRoutine(monster_));
                }
                else 
                {
                    monster_.slowDuration += slowMaxTime;
                }
                break;

            case (int)Monster.DebuffState.Frozen:

                if (monster_.frozenDuration <= 0) 
                {
                    monster_.frozenDuration += frozenMaxTime;
                    StartCoroutine(FrozenRoutine(monster_));
                }
                else 
                {
                    monster_.frozenDuration += frozenMaxTime;
                }
                break;

            case (int)Monster.DebuffState.Bind:

                if (monster_.bindDuration <= 0) 
                {
                    monster_.bindDuration += bindMaxTime;
                    StartCoroutine(BindRoutine(monster_));
                }
                else 
                {
                    monster_.bindDuration += bindMaxTime;
                }
                break;
        }
    }
    #endregion


    #region Toxic 루틴
    private IEnumerator ToxicRoutine(Monster monster_) 
    {
        // 이펙트 풀에서 이펙트를 가져온다
        ParticleSystem particle = toxicPool.ActiveObjFromPool(monster_.transform.position).GetComponent<ParticleSystem>();
        // 이펙트의 위치를 몬스터 위치로 지정한다
        particle.GetComponent<DebuffEffectMove>().monster = monster_.monsterControl;
        // 이펙트 크기 설정
        particle.transform.localScale = Vector3.one * monster_.monsterControl.height;
        // 이펙트 실행
        particle.Play();

        // 디버프 시간 동안 && 몬스터 체력이 0 보다 클 동안만 루프
        while (monster_.toxicDuration > 0 && monster_.monsterHP > 0)
        {
            // 디버프 간격 (0,5초)
            yield return repeatTime;
            // 독으로 인한 체력 감소
            monster_.monsterHP -= (int)(toxicVal * 0.2f);
            // 체력 체크
            CheckMonsterHP(monster_);
            // 지속시간 감소
            monster_.toxicDuration -= delayTime;
        }
        // 루프 끝나고 나갈 때 타이머 초기화
        monster_.toxicDuration = default;
        // 이펙트 반납
        particle.Stop();
        particle.GetComponent<DebuffEffectMove>().monster = default;
        toxicPool.ReturnObjToPool(particle.gameObject);
    }
    #endregion
    

    #region Slow 루틴
    private IEnumerator SlowRoutine(Monster monster_) 
    {
        // 몬스터 본래 속도 캐싱
        float originalSpeed = GetMonsterSpeedValue(monster_);
        // 속도 값을 줄인다
        monster_.monsterMoveSpeed -= slowVal * 0.02f;
        // 애니메이션 속도도 줄인다
        monster_.monsterAnimator.speed -= slowVal * 0.01f;

        // 이펙트 풀에서 이펙트를 가져온다
        ParticleSystem particle = slowPool.ActiveObjFromPool(monster_.transform.position).GetComponent<ParticleSystem>();
        // 이펙트의 위치를 몬스터 위치로 지정한다
        particle.GetComponent<DebuffEffectMove>().monster = monster_.monsterControl;
        // 이펙트 크기 설정 (몬스터 컨트롤러 height)
        particle.transform.localScale = Vector3.one * monster_.monsterControl.height;
        // 이펙트 돌리기
        particle.transform.rotation = Quaternion.Euler(slowEffectRot);
        // 이펙트 실행
        particle.Play();

        // 디버프 시간 동안 && 몬스터 체력이 0 보다 클 동안만 루프
        while (monster_.slowDuration > 0 && monster_.monsterHP > 0)
        {
            // 디버프 간격 (0,5초)
            yield return repeatTime;
            // 중간에 데미지로 인해 몬스터 죽을 경우 체크
            CheckMonsterHP(monster_);
            // 타이머 감소
            monster_.slowDuration -= delayTime;
        }
        // 속도 값 복구
        monster_.monsterMoveSpeed = originalSpeed;
        // 애니메이션 속도도 복구
        monster_.monsterAnimator.speed = 1;
        // 루프 끝나고 나갈 때 타이머 초기화
        monster_.slowDuration = default;

        // 이펙트 반납
        particle.Stop();
        particle.GetComponent<DebuffEffectMove>().monster = default;
        slowPool.ReturnObjToPool(particle.gameObject);
    }
    #endregion


    #region Frozen 루틴
    private IEnumerator FrozenRoutine(Monster monster_)
    {
        // 몬스터 본래 속도 캐싱
        float originalSpeed = GetMonsterSpeedValue(monster_);
        // 속도 값 = 0
        monster_.monsterMoveSpeed = 0;
        // 애니메이션 속도 = 0
        monster_.monsterAnimator.speed = 0;

        // 이펙트 풀에서 이펙트를 가져온다
        ParticleSystem particle = frozenPool.ActiveObjFromPool(monster_.transform.position).GetComponent<ParticleSystem>();
        // 이펙트의 위치를 몬스터 위치로 지정한다
        particle.transform.position = monster_.transform.position;
        // 이펙트 크기 설정 (몬스터 컨트롤러 height 절반)
        particle.transform.localScale = Vector3.one * monster_.monsterControl.height * 0.5f;
        // 이펙트 돌리기
        particle.transform.rotation = Quaternion.Euler(frozenEffectRot);
        // 이펙트 실행
        particle.Play();

        // 얼음기둥 솟는 시간까지 대기
        yield return frozenPause;

        // 이펙트 일시정지
        particle.Pause();
        // 지속시간에서 얼음기둥 솟는 대기시간 제외
        monster_.frozenDuration -= frozenPauseTime;

        // 디버프 시간 동안 && 몬스터 체력이 0 보다 클 동안만 루프
        while (monster_.frozenDuration > 0 && monster_.monsterHP > 0)
        {
            // 디버프 간격 (0,5초)
            yield return repeatTime;
            // 중간에 데미지로 인해 몬스터 죽을 경우 체크
            CheckMonsterHP(monster_);
            // 타이머 감소
            monster_.frozenDuration -= delayTime;
        }

        // 이펙트 다시 재생
        particle.Play();
        // 이펙트 재생이 완료될 때까지 대기
        yield return frozenPlayBack;

        // 속도 값 복구
        monster_.monsterMoveSpeed = originalSpeed;
        // 애니메이션 속도도 복구
        monster_.monsterAnimator.speed = 1;
        // 루프 끝나고 나갈 때 타이머 초기화
        monster_.frozenDuration = default;

        // 이펙트 반납
        particle.Stop();
        particle.GetComponent<DebuffEffectMove>().monster = default;
        frozenPool.ReturnObjToPool(particle.gameObject);
    }
    #endregion


    #region Bind 루틴
    private IEnumerator BindRoutine(Monster monster_)
    {
        // 몬스터 본래 속도 캐싱
        float originalSpeed = GetMonsterSpeedValue(monster_);
        // 속도 값을 줄인다
        monster_.monsterMoveSpeed = 0;


        // 디버프 시간 동안 && 몬스터 체력이 0 보다 클 동안만 루프
        while (monster_.bindDuration > 0 && monster_.monsterHP > 0)
        {
            // 디버프 간격 (0,5초)
            yield return repeatTime;
            // 중간에 데미지로 인해 몬스터 죽을 경우 체크
            CheckMonsterHP(monster_);
            // 타이머 감소
            monster_.bindDuration -= delayTime;
        }
        // 속도 값 복구
        monster_.monsterMoveSpeed = originalSpeed;
        // 루프 끝나고 나갈 때 타이머 초기화
        monster_.bindDuration = default;
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

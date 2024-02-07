using UnityEngine;

public class Monster : MonoBehaviour
{
    // Monster CSV 파일에서 읽어온 데이터를 알맞게 설정할 enum
    public enum MonsterType
    {
        Human_Melee = 701,
        Mech_Melee,
        Mech_Large,
        Orc_Melee,
        Spirit_Range,
        Yeti_Melee_Big,
        Spirit_Melee,

        YetiKing_Boss = 801,
        MechKing_Boss,
        MechKing_RightArm,
        MechKing_LeftArm
    }

    // Debuff CSV 파일에서 읽어온 데이터를 알맞게 설정할 enum
    public enum DebuffState
    {
        Toxic = 500,
        Slow,
        Frozen,
        Bind
    }

    // CSV 데이터
    public MonsterData monsterData;
    // 몬스터 FSM
    public MonsterStateMachine monsterFSM;
    // 몬스터 Character Controller
    public CharacterController monsterControl;
    // 몬스터 Animator
    public Animator monsterAnimator;
    // 몬스터 Sight
    public GameObject monsterSight;
    // 몬스터 Sonar (밤 몬스터)
    public GameObject monsterSonar;
    // 몬스터 Target
    public GameObject target;
    // 몬스터 HP 게이지
    public MonsterHPGauge hpGaugeUI;
    // 몬스터 UI (밤 몬스터)
    public GameObject detectUI;
    // 몬스터 오브젝트 풀
    public ObjectPool monsterPool;

    // 몬스터 데이터 타입을 분류해서 가져올 enum
    public MonsterType thisMonsterType;
    public float toxicDuration;
    public float slowDuration;
    public float frozenDuration;
    public float bindDuration;

    // 몬스터 HP (외부에서 접근가능하게끔 public 으로 열어둠)
    public int monsterHP;
    // 몬스터 이동속도 (외부에서 접근가능하게끔 public 으로 열어둠)
    public float monsterMoveSpeed;
    // 몬스터 이동속도 (외부에서 접근가능하게끔 public 으로 열어둠)
    public float monsterRunSpeed;
    // 몬스터 정찰범위
    [SerializeField]
    public float monsterPatrolRange;
    // 몬스터 시야범위
    [SerializeField]
    public float monsterSightRange;
    // 몬스터 청각범위
    [SerializeField]
    public float monsterSonarRange;

    // 몬스터 애니메이터 파라미터 ID : isMoving
    public int isMovingID;
    // 몬스터 애니메이터 파라미터 ID : isWalkng
    public int isAttackingID;
    // 몬스터 애니메이터 파라미터 ID : Dead
    public int deadID;

    // 몬스터 스폰 포인트 캐싱
    public Vector3 monsterSpawnedPoint;


    // 오브젝트가 켜질 때 실행
    protected virtual void OnEnable()
    {
        monsterSpawnedPoint = transform.position;
        monsterPool = transform.parent.gameObject.GetComponent<ObjectPool>();
        InitDebuffTimer();

        monsterFSM.ChangeState(MonsterStateMachine.State.Ready);
    }
    // 오브젝트가 꺼질 때 실행
    protected virtual void OnDisable()
    {
        monsterSpawnedPoint = default;
        monsterPool = default;
        InitDebuffTimer();

        // Die 상태 이후 작아진 크기 돌려놓기
        transform.localScale = Vector3.one;
        monsterFSM.ChangeState(MonsterStateMachine.State.Ready);
    }


    // 몬스터 오브젝트 초기화 메서드
    protected virtual void InitMonster(MonsterType inputType_)
    {
        monsterData = MonsterCSVReader.Instance.MonsterDataDic[inputType_];
        
        monsterHP = monsterData.MonsterHP;
        monsterMoveSpeed = monsterData.MonsterMoveSpeed;
        if (inputType_ == MonsterType.Mech_Large)
        {
            monsterRunSpeed = monsterData.MonsterRunSpeed;
        }

        CheckAnimatorParameter(inputType_);
    }


    // 몬스터 상태이상 타이머 초기화 메서드
    protected virtual void InitDebuffTimer()
    {
        toxicDuration = default;
        slowDuration = default;
        frozenDuration = default;
        bindDuration = default;
    }


    // 몬스터 애니메이터 파라미터 할당 메서드
    protected virtual void CheckAnimatorParameter(MonsterType inputType_)
    {
        // 애니메이터 파라미터 ID 캐싱
        isMovingID = Animator.StringToHash("isMoving");
        isAttackingID = Animator.StringToHash("isAttacking");
        deadID = Animator.StringToHash("Dead");
    }
}

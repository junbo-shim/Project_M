using UnityEngine;
using UnityEngine.UI;

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
        Yeti_Melee_Small,
        Spirit_Melee,

        YetiPrince_Boss = 801,
        MechKing_Boss
    }

    // Debuff CSV 파일에서 읽어온 데이터를 알맞게 설정할 enum
    public enum DebuffState
    {
        Nothing = 499,
        Toxic,
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

    // 몬스터 데이터 타입을 분류해서 가져올 enum
    public MonsterType thisMonsterType;
    public int toxicCount;
    public int slowCount;
    public int frozenCount;
    public int bindCount;

    // 몬스터 HP (외부에서 접근가능하게끔 public 으로 열어둠)
    public int monsterHP;
    // 몬스터 이동속도 (외부에서 접근가능하게끔 public 으로 열어둠)
    public float monsterMoveSpeed;
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

    // 몬스터 공격속도, 애니메이션 길이 (애니메이션과 싱크 맞출 속도)
    [SerializeField]
    public float monsterATKSpeed;





    // 오브젝트가 켜질 때 실행
    protected virtual void OnEnable()
    {
        toxicCount = default;
        slowCount = default;
        frozenCount = default;
        bindCount = default;

        monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
    }
    // 오브젝트가 꺼질 때 실행
    protected virtual void OnDisable()
    {
        toxicCount = default;
        slowCount = default;
        frozenCount = default;
        bindCount = default;

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

        // 애니메이터 파라미터 ID 캐싱
        isMovingID = Animator.StringToHash("isMoving");
        isAttackingID = Animator.StringToHash("isAttacking");
        deadID = Animator.StringToHash("Dead");
    }
}

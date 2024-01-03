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
        Orc_Large,
        Yeti_Melee,
        Yeti_Large,
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
        Freeze,
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
    // 몬스터 Target (sonar 로 감지한 Target, 밤 몬스터)
    public GameObject sonarTarget;
    // 몬스터 HP 게이지
    public GameObject hpGaugeUI;
    // 몬스터 UI (밤 몬스터)
    public GameObject detectUI;

    public MonsterType thisMonsterType;
    public DebuffState debuffState;

    // 몬스터 HP (외부에서 접근가능하게끔 public 으로 열어둠)
    public int monsterHP;
    // 몬스터 이동속도 (외부에서 접근가능하게끔 public 으로 열어둠)
    public float monsterMoveSpeed;
    // 몬스터 정찰범위
    public float monsterPatrolRange;
    // 몬스터 시야범위
    public float monsterSightRange;
    // 몬스터 청각범위
    public float monsterSonarRange;

    // 몬스터 애니메이터 파라미터 ID : isMoving
    public int isMovingID;
    // 몬스터 애니메이터 파라미터 ID : isWalkng
    public int isAttackingID;
    // 몬스터 애니메이터 파라미터 ID : Dead
    public int deadID;

    // 몬스터 공격속도, 애니메이션 길이 (애니메이션과 싱크 맞출 속도)
    public float monsterATKSpeed;
    // 몬스터 죽는속도, 애니메이션 길이 (애니메이션과 싱크 맞출 속도)
    public float monsterDeathSpeed;





    // 오브젝트가 켜질 때 실행
    protected virtual void OnEnable()
    {
        monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
    }
    // 오브젝트가 꺼질 때 실행
    protected virtual void OnDisable()
    {
        monsterFSM.ChangeState(MonsterStateMachine.State.Ready);
    }


    // 몬스터 오브젝트 초기화 메서드
    protected virtual void InitMonster(MonsterType inputType_)
    {
        monsterData = MonsterCSVReader.Instance.MonsterDataDic[inputType_];
        debuffState = DebuffState.Nothing;
        monsterHP = monsterData.MonsterHP;
        monsterMoveSpeed = monsterData.MonsterMoveSpeed;

        // 애니메이터 파라미터 ID 캐싱
        isMovingID = Animator.StringToHash("isMoving");
        isAttackingID = Animator.StringToHash("isAttacking");
        deadID = Animator.StringToHash("Dead");
    }
}

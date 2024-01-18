using UnityEngine;

public class Yeti_Boss : Monster
{
    public BossStateMachine bossFSM;
    public Transform projectileParent;


    // 애니메이션 bool ID 캐싱
    public int isShootingID = default;
    public int isChargingID_1 = default;
    public int isChargingID_2 = default;
    public int isChargingID_3 = default;


    // 몬스터 타입과 애니메이션 ID 를 캐싱
    private void Awake() 
    {
        thisMonsterType = MonsterType.YetiKing_Boss;

        CheckAnimatorParameter(thisMonsterType);
        isShootingID = Animator.StringToHash("isShooting");
        isChargingID_1 = Animator.StringToHash("isSubCharging_1");
        isChargingID_2 = Animator.StringToHash("isSubCharging_2");
        isChargingID_3 = Animator.StringToHash("isSubCharging_3");

        projectileParent = transform.GetChild(4).transform;
    }
    // 활성화시에 초기화
    protected override void OnEnable()
    {
        base.InitDebuffTimer();
        this.InitMonster(thisMonsterType);

        bossFSM.ChangeState(BossStateMachine.BossState.BossReady);
        bossFSM.ChangeState(BossStateMachine.BossState.Yeti_BossAttack);
    }
    // 비활성화시에 리셋
    protected override void OnDisable()
    {
        bossFSM.ChangeState(BossStateMachine.BossState.BossReady);

        base.InitDebuffTimer();
        CleanVariables();
    }



    protected override void InitMonster(MonsterType inputType_) 
    {
        monsterData = MonsterCSVReader.Instance.MonsterDataDic[inputType_];
        monsterHP = monsterData.MonsterHP;
    }

    private void CleanVariables() 
    {
        monsterData = default;
        monsterHP = default;
    }
}

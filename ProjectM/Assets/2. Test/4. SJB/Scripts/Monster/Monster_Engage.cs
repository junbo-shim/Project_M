using System.Collections;
using UnityEngine;

public class Monster_Engage : MonsterState
{
    private CharacterController monsterControl;
    private Animator monsterAni;
    private float speed;
    private float radius;
    private float gravity;

    private bool isAttacking;

    public GameObject target;

    public WaitForSecondsRealtime waitTime;
    public int waitTimer;

    public bool isRoutineOn;



    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StartCoroutine(DoEngage(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_)
    {
        // 만약 코루틴이 살아있을 경우를 대비한 안전장치
        if (isRoutineOn == true)
        {
            msm_.StopCoroutine(DoEngage(monster_, msm_));
        }
        CleanVariables(monster_);
    }





    #region 초기화
    private void Init(GameObject monster_)
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();
        speed = 5f;

        if (monster_.GetComponent<TestMonster>() == true)
        {
            radius = monster_.GetComponent<TestMonster>().sightRange * 2f;
        }
        else if (monster_.GetComponent<TestBigMonster>() == true)
        {
            radius = monster_.GetComponent<TestBigMonster>().sightRange * 2f;

            if (monster_.GetComponent<TestBigMonster>().sonarTarget != null) 
            {
                target = monster_.GetComponent<TestBigMonster>().sonarTarget;
            }
        }

        gravity = -9.81f;
        isAttacking = false;
        waitTime = new WaitForSecondsRealtime(1f);
        waitTimer = 5;

        isRoutineOn = false;
    }
    #endregion


    #region 교전
    // Engage 상태일 때 실행할 행동
    private IEnumerator DoEngage(GameObject monster_, MonsterStateMachine msm_)
    {
        // MonsterStateMachine 상태가 Engage 일 때만 Coroutine 지속
        while (msm_.currentState == MonsterStateMachine.State.Engage)
        {
            // 목표를 탐색하고 target 변수에 할당한다
            CheckTarget(monster_);

            // 타겟이 없다면
            if (target == null) 
            {
                // 타겟 재감지를 위한 카운트다운 코루틴을 실행한다
                yield return msm_.StartCoroutine(WaitForTarget(monster_, msm_));

                // 그래도 타겟이 없다면
                if (target == null) 
                {
                    // 코루틴 헛돎을 예방하는 break
                    yield break;
                }
            }
            // 타겟을 확보했다면
            else if (target != null) 
            {
                // 타겟 위치까지 움직이고 공격을 순차적으로 실행한다
                yield return msm_.StartCoroutine(EngageMove(monster_));
                yield return msm_.StartCoroutine(Attack(monster_));
            }
        }

        // 코루틴 작동 중 여부 체크용
        isRoutineOn = true;
    }
    #endregion


    #region 이동
    private IEnumerator EngageMove(GameObject monster_)
    {
        // 목표 좌표와의 거리가 3f 이하가 될 때까지만 while 문 지속
        // 몬스터의 애니메이션 공격 범위와 맞출 것
        while (Vector3.Distance(target.transform.position, monster_.transform.position) > 3f)
        {
            yield return null;

            Vector3 tempLook = new Vector3(target.transform.position.x,
                monster_.transform.position.y, target.transform.position.z);

            // 타겟을 바라보게 한다
            monster_.transform.LookAt(tempLook);

            Vector3 tempMove =
                new Vector3(target.transform.position.x - monster_.transform.position.x,
                gravity, target.transform.position.z - monster_.transform.position.z).normalized;

            // 타겟의 위치로 움직인다
            monsterControl.Move(tempMove * speed * Time.deltaTime);

            monsterAni.SetBool("isMoving", true);
        }
        monsterAni.SetBool("isMoving", false);

        // 이동을 마치면 타겟 변수를 초기화한다 (플레이어가 투명이나 이동마법으로 도망칠 수 있음)
        target = null;
    }
    #endregion


    #region 공격 (애니메이션 재생시간 염두)
    private IEnumerator Attack(GameObject monster_)
    {
        CheckTarget(monster_);

        // 만약 타겟이 없다면
        if (target == null) 
        { 
            /* Do Nothing */
        }
        // 타겟이 존재한다면
        else if (target != null) 
        {
            monsterAni.SetBool("isAttacking", true);

            // 애니메이션을 위한 대기 시간
            yield return new WaitForSecondsRealtime(3f);

            // 공격을 마치면 타겟 변수를 초기화한다 (플레이어가 투명이나 이동마법으로 도망칠 수 있음)
            target = null;
        }

        monsterAni.SetBool("isAttacking", false);
        //monsterAni.SetBool("isMoving", false);
    }
    #endregion


    #region 공격 타겟 Lost 시 대기 상태
    private IEnumerator WaitForTarget(GameObject monster_, MonsterStateMachine msm_)
    {
        int i = 0;

        // 타이머 설정한 부분까지 반복하고, 타겟이 존재하지 않을 때만 반복
        while (i < waitTimer && target == null) 
        {
            yield return waitTime;
            // n 초마다 타겟을 찾는다
            CheckTarget(monster_);
            i++;
        }

        // 루프를 다 돌아도 타겟이 없다면
        if (target == null) 
        {
            // 상태를 정찰로 변환한다
            msm_.ChangeState(MonsterStateMachine.State.Patrol);
        }
    }
    #endregion


    #region 타겟 확인 메서드
    private void CheckTarget(GameObject monster_)
    {
        // Detect -> Engage 일 경우 target 초기화 회피용 안전장치
        if (target != null) 
        {
            return;
        }

        // OverlapSphere 을 사용하여 Player Layer 를 검출시도한다
        Collider[] colliders = Physics.OverlapSphere(monster_.transform.position, radius, LayerMask.GetMask("Player"));

        //Debug.LogError(colliders.Length);

        // 만약 검출된 것이 없다면 
        if (colliders.Length <= 0) 
        {
            // 타겟 변수는 null 이다
            target = null;
            //Debug.LogWarning("타겟 없음");
        }
        // 만약 검출된 것이 있다면
        else if (colliders.Length > 0)
        {
            // foreach 반복을 통해 colliders 를 모두 검사한다
            foreach (var collider in colliders)
            {
                // 프로토타입
                if (collider.GetComponent<Rigidbody>() == true)
                {
                    target = collider.gameObject;
                    //Debug.LogWarning("타겟 찾음");
                }
            }
        }
    }
    #endregion


    #region 변수 비우는 메서드
    private void CleanVariables(GameObject monster_) 
    {
        monsterAni.SetBool("isMoving", false); 
        monsterAni.SetBool("isAttacking", false);

        monsterControl = default;
        monsterAni = default;
        speed = default;
        radius = default;
        isAttacking = false;
        target = null;
        waitTime = default;
        waitTimer = default;

        isRoutineOn = false;

        monster_.GetComponent<TestBigMonster>().sonarTarget = null;
    }
    #endregion
}

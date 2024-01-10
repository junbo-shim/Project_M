using System.Collections;
using UnityEngine;

public class Monster_Engage : MonsterState
{
    private CharacterController monsterControl;
    private Monster monsterComponent;
    private Animator monsterAni;
    private float speed;
    private float radius;
    private float atkRange;
    private float gravity;

    public GameObject target;

    private WaitForSeconds waitTime;
    private int waitTimer;




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
        msm_.StopCoroutine(EngageMove(monster_));
        msm_.StopCoroutine(Attack(monster_));
        msm_.StopCoroutine(WaitForTarget(monster_, msm_));
        msm_.StopCoroutine(DoEngage(monster_, msm_));
        
        // 변수 비우기
        CleanVariables();
    }





    #region 초기화
    private void Init(GameObject monster_)
    {
        // 몬스터 캐릭터 컨트롤러
        monsterControl = monster_.GetComponent<CharacterController>();
        monsterComponent = monster_.GetComponent<Monster>();
        // 몬스터 애니메이터
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();
        // Engage 속도, 범위, 타겟설정
        SetSpeedRadiusRangeTarget(monsterComponent.monsterData.MonsterType);
        // 중력 값
        gravity = -9.81f;
        // 교전 상대 놓칠 시 기다릴 타이머
        waitTime = new WaitForSeconds(1f);
        waitTimer = 5;
    }
    #endregion


    #region 낮밤 몬스터 구분하여 speed, radius, range, target 설정하는 함수
    private void SetSpeedRadiusRangeTarget(int type_)
    {
        if (monsterComponent == null) 
        {
            Debug.LogError("monsterComponent 가 null");
            return;
        }

        speed = monsterComponent.monsterData.MonsterMoveSpeed;
        radius = monsterComponent.monsterSightRange * 0.5f;
        //Debug.LogWarning(radius);
        atkRange = monsterComponent.monsterData.MonsterAttackRange;

        target = monsterComponent.target;
    }
    #endregion


    #region 교전
    // Engage 상태일 때 실행할 행동
    private IEnumerator DoEngage(GameObject monster_, MonsterStateMachine msm_)
    {
        if (Vector3.Distance(target.transform.position, monster_.transform.position) > monsterComponent.monsterSightRange)
        {
            target = null;
        }

        // MonsterStateMachine 상태가 Engage 일 때만 Coroutine 지속
        while (msm_.currentState.Equals(MonsterStateMachine.State.Engage)
            && !MapGameManager.instance.currentState.Equals(DayState.NIGHT))
        {
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

        if (MapGameManager.instance.currentState.Equals(DayState.NIGHT))
        {
            monsterComponent.monsterPool.ReturnObjToPool(monsterComponent.gameObject);
        }
    }
    #endregion


    #region 이동
    private IEnumerator EngageMove(GameObject monster_)
    {
        // 목표 좌표와의 거리가 3f 이하가 될 때까지만 while 문 지속
        // 몬스터의 애니메이션 공격 범위와 맞출 것
        while (Vector3.Distance(target.transform.position, monster_.transform.position) > atkRange)
        {
            // target 의 layer 가 Invisible 일 경우
            if(target.gameObject.layer.Equals(LayerMask.NameToLayer("Invisible")))
            {
                // target 해제 및 반복문 탈출
                target = null;
                yield break;
            }

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

            monsterAni.SetBool(monsterComponent.isMovingID, true);
        }
        monsterAni.SetBool(monsterComponent.isMovingID, false);

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
            yield break;
        }
        // 타겟이 존재한다면
        else if (target != null) 
        {
            monsterAni.SetBool(monsterComponent.isAttackingID, true);

            // 애니메이션이 모두 끝난 후까지 대기
            yield return new WaitUntil(() => monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

            // 공격을 마치면 타겟 변수를 초기화한다 (플레이어가 투명이나 이동마법으로 도망칠 수 있음)
            target = null;
            monsterAni.SetBool(monsterComponent.isAttackingID, false);
        }
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
        // !!! 현재 상태를 체크하지 않으면 문제발생
        if (target == null && msm_.currentState.Equals(MonsterStateMachine.State.Engage))
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

        // DEBUG
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
    private void CleanVariables() 
    {
        monsterAni.SetBool(monsterComponent.isMovingID, false); 
        monsterAni.SetBool(monsterComponent.isAttackingID, false);

        // 밤 몬스터일 경우 sonarTarget 변수 초기화
        if (monsterComponent.monsterData.MonsterType == 2) 
        {
            monsterComponent.target = null;
        }

        monsterControl = default;
        monsterComponent = default;
        monsterAni = default;
        speed = default;
        radius = default;
        target = null;
        waitTime = default;
        waitTimer = default;
    }
    #endregion
}

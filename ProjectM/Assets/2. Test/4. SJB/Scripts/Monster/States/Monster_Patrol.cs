using System.Collections;
using UnityEngine;

public class Monster_Patrol : MonsterState
{
    private CharacterController monsterControl;
    private Monster monsterComponent;
    private Animator monsterAni;
    private Vector3 patrolCenterPoint;
    private Vector3 destination;
    private float range;
    private float gravity;

    private float minWaitTime;
    private float maxWaitTime;




    public override void OnStateEnter(GameObject monster_) 
    {
        int monsterType = monster_.GetComponent<Monster>().monsterData.MonsterType;

        // 초기화
        Init(monster_);
        SetCenterPoint(monster_.transform.position);

        // 감지센서 On
        TurnSightSonarOnOff(true, monsterType);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_) 
    {
        msm_.StartCoroutine(DoPatrol(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_) 
    {
        
        int monsterType = monster_.GetComponent<Monster>().monsterData.MonsterType;

        // 만약 코루틴이 살아있을 경우를 대비한 안전장치
        msm_.StopCoroutine(PatrolMove(monster_));
        msm_.StopCoroutine(Wait());
        msm_.StopCoroutine(DoPatrol(monster_, msm_));

        // 감지센서 Off
        TurnSightSonarOnOff(false, monsterType);

        // 변수 비우기
        CleanVariables();
    }





    #region 초기화
    private void Init(GameObject monster_) 
    {
        // 몬스터 캐릭터 컨트롤러
        monsterControl = monster_.GetComponent<CharacterController>();
        // 몬스터 초기화
        monsterComponent = monster_.GetComponent<Monster>();
        // 몬스터 애니메이터
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();
        // Patrol 범위
        range = monsterComponent.monsterPatrolRange;
        // 중력 값
        gravity = -9.81f;
        // 최대최소 대기시간 값
        minWaitTime = 2f;
        maxWaitTime = 6f;
    }
    #endregion


    #region 낮밤 몬스터 구분하여 감지센서 OnOff 하는 함수
    private void TurnSightSonarOnOff(bool bool_, int type_) 
    {
        if (monsterComponent == null) 
        {
            Debug.LogError("monsterComponent 가 null");
            return;
        }

        monsterComponent.monsterSight.SetActive(bool_);

        switch (type_)
        {
            case 1:
                break;
            case 2:
                monsterComponent.monsterSonar.SetActive(bool_);
                break;
        }
    }
    #endregion


    #region 정찰 범위의 기준점을 설정
    // 정찰 범위의 중심점을 설정하는 메서드
    private void SetCenterPoint(Vector3 position_) 
    {
        // 만약 스폰이 이상한 곳에 될 경우에 대비하여 위 아래 terrain 체크 메서드 필요
        patrolCenterPoint = position_;
    }
    #endregion


    #region 정찰 범위에서 특정 좌표를 추출
    // 기준점을 중심으로 정찰 범위를 설정하고 그 범위 내의 랜덤 포인트를 추출하고 destination 에 설정하는 메서드
    private void SetDestination(Vector3 centerPoint_, float patrolRange_) 
    {
        // 반지름이 patrolRange_ 인 랜덤한 구를 centerPoint_ 을 중심으로 만들고, 그 안의 랜덤한 좌표를 뽑아 temp 에 저장한다
        Vector3 temp = centerPoint_ + (Random.insideUnitSphere * patrolRange_);

        // 레이를 캐스팅한 결과를 저장할 지역변수 생성
        RaycastHit hit;

        // temp 가 terrain 아래 있는지 체크하는 raycasthit
        // 정찰 포인트를 검증하는 과정 : Ray 를 위쪽으로 쏘게 되는 경우, Terrain 의 최저 높이에 가로막혀서 제대로 된 y 값이 나오지 않음
        bool isAboveTerrain = 
            Physics.Raycast(temp, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"));

        // temp 가 terrain 의 위쪽에서 나올 때 까지 루프를 돈다
        // while 문 종료조건 -> isAboveTerrain 이 true
        while (isAboveTerrain == false)
        {
            // 다시 temp 를 뽑는다
            temp = centerPoint_ + (Random.insideUnitSphere * patrolRange_);
            // 다시 isAboveTerrain 값을 체크한다
            isAboveTerrain = 
                Physics.Raycast(temp, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"));
        }

        #region DebugCode : Random Vector
        //GameObject temp2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //temp2.transform.position = temp;
        //temp2.GetComponent<BoxCollider>().enabled = false;
        //temp2.GetComponent<MeshRenderer>().material.color = Color.cyan;
        //Debug.LogError(temp);
        #endregion


        // hit point 의 y 좌표를 적용하여 목표 좌표를 저장한다
        destination = new Vector3(temp.x, hit.point.y, temp.z);


        #region DebugCode : Random Vector to Ground Vector
        //GameObject temp3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //temp3.transform.position = destination;
        //temp3.GetComponent<SphereCollider>().enabled = false;
        //temp3.GetComponent<MeshRenderer>().material.color = Color.red;
        //Debug.LogError(destination);
        #endregion
    }
    #endregion


    #region 정찰
    // Patrol 상태일 때 실행할 정찰 행동
    public IEnumerator DoPatrol(GameObject monster_, MonsterStateMachine msm_)
    {
        // MonsterStateMachine 상태가 Patrol 일 때만 Coroutine 지속
        while (msm_.currentState.Equals(MonsterStateMachine.State.Patrol) 
            && !MapGameManager.instance.currentState.Equals(DayState.NIGHT))
        {
            // PatrolMove 완료할 때까지 그 다음 행동이 대기한다
            yield return msm_.StartCoroutine(PatrolMove(monster_));
            // Wait 완료할 때까지 그 다음 행동이 대기한다
            yield return msm_.StartCoroutine(Wait());
        }

        if (MapGameManager.instance.currentState.Equals(DayState.NIGHT)) 
        {
            monsterComponent.monsterPool.ReturnObjToPool(monsterComponent.gameObject);
        }
    }
    #endregion


    #region 이동 및 대기
    // 이동 코루틴
    public IEnumerator PatrolMove(GameObject monster_) 
    {
        // 목표 좌표 지정
        SetDestination(patrolCenterPoint, range);

        // 목표 좌표와의 거리가 2f 이하가 될 때까지만 while 문 지속
        while (Vector3.Distance(destination, monster_.transform.position) > 2f) 
        {
            // 매 프레임 단위로 루프 작동 : Update 보다 성능에 악영향을 줄 수 있지만 특정 조건에서만 작동하도록 함
            yield return null;

            Vector3 tempLook = new Vector3(destination.x, monster_.transform.position.y, destination.z);

            monster_.transform.LookAt(tempLook);

            Vector3 tempMove = 
                new Vector3(destination.x - monster_.transform.position.x,
                gravity, destination.z - monster_.transform.position.z).normalized;

            monsterControl.Move(tempMove * monsterComponent.monsterMoveSpeed * Time.deltaTime);

            monsterAni.SetBool(monsterComponent.isMovingID, true);
        }
        monsterAni.SetBool(monsterComponent.isMovingID, false);
    }

    // 대기 코루틴
    public IEnumerator Wait() 
    {
        float random = Random.Range(minWaitTime, maxWaitTime);

        yield return new WaitForSeconds(random);
    }
    #endregion


    #region 변수 비우는 메서드
    private void CleanVariables() 
    {
        monsterAni.SetBool(monsterComponent.isMovingID, false);

        monsterControl = default;
        monsterComponent = default;
        monsterAni = default;
        patrolCenterPoint = default;
        destination = default;
        range = default;
        minWaitTime = default;
        maxWaitTime = default;
    }
    #endregion
}

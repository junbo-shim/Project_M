using System.Collections;
using UnityEngine;

public class Monster_Patrol : MonsterState
{
    private CharacterController monsterControl;
    private Vector3 patrolCenterPoint;
    private Vector3 destination;
    private float range;
    private float speed;

    private WaitUntil untilMonsterArrived;
    private WaitForSecondsRealtime returnPatrol;


    public override void OnStateEnter(GameObject monster_) 
    {
        Init(monster_);
        SetCenterPoint(monster_.transform.position);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_) 
    {
        msm_.StartCoroutine(SetPoint(monster_, msm_));
        msm_.StartCoroutine(Move(monster_, msm_));
    }

    public override void OnStateExit() 
    {
        monsterControl = default;
        patrolCenterPoint = default;
        destination = default;
        range = default;
        speed = default;
        untilMonsterArrived = default;
    }





    #region 초기화
    private void Init(GameObject monster_) 
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        range = monster_.GetComponent<TestMonster>().patrolRange;
        speed = monster_.GetComponent<TestMonster>().moveSpeed;
        untilMonsterArrived = new WaitUntil(() => Vector3.Distance(destination, monster_.transform.position) < 1.5f);
        returnPatrol = new WaitForSecondsRealtime(4f);
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
    private void SetDesstination(Vector3 centerPoint_, float patrolRange_) 
    {
        // 반지름이 patrolRange_ 인 랜덤한 구를 centerPoint_ 을 중심으로 만들고, 그 안의 랜덤한 좌표를 뽑아 temp 에 저장한다
        Vector3 temp = centerPoint_ + (Random.insideUnitSphere * patrolRange_);

        // 레이를 캐스팅한 결과를 저장할 지역변수 생성
        RaycastHit hit;

        // temp 가 terrain 아래 있는지 체크하는 raycasthit
        // 정찰 포인트를 검증하는 과정 : Ray 를 위쪽으로 쏘게 되는 경우, Terrain 의 최저 높이에 가로막혀서 제대로 된 y 값이 나오지 않음
        bool isAboveTerrain = Physics.Raycast(temp, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"));

        // temp 가 terrain 의 위쪽에서 나올 때 까지 루프를 돈다
        // while 문 종료조건 -> isAboveTerrain 이 true
        while (isAboveTerrain == false)
        {
            // 다시 temp 를 뽑는다
            temp = centerPoint_ + (Random.insideUnitSphere * patrolRange_);
            // 다시 isAboveTerrain 값을 체크한다
            isAboveTerrain = Physics.Raycast(temp, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"));
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


    #region 정찰 행동
    // 포인트 지정 코루틴
    public IEnumerator SetPoint(GameObject monster_, MonsterStateMachine msm_) 
    {
        // MonsterStateMachine 상태가 Patrol 일 때만 Coroutine 사용
        while (msm_.currentState == MonsterStateMachine.State.Patrol) 
        {
            // untilMonsterArrived 거리가 1.5f 이하일 때까지 hold 하는 로직
            yield return untilMonsterArrived;
            //yield return returnPatrol;
            SetDesstination(patrolCenterPoint, range);
            Debug.LogWarning("SetPoint 작동중");
        }   
    }
    // 이동 코루틴
    public IEnumerator Move(GameObject monster_, MonsterStateMachine msm_) 
    {
        SetDesstination(patrolCenterPoint, range);

        // MonsterStateMachine 상태가 Patrol 일 때만 Coroutine 사용
        while (msm_.currentState == MonsterStateMachine.State.Patrol) 
        {
            // 매 프레임 단위로 루프 작동 : Update 보다 성능에 악영향을 줄 수 있지만 특정 조건에서만 작동하도록 함
            yield return null;
            //Debug.LogWarning(Vector3.Distance(destination, monster_.transform.position));
            monster_.transform.LookAt(destination + new Vector3(0f, monster_.transform.position.y, 0f));
            monsterControl.Move((destination - monster_.transform.position) * speed * Time.deltaTime);
            Debug.LogWarning("Move 작동중");
        }
    }
    #endregion
}

using System.Collections;
using UnityEngine;

public class Monster_Return : MonsterState
{
    private CharacterController monsterControl;
    private Monster monsterComponent;
    private Animator monsterAni;
    private Vector3 returnPoint;
    private float gravity;


    public override void OnStateEnter(GameObject monster_)
    {
        int monsterType = monster_.GetComponent<Monster>().monsterData.MonsterType;

        Init(monster_);
        // 감지센서 On
        TurnSightSonarOnOff(true, monsterType);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StartCoroutine(DoReturn(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_)
    {
        int monsterType = monster_.GetComponent<Monster>().monsterData.MonsterType;

        msm_.StopCoroutine(ReturnMove(monster_));
        msm_.StopCoroutine(DoReturn(monster_, msm_));

        // 감지센서 Off
        TurnSightSonarOnOff(false, monsterType);
        CleanVariables();
    }




    #region 초기화
    private void Init(GameObject monster_)
    {
        // 몬스터 캐릭터 컨트롤러
        monsterControl = monster_.GetComponent<CharacterController>();
        // 몬스터 컴포넌트
        monsterComponent = monster_.GetComponent<Monster>();
        // 몬스터 애니메이터
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();
        // 복귀할 지점
        returnPoint = monsterComponent.monsterSpawnedPoint;
        gravity = -9.81f;
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

    #region 복귀
    private IEnumerator DoReturn(GameObject monster_, MonsterStateMachine msm_)
    {
        // 만약 밤몬스터일 경우
        if (monsterComponent.thisMonsterType.Equals(Monster.MonsterType.Mech_Large))
        {
            // MonsterStateMachine 상태가 Return 일 때만 Coroutine 지속
            // 낮 상태가 아닐때만 Coroutine 지속
            while (msm_.currentState.Equals(MonsterStateMachine.State.Return) &&
                !MapGameManager.instance.currentState.Equals(DayState.MORNING))
            {
                // PatrolMove 완료할 때까지 그 다음 행동이 대기한다
                yield return msm_.StartCoroutine(ReturnMove(monster_));
            }

            // Coroutine 이 끝나면 현재가 낮인지 아닌지 체크한다
            if (MapGameManager.instance.currentState.Equals(DayState.MORNING))
            {
                // 낮이면 풀로 돌아간다
                monsterComponent.monsterPool.ReturnObjToPool(monsterComponent.gameObject);
            }
            else 
            {
                // ReturnMove 가 끝나면 다시 Patrol 로 돌아감
                msm_.ChangeState(MonsterStateMachine.State.Patrol);
            }
        }
        else
        {
            // MonsterStateMachine 상태가 Return 일 때만 Coroutine 지속
            // 밤 상태가 아닐때만 Coroutine 지속
            while (msm_.currentState.Equals(MonsterStateMachine.State.Return) &&
                !MapGameManager.instance.currentState.Equals(DayState.NIGHT))
            {
                // PatrolMove 완료할 때까지 그 다음 행동이 대기한다
                yield return msm_.StartCoroutine(ReturnMove(monster_));
            }

            // Coroutine 이 끝나면 현재가 밤인지 아닌지 체크한다
            if (MapGameManager.instance.currentState.Equals(DayState.NIGHT))
            {
                // 밤이면 풀로 돌아간다
                monsterComponent.monsterPool.ReturnObjToPool(monsterComponent.gameObject);
            }
            else 
            {
                // ReturnMove 가 끝나면 다시 Patrol 로 돌아감
                msm_.ChangeState(MonsterStateMachine.State.Patrol);
            }
        }
    }
    #endregion

    #region 스폰한 포인트로 복귀
    private IEnumerator ReturnMove(GameObject monster_)
    {
        // 복귀지점까지 돌아갈때까지 코루틴 반복
        while (Vector3.Distance(returnPoint, monster_.transform.position) > 1f)
        {
            // 매 프레임 단위로 루프 작동 : Update 보다 성능에 악영향을 줄 수 있지만 특정 조건에서만 작동하도록 함
            yield return null;
            Vector3 tempLook = new Vector3(returnPoint.x, monster_.transform.position.y, returnPoint.z);
            monster_.transform.LookAt(tempLook);
            Vector3 tempMove =
                new Vector3(returnPoint.x - monster_.transform.position.x,
                gravity, returnPoint.z - monster_.transform.position.z).normalized;

            monsterControl.Move(tempMove * monsterComponent.monsterMoveSpeed * Time.deltaTime);

            monsterAni.SetBool(monsterComponent.isMovingID, true);
        }
        monsterAni.SetBool(monsterComponent.isMovingID, false);
    }
    #endregion

    #region 변수 비우는 메서드
    private void CleanVariables()
    {
        monsterControl = default;
        monsterComponent = default;
        monsterAni = default;
        returnPoint = default;
        gravity = default;
    }
    #endregion
}


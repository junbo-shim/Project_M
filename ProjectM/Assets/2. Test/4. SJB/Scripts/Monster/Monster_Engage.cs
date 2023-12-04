using System.Collections;
using UnityEngine;

public class Monster_Engage : MonsterState
{
    private CharacterController monsterControl;
    private float speed;
    private float radius;

    private bool isAttacking;

    public GameObject target;


    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StartCoroutine(DoEngage(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_)
    {
        CleanVariables();
    }





    #region 초기화
    private void Init(GameObject monster_)
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        speed = 1f;
        radius = 10f;
        isAttacking = false;
    }
    #endregion


    #region 교전
    // Engage 상태일 때 실행할 행동
    private IEnumerator DoEngage(GameObject monster_, MonsterStateMachine msm_)
    {
        // MonsterStateMachine 상태가 Engage 일 때만 Coroutine 지속
        while (msm_.currentState == MonsterStateMachine.State.Engage)
        {
            yield return msm_.StartCoroutine(EngageMove(monster_));
            yield return msm_.StartCoroutine(CheckAttack(monster_, msm_));
        }
    }
    #endregion


    #region 이동
    private IEnumerator EngageMove(GameObject monster_)
    {
        // 목표를 탐색하고 target 변수에 할당한다
        CheckTarget(monster_);

        // 목표 좌표와의 거리가 3f 이하가 될 때까지만 while 문 지속
        // 몬스터의 애니메이션 공격 범위와 맞출 것
        while (Vector3.Distance(target.transform.position, monster_.transform.position) > 3f)
        {
            yield return null;

            Vector3 temp = new Vector3(target.transform.position.x,
                monster_.transform.position.y, target.transform.position.z);

            monster_.transform.LookAt(temp);
            monsterControl.Move((target.transform.position - monster_.transform.position) * speed * Time.deltaTime);
        }
    }
    #endregion


    #region 공격체크
    private IEnumerator CheckAttack(GameObject monster_, MonsterStateMachine msm_)
    {
        CheckTarget(monster_);

        if (target == null)
        {
            Debug.LogWarning("null");
            //yield return msm_.StartCoroutine(WaitForTarget(monster_, msm_));
        }
        else if (target != null)
        {
            Debug.LogWarning("Attack 시작");
            yield return msm_.StartCoroutine(Attack());
        }
    }
    #endregion


    #region 데미지 (애니메이션 재생시간 염두)
    private IEnumerator Attack()
    {
        Debug.Log("true");
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("false");
        target = default;
    }
    #endregion


    #region 공격 타겟 Lost 시 대기 상태
    private IEnumerator WaitForTarget(GameObject monster_, MonsterStateMachine msm_)
    {
        int i = 0;

        while (i < 5)
        {
            yield return new WaitForSecondsRealtime(1f);
            CheckTarget(monster_);

            if (target != null)
            {
                msm_.StopAllCoroutines();
                msm_.StartCoroutine(DoEngage(monster_, msm_));
            }

            Debug.Log("Now :" + i);
            i++;
            Debug.Log("Next :" + i);
        }

        if (target == null)
        {
            Debug.Log("목표 상실, 정찰로 변경");
            msm_.StopAllCoroutines();
            // 프로토타입
            msm_.ChangeState(MonsterStateMachine.State.Patrol);
        }
    }
    #endregion


    #region 타겟 확인
    private void CheckTarget(GameObject monster_)
    {
        Collider[] colliders = Physics.OverlapSphere(monster_.transform.position, radius, LayerMask.GetMask("Player"));

        Debug.LogError(colliders.Length);

        foreach (var collider in colliders)
        {
            Debug.LogError(collider.name);

            // 프로토타입
            if (collider.GetComponent<Rigidbody>() == true)
            {
                Debug.LogWarning("플레이어 확인" + collider.name);
                target = collider.gameObject;
            }
            else 
            {
                Debug.LogWarning("target lost");
                target = null;
            }
        }
    }
    #endregion


    private void CleanVariables() 
    {
        monsterControl = default;
        speed = default;
        radius = default;
        isAttacking = false;
        target = null;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class Monster_Detect : MonsterState
{
    private float radius;

    public GameObject target;
    public WaitForSeconds waitTime;
    public int waitTimer;



    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StartCoroutine(DoDetect(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StopCoroutine(DoDetect(monster_, msm_));
        // 변수 비우기
        CleanVariables(monster_);
    }





    #region 초기화
    private void Init(GameObject monster_)
    {
        radius = monster_.GetComponent<Monster>().monsterSonarRange;
        waitTime = new WaitForSeconds(1f);
        waitTimer = 5;

        monster_.GetComponent<Monster>().detectUI.SetActive(true);
    }
    #endregion


    #region 감지
    // 감지 상태일 때 실행할 행동
    private IEnumerator DoDetect(GameObject monster_, MonsterStateMachine msm_) 
    {
        int i = 0;

        // 타이머 설정한 부분까지 반복
        while (i < waitTimer)
        {
            yield return waitTime;
            i++;
        }

        // 타겟 존재 여부를 체크하기 위한 CheckTarget
        CheckTarget(monster_);

        if (target != null) 
        {
            monster_.GetComponent<Monster>().detectUI.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = "!";
        }
        yield return new WaitForSeconds(2f);

        // 타겟이 없다면
        if (target == null)
        {
            // 상태를 정찰로 변환한다
            msm_.ChangeState(MonsterStateMachine.State.Patrol);
        }
        else if (target != null) 
        {
            // 상태를 교전으로 변환한다
            msm_.ChangeState(MonsterStateMachine.State.Engage);
        }
    }
    #endregion


    #region 타겟 확인 메서드
    private void CheckTarget(GameObject monster_)
    {
        // OverlapSphere 을 사용하여 Player Layer 를 검출시도한다
        Collider[] colliders = Physics.OverlapSphere(monster_.transform.position, radius, LayerMask.GetMask("PlayerFoot"));

        //Debug.LogError(colliders.Length);

        // 만약 검출된 것이 없다면 
        if (colliders.Length <= 0)
        {
            // 타겟 변수는 null 이다
            target = null;
            monster_.GetComponent<Monster>().target = null;
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
                    monster_.GetComponent<Monster>().target = collider.transform.parent.gameObject;
                    target = monster_.GetComponent<Monster>().target;
                }
            }
        }
    }
    #endregion


    #region 변수 비우는 메서드
    private void CleanVariables(GameObject monster_)
    {
        radius = default;
        target = null;
        waitTime = default;
        waitTimer = default;

        monster_.GetComponent<Monster>().detectUI.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = "?";
        monster_.GetComponent<Monster>().detectUI.SetActive(false);
    }
    #endregion
}

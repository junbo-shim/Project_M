using System.Collections;
using UnityEngine;

public class Monster_Die : MonsterState
{
    private CharacterController monsterControl;
    private Monster monsterComponent;
    private Animator monsterAni;

    private int randomNum;

    private WaitForSeconds dieAnimatorTime;




    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_)
    {
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
        // 죽음 애니메이션 인덱스 번호
        randomNum = Random.Range(0, 2);
        // 죽음 애니메이션 길이
        dieAnimatorTime = new WaitForSeconds(monsterComponent.monsterDeathSpeed);
    }
    #endregion

    #region 죽는 애니메이션 재생
    private IEnumerator PlayDieAnimation()
    {


        // 애니메이션 재생
        monsterAni.SetTrigger(monsterComponent.deadID);
        monsterAni.SetInteger("DieNumber", randomNum);

        // 애니메이션이 모두 끝난 후까지 대기
        yield return dieAnimatorTime;


    }
    #endregion

    #region 변수 비우는 메서드
    private void CleanVariables()
    {
        monsterControl = default;
        monsterComponent = default;
        monsterAni = default;
        randomNum = default;
    }
    #endregion
}

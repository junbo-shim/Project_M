using UnityEngine;

public class Monster_Die : MonsterState
{
    private CharacterController monsterControl;
    private Monster monsterComponent;




    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        Debug.Log("죽음 중");
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
    }
    #endregion

    #region 변수 비우는 메서드
    private void CleanVariables()
    {
        monsterControl = default;
        monsterComponent = default;
    }
    #endregion
}

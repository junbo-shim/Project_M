using UnityEngine;

public class Boss_Ready : MonsterState
{
    private Monster monsterComponent;




    public override void OnStateEnter(GameObject monster_)
    {
        ResetHPSpeed(monster_);
    }
    public override void OnStateExit(BossStateMachine bossFSM_)
    {
        CleanVariables();
    }




    #region 초기화
    private void ResetHPSpeed(GameObject monster_)
    {
        monsterComponent = monster_.GetComponent<Monster>();
        monsterComponent.monsterHP = monsterComponent.monsterData.MonsterHP;
        monsterComponent.monsterMoveSpeed = monsterComponent.monsterData.MonsterMoveSpeed;
    }
    #endregion

    #region 변수 비우는 메서드
    private void CleanVariables()
    {
        monsterComponent = default;
    }
    #endregion
}

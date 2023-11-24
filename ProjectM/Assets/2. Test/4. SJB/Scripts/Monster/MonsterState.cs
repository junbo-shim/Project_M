using UnityEngine;

public class MonsterState
{
    // 상태 진입 시 호출되는 메서드
    public virtual void OnStateEnter() { }

    // 상태 중 호출되는 메서드
    public virtual void OnStateStay() { }

    // 상태 탈출 시 호출되는 메서드
    public virtual void OnStateExit() { }

    #region PATROL
    // 상태 진입 시 호출되는 메서드 - 오버로드(patrol)
    public virtual void OnStateEnter(GameObject monster_) { }

    // 상태 중 호출되는 메서드 - 오버로드(patrol)
    public virtual void OnStateStay(GameObject monster_, MonsterStateMachine msm_) { }
    #endregion
}

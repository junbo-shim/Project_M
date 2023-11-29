using UnityEngine;

public class MonsterState
{
    // ���� ���� �� ȣ��Ǵ� �޼���
    public virtual void OnStateEnter() { }

    // ���� �� ȣ��Ǵ� �޼���
    public virtual void OnStateStay() { }

    // ���� Ż�� �� ȣ��Ǵ� �޼���
    public virtual void OnStateExit() { }

    #region PATROL
    // ���� ���� �� ȣ��Ǵ� �޼��� - �����ε�(patrol)
    public virtual void OnStateEnter(GameObject monster_) { }

    // ���� �� ȣ��Ǵ� �޼��� - �����ε�(patrol)
    public virtual void OnStateStay(GameObject monster_, MonsterStateMachine msm_) { }
    #endregion

    #region Engage
    #endregion
}

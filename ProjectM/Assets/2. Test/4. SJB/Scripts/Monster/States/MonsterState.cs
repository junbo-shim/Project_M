using UnityEngine;

public class MonsterState
{
    // ���� ���� �� ȣ��Ǵ� �޼���
    public virtual void OnStateEnter() { }

    // ���� �� ȣ��Ǵ� �޼���
    public virtual void OnStateStay() { }

    // ���� Ż�� �� ȣ��Ǵ� �޼���
    public virtual void OnStateExit() { }

    // ���� ���� �� ȣ��Ǵ� �޼��� - �����ε�
    public virtual void OnStateEnter(GameObject monster_) { }

    // ���� �� ȣ��Ǵ� �޼��� - �����ε�
    public virtual void OnStateStay(GameObject monster_, MonsterStateMachine msm_) { }

    // ���� Ż�� �� ȣ��Ǵ� �޼��� - �����ε�
    public virtual void OnStateExit(GameObject monster_, MonsterStateMachine msm_) { }
}

using UnityEngine;

public abstract class MonsterState : MonoBehaviour
{
    // ���� ���� �� ȣ��Ǵ� �޼���
    public abstract void OnStateEnter();

    // ���� �� ȣ��Ǵ� �޼���
    public abstract void OnStateStay();

    // ���� Ż�� �� ȣ��Ǵ� �޼���
    public abstract void OnStateExit();
}

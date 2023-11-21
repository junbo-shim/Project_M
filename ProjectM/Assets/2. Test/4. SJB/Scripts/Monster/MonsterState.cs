using UnityEngine;

public abstract class MonsterState : MonoBehaviour
{
    // 상태 진입 시 호출되는 메서드
    public abstract void OnStateEnter();

    // 상태 중 호출되는 메서드
    public abstract void OnStateStay();

    // 상태 탈출 시 호출되는 메서드
    public abstract void OnStateExit();
}

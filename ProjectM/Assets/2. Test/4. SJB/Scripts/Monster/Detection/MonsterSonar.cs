using UnityEngine;

public class MonsterSonar : MonoBehaviour
{
    public GameObject monster;
    public float listenRange;

    private void Start()
    {
        InitSonar();
    }

    // 밤몬스터 청각 초기화
    private void InitSonar()
    {
        // 몬스터의 청각 범위
        listenRange = monster.GetComponent<Monster>().monsterSonarRange;
        transform.localScale = Vector3.one * listenRange;
    }

    private void OnTriggerEnter(Collider other_) 
    {
        // 만약 충돌 물체가 PlayerFoot 레이어를 가졌다면
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerFoot"))) 
        {
            // 몬스터의 FSM 에 접근하여 상태를 감지로 변경
            monster.GetComponent<Monster>().monsterFSM.ChangeState(MonsterStateMachine.State.Detect);
        }
    }
}

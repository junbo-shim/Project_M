using UnityEngine;

public class NightMonsterSight : MonoBehaviour
{
    public GameObject monster;

    private void OnTriggerEnter(Collider other_)
    {
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            // 몬스터의 target 변수에 해당 오브젝트 전달
            monster.GetComponent<Monster>().target = other_.gameObject;
            // 몬스터의 FSM 에 접근하여 상태를 교전으로 변경
            monster.GetComponent<Monster>().monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
        }
    }
}

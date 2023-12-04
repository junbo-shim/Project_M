using Unity.VisualScripting;
using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    public GameObject monster;
    public float viewAngle;
    public bool isInAngle;


    private void Start()
    {
        InitSight();
    }

    // 몬스터 시야 초기화
    private void InitSight() 
    {
        // 몬스터의 키(transform.position.y)만큼 아래로 낮춘다
        transform.position = monster.transform.position - new Vector3(0f, monster.transform.position.y, 0f);

        // 몬스터에서 변수를 가져온다
        viewAngle = monster.GetComponent<TestMonster>().sightAngle * 0.5f;

        // 시야 내에 들어왔는가를 체크할 bool
        isInAngle = false;
    }

    private void OnTriggerStay(Collider other_)
    {
        // 프로토타입
        if (other_.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 역탄젠트를 이용하여 플레이어와 부딪힌 좌표의 각을 구한다
            float angle = Mathf.Atan2(other_.transform.position.z - transform.position.z, 
                other_.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

            // 그 각도를 체크해서 만약 true 값이 반환된다면
            if (Check(angle) == true)
            {
                // 프로토타입
                // 몬스터의 player 변수에 부딪힌 플레이어를 담는다
                monster.GetComponent<TestMonster>().player = other_.gameObject;
                // 몬스터의 FSM 에 접근하여 상태를 교전으로 변경
                monster.GetComponent<TestMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
            }
        }
    }

    private bool Check(float angle_)
    {
        // 몬스터의 앞쪽에서 지정된 각도(sightAngle)에만 반응하게 하려면
        // sightAngle 의 절반(viewAngle) 으로
        // + viewAngle , 180 - viewAngle 안에 있는지 확인하면 된다
        if (angle_ > viewAngle && angle_ < 180f - viewAngle)
        {
            isInAngle = true;
        }
        else
        {
            isInAngle = false;
        }
        return isInAngle;
    }
}

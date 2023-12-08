using UnityEngine;

public class MonsterSonar : MonoBehaviour
{
    public GameObject monster;
    public float listenRange;

    public float viewAngle;
    public float viewRange;
    public bool isInAngle;


    private void Start()
    {
        InitSonar();
    }

    // 빅몬스터 청각 초기화
    private void InitSonar()
    {
        // 빅몬스터의 키(transform.position.y)만큼 아래로 낮춘다
        transform.position = monster.transform.position - new Vector3(0f, monster.transform.position.y, 0f);

        // 빅몬스터에서 변수를 가져온다
        listenRange = monster.GetComponent<TestBigMonster>().sonarRange;

        viewAngle = monster.GetComponent<TestBigMonster>().sightAngle * 0.5f;
        viewRange = monster.GetComponent<TestBigMonster>().sightRange;

        isInAngle = false;
    }

    private void OnTriggerEnter(Collider other_) 
    {
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerFoot"))) 
        {
            monster.GetComponent<TestBigMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Detect);
        }
    }
}

using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    public GameObject monster;
    public float viewAngle;
    public float viewRange;
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

        if (monster.GetComponent<TestMonster>() == true) 
        {
            // 몬스터에서 변수를 가져온다
            viewAngle = monster.GetComponent<TestMonster>().sightAngle * 0.5f;
            viewRange = monster.GetComponent<TestMonster>().sightRange;
        }
        else if (monster.GetComponent<TestBigMonster>() == true) 
        {
            viewAngle = monster.GetComponent<TestBigMonster>().sightAngle * 0.5f;
            viewRange = monster.GetComponent<TestBigMonster>().sightRange;
        }

        // 시야 내에 들어왔는가를 체크할 bool
        isInAngle = false;
    }


    private void OnTriggerStay(Collider other_)
    {
        // 프로토타입
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            CalcAngle(other_);
        }
    }
    

    // 몬스터의 정면으로부터 내적하는 각도 구하는 메서드
    private void CalcAngle(Collider other_) 
    {
        // 몬스터의 시선 정면을 향하는 벡터를 구한다
        Vector3 frontVec = monster.transform.forward * viewRange;
        // 몬스터로부터 플레이어까지의 방향을 가진 벡터값을 구한다
        Vector3 otherVec = other_.transform.position - monster.transform.position;

        // 두 벡터간의 내적을 구한다 (음수 또는 양수의 형태)
        float dot = Vector3.Dot(frontVec, otherVec);
        // 두 벡터 크기의 곱을 구한다 (항상 양수)
        float magResult = Vector3.Magnitude(frontVec) * Vector3.Magnitude(otherVec);

        // (A*B / |A|*|B|) 의 역코사인 값을 구한다 (라디안 -> 각도)
        float angle = Mathf.Acos(dot / magResult) * Mathf.Rad2Deg;
        //Debug.Log(angle);

        // 그 각도를 체크해서 만약 true 값이 반환된다면
        if (Check(angle) == true)
        {
            if (monster.GetComponent<TestMonster>() == true)
            {
                Debug.LogWarning("???");
                // 몬스터의 FSM 에 접근하여 상태를 교전으로 변경
                monster.GetComponent<TestMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
            }
            else if (monster.GetComponent<TestBigMonster>() == true)
            {
                monster.GetComponent<TestBigMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
            }
        }
    }


    // 각도 검사
    private bool Check(float angle_)
    {
        if (angle_ <= viewAngle)
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

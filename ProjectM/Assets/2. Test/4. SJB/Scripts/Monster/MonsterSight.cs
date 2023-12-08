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

    // ���� �þ� �ʱ�ȭ
    private void InitSight() 
    {
        // ������ Ű(transform.position.y)��ŭ �Ʒ��� �����
        transform.position = monster.transform.position - new Vector3(0f, monster.transform.position.y, 0f);

        if (monster.GetComponent<TestMonster>() == true) 
        {
            // ���Ϳ��� ������ �����´�
            viewAngle = monster.GetComponent<TestMonster>().sightAngle * 0.5f;
            viewRange = monster.GetComponent<TestMonster>().sightRange;
        }
        else if (monster.GetComponent<TestBigMonster>() == true) 
        {
            viewAngle = monster.GetComponent<TestBigMonster>().sightAngle * 0.5f;
            viewRange = monster.GetComponent<TestBigMonster>().sightRange;
        }

        // �þ� ���� ���Դ°��� üũ�� bool
        isInAngle = false;
    }


    private void OnTriggerStay(Collider other_)
    {
        // ������Ÿ��
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            CalcAngle(other_);
        }
    }
    

    // ������ �������κ��� �����ϴ� ���� ���ϴ� �޼���
    private void CalcAngle(Collider other_) 
    {
        // ������ �ü� ������ ���ϴ� ���͸� ���Ѵ�
        Vector3 frontVec = monster.transform.forward * viewRange;
        // ���ͷκ��� �÷��̾������ ������ ���� ���Ͱ��� ���Ѵ�
        Vector3 otherVec = other_.transform.position - monster.transform.position;

        // �� ���Ͱ��� ������ ���Ѵ� (���� �Ǵ� ����� ����)
        float dot = Vector3.Dot(frontVec, otherVec);
        // �� ���� ũ���� ���� ���Ѵ� (�׻� ���)
        float magResult = Vector3.Magnitude(frontVec) * Vector3.Magnitude(otherVec);

        // (A*B / |A|*|B|) �� ���ڻ��� ���� ���Ѵ� (���� -> ����)
        float angle = Mathf.Acos(dot / magResult) * Mathf.Rad2Deg;
        //Debug.Log(angle);

        // �� ������ üũ�ؼ� ���� true ���� ��ȯ�ȴٸ�
        if (Check(angle) == true)
        {
            if (monster.GetComponent<TestMonster>() == true)
            {
                // ������ FSM �� �����Ͽ� ���¸� �������� ����
                monster.GetComponent<TestMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
            }
            else if (monster.GetComponent<TestBigMonster>() == true)
            {
                monster.GetComponent<TestBigMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
            }
        }
    }


    // ���� �˻�
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

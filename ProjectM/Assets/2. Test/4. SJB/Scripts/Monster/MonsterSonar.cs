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

    // ����� û�� �ʱ�ȭ
    private void InitSonar()
    {
        // ������� Ű(transform.position.y)��ŭ �Ʒ��� �����
        transform.position = monster.transform.position - new Vector3(0f, monster.transform.position.y, 0f);

        // ����Ϳ��� ������ �����´�
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

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

    // ���� �þ� �ʱ�ȭ
    private void InitSight() 
    {
        // ������ Ű(transform.position.y)��ŭ �Ʒ��� �����
        transform.position = monster.transform.position - new Vector3(0f, monster.transform.position.y, 0f);

        // ���Ϳ��� ������ �����´�
        viewAngle = monster.GetComponent<TestMonster>().sightAngle * 0.5f;

        // �þ� ���� ���Դ°��� üũ�� bool
        isInAngle = false;
    }

    private void OnTriggerStay(Collider other_)
    {
        // ������Ÿ��
        if (other_.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // ��ź��Ʈ�� �̿��Ͽ� �÷��̾�� �ε��� ��ǥ�� ���� ���Ѵ�
            float angle = Mathf.Atan2(other_.transform.position.z - transform.position.z, 
                other_.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

            // �� ������ üũ�ؼ� ���� true ���� ��ȯ�ȴٸ�
            if (Check(angle) == true)
            {
                // ������Ÿ��
                // ������ player ������ �ε��� �÷��̾ ��´�
                monster.GetComponent<TestMonster>().player = other_.gameObject;
                // ������ FSM �� �����Ͽ� ���¸� �������� ����
                monster.GetComponent<TestMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
            }
        }
    }

    private bool Check(float angle_)
    {
        // ������ ���ʿ��� ������ ����(sightAngle)���� �����ϰ� �Ϸ���
        // sightAngle �� ����(viewAngle) ����
        // + viewAngle , 180 - viewAngle �ȿ� �ִ��� Ȯ���ϸ� �ȴ�
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

using System.Collections;
using UnityEngine;

public class Monster_Patrol : MonsterState
{
    private CharacterController monsterControl;
    private Vector3 patrolCenterPoint;
    private Vector3 destination;
    private float range;
    private float speed;

    private WaitUntil untilMonsterArrived;
    private WaitForSecondsRealtime returnPatrol;


    public override void OnStateEnter(GameObject monster_) 
    {
        Init(monster_);
        SetCenterPoint(monster_.transform.position);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_) 
    {
        msm_.StartCoroutine(SetPoint(monster_, msm_));
        msm_.StartCoroutine(Move(monster_, msm_));
    }

    public override void OnStateExit() 
    {
        monsterControl = default;
        patrolCenterPoint = default;
        destination = default;
        range = default;
        speed = default;
        untilMonsterArrived = default;
    }





    #region �ʱ�ȭ
    private void Init(GameObject monster_) 
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        range = monster_.GetComponent<TestMonster>().patrolRange;
        speed = monster_.GetComponent<TestMonster>().moveSpeed;
        untilMonsterArrived = new WaitUntil(() => Vector3.Distance(destination, monster_.transform.position) < 1.5f);
        returnPatrol = new WaitForSecondsRealtime(4f);
    }
    #endregion


    #region ���� ������ �������� ����
    // ���� ������ �߽����� �����ϴ� �޼���
    private void SetCenterPoint(Vector3 position_) 
    {
        // ���� ������ �̻��� ���� �� ��쿡 ����Ͽ� �� �Ʒ� terrain üũ �޼��� �ʿ�
        patrolCenterPoint = position_;
    }
    #endregion


    #region ���� �������� Ư�� ��ǥ�� ����
    // �������� �߽����� ���� ������ �����ϰ� �� ���� ���� ���� ����Ʈ�� �����ϰ� destination �� �����ϴ� �޼���
    private void SetDesstination(Vector3 centerPoint_, float patrolRange_) 
    {
        // �������� patrolRange_ �� ������ ���� centerPoint_ �� �߽����� �����, �� ���� ������ ��ǥ�� �̾� temp �� �����Ѵ�
        Vector3 temp = centerPoint_ + (Random.insideUnitSphere * patrolRange_);

        // ���̸� ĳ������ ����� ������ �������� ����
        RaycastHit hit;

        // temp �� terrain �Ʒ� �ִ��� üũ�ϴ� raycasthit
        // ���� ����Ʈ�� �����ϴ� ���� : Ray �� �������� ��� �Ǵ� ���, Terrain �� ���� ���̿� ���θ����� ����� �� y ���� ������ ����
        bool isAboveTerrain = Physics.Raycast(temp, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"));

        // temp �� terrain �� ���ʿ��� ���� �� ���� ������ ����
        // while �� �������� -> isAboveTerrain �� true
        while (isAboveTerrain == false)
        {
            // �ٽ� temp �� �̴´�
            temp = centerPoint_ + (Random.insideUnitSphere * patrolRange_);
            // �ٽ� isAboveTerrain ���� üũ�Ѵ�
            isAboveTerrain = Physics.Raycast(temp, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain"));
        }

        #region DebugCode : Random Vector
        //GameObject temp2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //temp2.transform.position = temp;
        //temp2.GetComponent<BoxCollider>().enabled = false;
        //temp2.GetComponent<MeshRenderer>().material.color = Color.cyan;
        //Debug.LogError(temp);
        #endregion


        // hit point �� y ��ǥ�� �����Ͽ� ��ǥ ��ǥ�� �����Ѵ�
        destination = new Vector3(temp.x, hit.point.y, temp.z);


        #region DebugCode : Random Vector to Ground Vector
        //GameObject temp3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //temp3.transform.position = destination;
        //temp3.GetComponent<SphereCollider>().enabled = false;
        //temp3.GetComponent<MeshRenderer>().material.color = Color.red;
        //Debug.LogError(destination);
        #endregion
    }
    #endregion


    #region ���� �ൿ
    // ����Ʈ ���� �ڷ�ƾ
    public IEnumerator SetPoint(GameObject monster_, MonsterStateMachine msm_) 
    {
        // MonsterStateMachine ���°� Patrol �� ���� Coroutine ���
        while (msm_.currentState == MonsterStateMachine.State.Patrol) 
        {
            // untilMonsterArrived �Ÿ��� 1.5f ������ ������ hold �ϴ� ����
            yield return untilMonsterArrived;
            //yield return returnPatrol;
            SetDesstination(patrolCenterPoint, range);
            Debug.LogWarning("SetPoint �۵���");
        }   
    }
    // �̵� �ڷ�ƾ
    public IEnumerator Move(GameObject monster_, MonsterStateMachine msm_) 
    {
        SetDesstination(patrolCenterPoint, range);

        // MonsterStateMachine ���°� Patrol �� ���� Coroutine ���
        while (msm_.currentState == MonsterStateMachine.State.Patrol) 
        {
            // �� ������ ������ ���� �۵� : Update ���� ���ɿ� �ǿ����� �� �� ������ Ư�� ���ǿ����� �۵��ϵ��� ��
            yield return null;
            //Debug.LogWarning(Vector3.Distance(destination, monster_.transform.position));
            monster_.transform.LookAt(destination + new Vector3(0f, monster_.transform.position.y, 0f));
            monsterControl.Move((destination - monster_.transform.position) * speed * Time.deltaTime);
            Debug.LogWarning("Move �۵���");
        }
    }
    #endregion
}

using System.Collections;
using System.Threading;
using UnityEngine;

public class Monster_Patrol : MonsterState
{
    private CharacterController monsterControl;
    private Animator monsterAni;
    private Vector3 patrolCenterPoint;
    private Vector3 destination;
    private float range;
    private float speed;
    private float gravity;

    private WaitForSecondsRealtime returnPatrol;

    public bool isRoutineOn;


    public override void OnStateEnter(GameObject monster_) 
    {
        Init(monster_);
        SetCenterPoint(monster_.transform.position);
        // ������Ÿ��
        if (monster_.GetComponent<TestMonster>() == true)
        {
            monster_.GetComponent<TestMonster>().monsterSight.SetActive(true);
        }
        else if (monster_.GetComponent<TestBigMonster>() == true) 
        {
            monster_.GetComponent<TestBigMonster>().monsterSight.SetActive(true);
            monster_.GetComponent<TestBigMonster>().monsterSonar.SetActive(true);
        }
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_) 
    {
        msm_.StartCoroutine(DoPatrol(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_) 
    {
        // ���� �ڷ�ƾ�� ������� ��츦 ����� ������ġ
        if (isRoutineOn == true) 
        {
            Debug.LogError("?");
            msm_.StopCoroutine(DoPatrol(monster_, msm_));
        }
        // ������Ÿ��
        if (monster_.GetComponent<TestMonster>() == true)
        {
            monster_.GetComponent<TestMonster>().monsterSight.SetActive(false);
        }
        else if (monster_.GetComponent<TestBigMonster>() == true)
        {
            monster_.GetComponent<TestBigMonster>().monsterSight.SetActive(false);
            monster_.GetComponent<TestBigMonster>().monsterSonar.SetActive(false);
        }
        CleanVariables();
    }





    #region �ʱ�ȭ
    private void Init(GameObject monster_) 
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();

        if (monster_.GetComponent<TestMonster>() == true)
        {
            range = monster_.GetComponent<TestMonster>().patrolRange;
            speed = monster_.GetComponent<TestMonster>().moveSpeed;
        }
        else if (monster_.GetComponent<TestBigMonster>() == true)
        {
            range = monster_.GetComponent<TestBigMonster>().patrolRange;
            speed = monster_.GetComponent<TestBigMonster>().moveSpeed;
        }

        gravity = -9.81f;

        returnPatrol = new WaitForSecondsRealtime(4f);
        isRoutineOn = false;
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
    private void SetDestination(Vector3 centerPoint_, float patrolRange_) 
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


    #region ����
    // Patrol ������ �� ������ ���� �ൿ
    public IEnumerator DoPatrol(GameObject monster_, MonsterStateMachine msm_)
    {
        // MonsterStateMachine ���°� Patrol �� ���� Coroutine ����
        while (msm_.currentState == MonsterStateMachine.State.Patrol)
        {
            // PatrolMove �Ϸ��� ������ �� ���� �ൿ�� ����Ѵ�
            yield return msm_.StartCoroutine(PatrolMove(monster_));
            // Wait �Ϸ��� ������ �� ���� �ൿ�� ����Ѵ�
            yield return msm_.StartCoroutine(Wait());
        }

        // �ڷ�ƾ �۵� �� ���� üũ��
        isRoutineOn = true;
    }
    #endregion


    #region �̵� �� ���
    // �̵� �ڷ�ƾ
    public IEnumerator PatrolMove(GameObject monster_) 
    {
        // ��ǥ ��ǥ ����
        SetDestination(patrolCenterPoint, range);

        // ��ǥ ��ǥ���� �Ÿ��� 2f ���ϰ� �� �������� while �� ����
        while (Vector3.Distance(destination, monster_.transform.position) > 2f) 
        {
            // �� ������ ������ ���� �۵� : Update ���� ���ɿ� �ǿ����� �� �� ������ Ư�� ���ǿ����� �۵��ϵ��� ��
            yield return null;

            Vector3 tempLook = new Vector3(destination.x, monster_.transform.position.y, destination.z);

            monster_.transform.LookAt(tempLook);

            Vector3 tempMove = 
                new Vector3(destination.x - monster_.transform.position.x,
                gravity, destination.z - monster_.transform.position.z).normalized;

            monsterControl.Move(tempMove * speed * Time.deltaTime);

            monsterAni.SetBool("isMoving", true);
        }
        monsterAni.SetBool("isMoving", false);
    }

    // ��� �ڷ�ƾ
    public IEnumerator Wait() 
    {
        yield return returnPatrol;
    }
    #endregion


    #region ���� ���� �޼���
    private void CleanVariables() 
    {
        monsterAni.SetBool("isMoving", false);

        monsterControl = default;
        monsterAni = default;
        patrolCenterPoint = default;
        destination = default;
        range = default;
        speed = default;
        returnPatrol = default;

        isRoutineOn = false;
    }
    #endregion
}

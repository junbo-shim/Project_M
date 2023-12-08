using System.Collections;
using UnityEngine;

public class Monster_Engage : MonsterState
{
    private CharacterController monsterControl;
    private Animator monsterAni;
    private float speed;
    private float radius;
    private float gravity;

    private bool isAttacking;

    public GameObject target;

    public WaitForSecondsRealtime waitTime;
    public int waitTimer;

    public bool isRoutineOn;



    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StartCoroutine(DoEngage(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_)
    {
        // ���� �ڷ�ƾ�� ������� ��츦 ����� ������ġ
        if (isRoutineOn == true)
        {
            msm_.StopCoroutine(DoEngage(monster_, msm_));
        }
        CleanVariables(monster_);
    }





    #region �ʱ�ȭ
    private void Init(GameObject monster_)
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        monsterAni = monster_.transform.Find("MonsterRigid").GetComponent<Animator>();
        speed = 5f;

        if (monster_.GetComponent<TestMonster>() == true)
        {
            radius = monster_.GetComponent<TestMonster>().sightRange * 2f;
        }
        else if (monster_.GetComponent<TestBigMonster>() == true)
        {
            radius = monster_.GetComponent<TestBigMonster>().sightRange * 2f;

            if (monster_.GetComponent<TestBigMonster>().sonarTarget != null) 
            {
                target = monster_.GetComponent<TestBigMonster>().sonarTarget;
            }
        }

        gravity = -9.81f;
        isAttacking = false;
        waitTime = new WaitForSecondsRealtime(1f);
        waitTimer = 5;

        isRoutineOn = false;
    }
    #endregion


    #region ����
    // Engage ������ �� ������ �ൿ
    private IEnumerator DoEngage(GameObject monster_, MonsterStateMachine msm_)
    {
        // MonsterStateMachine ���°� Engage �� ���� Coroutine ����
        while (msm_.currentState == MonsterStateMachine.State.Engage)
        {
            // ��ǥ�� Ž���ϰ� target ������ �Ҵ��Ѵ�
            CheckTarget(monster_);

            // Ÿ���� ���ٸ�
            if (target == null) 
            {
                // Ÿ�� �簨���� ���� ī��Ʈ�ٿ� �ڷ�ƾ�� �����Ѵ�
                yield return msm_.StartCoroutine(WaitForTarget(monster_, msm_));

                // �׷��� Ÿ���� ���ٸ�
                if (target == null) 
                {
                    // �ڷ�ƾ �굺�� �����ϴ� break
                    yield break;
                }
            }
            // Ÿ���� Ȯ���ߴٸ�
            else if (target != null) 
            {
                // Ÿ�� ��ġ���� �����̰� ������ ���������� �����Ѵ�
                yield return msm_.StartCoroutine(EngageMove(monster_));
                yield return msm_.StartCoroutine(Attack(monster_));
            }
        }

        // �ڷ�ƾ �۵� �� ���� üũ��
        isRoutineOn = true;
    }
    #endregion


    #region �̵�
    private IEnumerator EngageMove(GameObject monster_)
    {
        // ��ǥ ��ǥ���� �Ÿ��� 3f ���ϰ� �� �������� while �� ����
        // ������ �ִϸ��̼� ���� ������ ���� ��
        while (Vector3.Distance(target.transform.position, monster_.transform.position) > 3f)
        {
            yield return null;

            Vector3 tempLook = new Vector3(target.transform.position.x,
                monster_.transform.position.y, target.transform.position.z);

            // Ÿ���� �ٶ󺸰� �Ѵ�
            monster_.transform.LookAt(tempLook);

            Vector3 tempMove =
                new Vector3(target.transform.position.x - monster_.transform.position.x,
                gravity, target.transform.position.z - monster_.transform.position.z).normalized;

            // Ÿ���� ��ġ�� �����δ�
            monsterControl.Move(tempMove * speed * Time.deltaTime);

            monsterAni.SetBool("isMoving", true);
        }
        monsterAni.SetBool("isMoving", false);

        // �̵��� ��ġ�� Ÿ�� ������ �ʱ�ȭ�Ѵ� (�÷��̾ �����̳� �̵��������� ����ĥ �� ����)
        target = null;
    }
    #endregion


    #region ���� (�ִϸ��̼� ����ð� ����)
    private IEnumerator Attack(GameObject monster_)
    {
        CheckTarget(monster_);

        // ���� Ÿ���� ���ٸ�
        if (target == null) 
        { 
            /* Do Nothing */
        }
        // Ÿ���� �����Ѵٸ�
        else if (target != null) 
        {
            monsterAni.SetBool("isAttacking", true);

            // �ִϸ��̼��� ���� ��� �ð�
            yield return new WaitForSecondsRealtime(3f);

            // ������ ��ġ�� Ÿ�� ������ �ʱ�ȭ�Ѵ� (�÷��̾ �����̳� �̵��������� ����ĥ �� ����)
            target = null;
        }

        monsterAni.SetBool("isAttacking", false);
        //monsterAni.SetBool("isMoving", false);
    }
    #endregion


    #region ���� Ÿ�� Lost �� ��� ����
    private IEnumerator WaitForTarget(GameObject monster_, MonsterStateMachine msm_)
    {
        int i = 0;

        // Ÿ�̸� ������ �κб��� �ݺ��ϰ�, Ÿ���� �������� ���� ���� �ݺ�
        while (i < waitTimer && target == null) 
        {
            yield return waitTime;
            // n �ʸ��� Ÿ���� ã�´�
            CheckTarget(monster_);
            i++;
        }

        // ������ �� ���Ƶ� Ÿ���� ���ٸ�
        if (target == null) 
        {
            // ���¸� ������ ��ȯ�Ѵ�
            msm_.ChangeState(MonsterStateMachine.State.Patrol);
        }
    }
    #endregion


    #region Ÿ�� Ȯ�� �޼���
    private void CheckTarget(GameObject monster_)
    {
        // Detect -> Engage �� ��� target �ʱ�ȭ ȸ�ǿ� ������ġ
        if (target != null) 
        {
            return;
        }

        // OverlapSphere �� ����Ͽ� Player Layer �� ����õ��Ѵ�
        Collider[] colliders = Physics.OverlapSphere(monster_.transform.position, radius, LayerMask.GetMask("Player"));

        //Debug.LogError(colliders.Length);

        // ���� ����� ���� ���ٸ� 
        if (colliders.Length <= 0) 
        {
            // Ÿ�� ������ null �̴�
            target = null;
            //Debug.LogWarning("Ÿ�� ����");
        }
        // ���� ����� ���� �ִٸ�
        else if (colliders.Length > 0)
        {
            // foreach �ݺ��� ���� colliders �� ��� �˻��Ѵ�
            foreach (var collider in colliders)
            {
                // ������Ÿ��
                if (collider.GetComponent<Rigidbody>() == true)
                {
                    target = collider.gameObject;
                    //Debug.LogWarning("Ÿ�� ã��");
                }
            }
        }
    }
    #endregion


    #region ���� ���� �޼���
    private void CleanVariables(GameObject monster_) 
    {
        monsterAni.SetBool("isMoving", false); 
        monsterAni.SetBool("isAttacking", false);

        monsterControl = default;
        monsterAni = default;
        speed = default;
        radius = default;
        isAttacking = false;
        target = null;
        waitTime = default;
        waitTimer = default;

        isRoutineOn = false;

        monster_.GetComponent<TestBigMonster>().sonarTarget = null;
    }
    #endregion
}

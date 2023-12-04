using System.Collections;
using UnityEngine;

public class Monster_Engage : MonsterState
{
    private CharacterController monsterControl;
    private float speed;
    private float radius;

    private bool isAttacking;

    public GameObject target;


    public override void OnStateEnter(GameObject monster_)
    {
        Init(monster_);
    }

    public override void OnStateStay(GameObject monster_, MonsterStateMachine msm_)
    {
        msm_.StartCoroutine(DoEngage(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_)
    {
        CleanVariables();
    }





    #region �ʱ�ȭ
    private void Init(GameObject monster_)
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        speed = 1f;
        radius = 10f;
        isAttacking = false;
    }
    #endregion


    #region ����
    // Engage ������ �� ������ �ൿ
    private IEnumerator DoEngage(GameObject monster_, MonsterStateMachine msm_)
    {
        // MonsterStateMachine ���°� Engage �� ���� Coroutine ����
        while (msm_.currentState == MonsterStateMachine.State.Engage)
        {
            yield return msm_.StartCoroutine(EngageMove(monster_));
            yield return msm_.StartCoroutine(CheckAttack(monster_, msm_));
        }
    }
    #endregion


    #region �̵�
    private IEnumerator EngageMove(GameObject monster_)
    {
        // ��ǥ�� Ž���ϰ� target ������ �Ҵ��Ѵ�
        CheckTarget(monster_);

        // ��ǥ ��ǥ���� �Ÿ��� 3f ���ϰ� �� �������� while �� ����
        // ������ �ִϸ��̼� ���� ������ ���� ��
        while (Vector3.Distance(target.transform.position, monster_.transform.position) > 3f)
        {
            yield return null;

            Vector3 temp = new Vector3(target.transform.position.x,
                monster_.transform.position.y, target.transform.position.z);

            monster_.transform.LookAt(temp);
            monsterControl.Move((target.transform.position - monster_.transform.position) * speed * Time.deltaTime);
        }
    }
    #endregion


    #region ����üũ
    private IEnumerator CheckAttack(GameObject monster_, MonsterStateMachine msm_)
    {
        CheckTarget(monster_);

        if (target == null)
        {
            Debug.LogWarning("null");
            //yield return msm_.StartCoroutine(WaitForTarget(monster_, msm_));
        }
        else if (target != null)
        {
            Debug.LogWarning("Attack ����");
            yield return msm_.StartCoroutine(Attack());
        }
    }
    #endregion


    #region ������ (�ִϸ��̼� ����ð� ����)
    private IEnumerator Attack()
    {
        Debug.Log("true");
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("false");
        target = default;
    }
    #endregion


    #region ���� Ÿ�� Lost �� ��� ����
    private IEnumerator WaitForTarget(GameObject monster_, MonsterStateMachine msm_)
    {
        int i = 0;

        while (i < 5)
        {
            yield return new WaitForSecondsRealtime(1f);
            CheckTarget(monster_);

            if (target != null)
            {
                msm_.StopAllCoroutines();
                msm_.StartCoroutine(DoEngage(monster_, msm_));
            }

            Debug.Log("Now :" + i);
            i++;
            Debug.Log("Next :" + i);
        }

        if (target == null)
        {
            Debug.Log("��ǥ ���, ������ ����");
            msm_.StopAllCoroutines();
            // ������Ÿ��
            msm_.ChangeState(MonsterStateMachine.State.Patrol);
        }
    }
    #endregion


    #region Ÿ�� Ȯ��
    private void CheckTarget(GameObject monster_)
    {
        Collider[] colliders = Physics.OverlapSphere(monster_.transform.position, radius, LayerMask.GetMask("Player"));

        Debug.LogError(colliders.Length);

        foreach (var collider in colliders)
        {
            Debug.LogError(collider.name);

            // ������Ÿ��
            if (collider.GetComponent<Rigidbody>() == true)
            {
                Debug.LogWarning("�÷��̾� Ȯ��" + collider.name);
                target = collider.gameObject;
            }
            else 
            {
                Debug.LogWarning("target lost");
                target = null;
            }
        }
    }
    #endregion


    private void CleanVariables() 
    {
        monsterControl = default;
        speed = default;
        radius = default;
        isAttacking = false;
        target = null;
    }
}

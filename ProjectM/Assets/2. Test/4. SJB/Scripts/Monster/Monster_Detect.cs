using System.Collections;
using TMPro;
using UnityEngine;

public class Monster_Detect : MonsterState
{
    private CharacterController monsterControl;
    private float radius;

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
        msm_.StartCoroutine(DoDetect(monster_, msm_));
    }

    public override void OnStateExit(GameObject monster_, MonsterStateMachine msm_)
    {
        // ���� �ڷ�ƾ�� ������� ��츦 ����� ������ġ
        if (isRoutineOn == true)
        {
            msm_.StopCoroutine(DoDetect(monster_, msm_));
            Debug.LogError("Wrong");
        }
        CleanVariables(monster_);
    }





    #region �ʱ�ȭ
    private void Init(GameObject monster_)
    {
        monsterControl = monster_.GetComponent<CharacterController>();
        radius = monster_.GetComponent<TestBigMonster>().sonarRange * 2f;
        waitTime = new WaitForSecondsRealtime(1f);
        waitTimer = 5;

        isRoutineOn = false;

        monster_.GetComponent<TestBigMonster>().detectUI.SetActive(true);
    }
    #endregion


    #region ����
    // ���� ������ �� ������ �ൿ
    private IEnumerator DoDetect(GameObject monster_, MonsterStateMachine msm_) 
    {
        int i = 0;

        // Ÿ�̸� ������ �κб��� �ݺ�
        while (i < waitTimer)
        {
            yield return waitTime;
            i++;
        }

        // Ÿ�� ���� ���θ� üũ�ϱ� ���� CheckTarget
        CheckTarget(monster_);

        if (target != null) 
        {
            monster_.GetComponent<TestBigMonster>().detectUI.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = "!";
        }
        yield return new WaitForSecondsRealtime(2f);

        // Ÿ���� ���ٸ�
        if (target == null)
        {
            // ���¸� ������ ��ȯ�Ѵ�
            msm_.ChangeState(MonsterStateMachine.State.Patrol);
        }
        else if (target != null) 
        {
            // ���¸� �������� ��ȯ�Ѵ�
            msm_.ChangeState(MonsterStateMachine.State.Engage);
        }

        isRoutineOn = true;
    }
    #endregion


    #region Ÿ�� Ȯ�� �޼���
    private void CheckTarget(GameObject monster_)
    {
        // OverlapSphere �� ����Ͽ� Player Layer �� ����õ��Ѵ�
        Collider[] colliders = Physics.OverlapSphere(monster_.transform.position, radius, LayerMask.GetMask("PlayerFoot"));

        //Debug.LogError(colliders.Length);

        // ���� ����� ���� ���ٸ� 
        if (colliders.Length <= 0)
        {
            // Ÿ�� ������ null �̴�
            target = null;
            monster_.GetComponent<TestBigMonster>().sonarTarget = null;
            Debug.LogWarning("Ÿ�� ����");
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
                    monster_.GetComponent<TestBigMonster>().sonarTarget = collider.transform.parent.gameObject;
                    target = monster_.GetComponent<TestBigMonster>().sonarTarget;
                    Debug.LogWarning("Ÿ�� ã��");
                }
            }
        }
    }
    #endregion


    #region ���� ���� �޼���
    private void CleanVariables(GameObject monster_)
    {
        monsterControl = default;
        radius = default;
        target = null;
        waitTime = default;
        waitTimer = default;

        isRoutineOn = false;

        //monster_.GetComponent<TestBigMonster>().sonarTarget = null;
        monster_.GetComponent<TestBigMonster>().detectUI.transform.Find("Text (TMP)").GetComponent<TMP_Text>().text = "?";
        monster_.GetComponent<TestBigMonster>().detectUI.SetActive(false);
    }
    #endregion
}

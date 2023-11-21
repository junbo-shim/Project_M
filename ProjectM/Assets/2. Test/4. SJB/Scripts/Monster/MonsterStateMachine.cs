using UnityEngine;
using System.Collections.Generic;

public class MonsterStateMachine : MonoBehaviour
{
    // ���� enum
    public enum State
    {
        // �⺻-����
        Idle,
        // ����
        Detect,
        // ��������
        Engage,
        // ���� (���� ü�� ���� �� ����)
        Runaway,
        // ����
        Die
    }

    public State currentState;

    private Monster_Idle idleState;
    private Monster_Detect detectState;
    private Monster_Engage engageState;
    private Monster_Runaway runawayState;
    private Monster_Die dieState;

    public Dictionary<State, MonsterState> enumToStateClass;


    // �����ڿ��� �ʱ�ȭ�� �����Ѵ�
    public MonsterStateMachine()
    {
        currentState = State.Idle;

        idleState = new Monster_Idle();
        detectState = new Monster_Detect();
        engageState = new Monster_Engage();
        runawayState = new Monster_Runaway();
        dieState = new Monster_Die();

        enumToStateClass = new Dictionary<State, MonsterState>();

        // ��ųʸ��� ����Ͽ� enum �� �� MonsterState �� �������ش�
        enumToStateClass.Add(State.Idle, idleState);
        enumToStateClass.Add(State.Detect, detectState);
        enumToStateClass.Add(State.Engage, engageState);
        enumToStateClass.Add(State.Runaway, runawayState);
        enumToStateClass.Add(State.Die, dieState);
    }


    public void ChangeState(State nextState_)
    {
        if (currentState == null)
        {
            Debug.LogError("currentState_ �� null �Դϴ�");
            return;
        }
        
        if (nextState_ != null)
        {
            // ���� ���¸� �����Ѵ�
            enumToStateClass[currentState].OnStateExit();

            // ��ȯ�� ���¸� ���� ���¿� �����Ѵ�
            currentState = nextState_;

            // ��ȯ�� ���¸� �����Ѵ�
            enumToStateClass[currentState].OnStateEnter();
        }
    }
}

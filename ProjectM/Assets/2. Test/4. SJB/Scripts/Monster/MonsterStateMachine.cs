using UnityEngine;
using System.Collections.Generic;

public class MonsterStateMachine : MonoBehaviour
{
    // ���� enum
    public enum State
    {
        // �ʱ����
        Spawn,
        // ����
        Patrol,
        // ����
        Detect,
        // ��������
        Engage,
        // ���� (���� ü�� ���� �� ����)
        Runaway,
        // ����
        Die
    }

    public GameObject monster;

    public State currentState;

    private Monster_Spawn spawnState;
    private Monster_Patrol patrolState;
    private Monster_Detect detectState;
    private Monster_Engage engageState;
    private Monster_Runaway runawayState;
    private Monster_Die dieState;

    public Dictionary<State, MonsterState> enumToStateClass;


    // �����ڿ��� �ʱ�ȭ�� �����Ѵ�
    public MonsterStateMachine()
    {
        currentState = State.Spawn;

        spawnState = new Monster_Spawn();
        patrolState = new Monster_Patrol();
        detectState = new Monster_Detect();
        engageState = new Monster_Engage();
        runawayState = new Monster_Runaway();
        dieState = new Monster_Die();

        enumToStateClass = new Dictionary<State, MonsterState>();

        // ��ųʸ��� ����Ͽ� enum �� �� MonsterState �� �������ش�
        enumToStateClass.Add(State.Spawn, spawnState);
        enumToStateClass.Add(State.Patrol, patrolState);
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
            switch (nextState_) 
            {
                case State.Patrol:
                    // ���� ���¸� �����Ѵ�
                    enumToStateClass[currentState].OnStateExit(monster);
                    // ��ȯ�� ���¸� ���� ���¿� �����Ѵ�
                    currentState = nextState_;
                    // ��ȯ�� ���¸� �����Ѵ�
                    enumToStateClass[currentState].OnStateEnter(monster);
                    enumToStateClass[currentState].OnStateStay(monster, this);
                    break;
                case State.Detect:
                    enumToStateClass[currentState].OnStateExit(monster);
                    currentState = nextState_;
                    enumToStateClass[currentState].OnStateEnter(monster);
                    enumToStateClass[currentState].OnStateStay(monster, this);
                    break;
                case State.Engage:
                    enumToStateClass[currentState].OnStateExit(monster);
                    currentState = nextState_;
                    enumToStateClass[currentState].OnStateEnter(monster);
                    enumToStateClass[currentState].OnStateStay(monster, this);
                    break;
                case State.Runaway:
                    enumToStateClass[currentState].OnStateExit();
                    currentState = nextState_;
                    enumToStateClass[currentState].OnStateEnter();
                    break;
                case State.Die:
                    enumToStateClass[currentState].OnStateExit();
                    currentState = nextState_;
                    enumToStateClass[currentState].OnStateEnter();
                    break;
            }
        }
    }
}

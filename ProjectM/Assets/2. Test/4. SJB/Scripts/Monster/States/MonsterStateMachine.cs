using UnityEngine;
using System.Collections.Generic;

public class MonsterStateMachine : MonoBehaviour
{
    // 상태 enum
    public enum State
    {
        // 오브젝트 풀 보관상태
        Ready,
        // 정찰
        Patrol,
        // 감지
        Detect,
        // 전투돌입
        Engage,
        // 도망 (일정 체력 이하 시 도망)
        Runaway,
        // 죽음
        Die
    }

    public GameObject monster;

    public State currentState;

    private Monster_Ready readyState;
    private Monster_Patrol patrolState;
    private Monster_Detect detectState;
    private Monster_Engage engageState;
    private Monster_Runaway runawayState;
    private Monster_Die dieState;

    public Dictionary<State, MonsterState> enumToStateClass;


    // 생성자에서 초기화를 진행한다
    public MonsterStateMachine()
    {
        readyState = new Monster_Ready();
        patrolState = new Monster_Patrol();
        detectState = new Monster_Detect();
        engageState = new Monster_Engage();
        runawayState = new Monster_Runaway();
        dieState = new Monster_Die();

        enumToStateClass = new Dictionary<State, MonsterState>();

        // 딕셔너리를 사용하여 enum 을 각 MonsterState 와 연결해준다
        enumToStateClass.Add(State.Ready, readyState);
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
            Debug.LogError("currentState_ 가 null 입니다");
            return;
        }

        #region LEGACY
        //if (nextState_ != null)
        //{
        //    switch (nextState_) 
        //    {
        //        case State.Patrol:
        //            // 현재 상태를 종료한다
        //            enumToStateClass[currentState].OnStateExit(monster, this);
        //            // 전환할 상태를 현재 상태에 저장한다
        //            currentState = nextState_;
        //            // 전환된 상태를 실행한다
        //            enumToStateClass[currentState].OnStateEnter(monster);
        //            enumToStateClass[currentState].OnStateStay(monster, this);
        //            break;
        //        case State.Detect:
        //            enumToStateClass[currentState].OnStateExit(monster, this);
        //            currentState = nextState_;
        //            enumToStateClass[currentState].OnStateEnter(monster);
        //            enumToStateClass[currentState].OnStateStay(monster, this);
        //            break;
        //        case State.Engage:
        //            enumToStateClass[currentState].OnStateExit(monster, this);
        //            currentState = nextState_;
        //            enumToStateClass[currentState].OnStateEnter(monster);
        //            enumToStateClass[currentState].OnStateStay(monster, this);
        //            break;
        //        case State.Runaway:
        //            enumToStateClass[currentState].OnStateExit();
        //            currentState = nextState_;
        //            enumToStateClass[currentState].OnStateEnter();
        //            break;
        //        case State.Die:
        //            enumToStateClass[currentState].OnStateExit();
        //            currentState = nextState_;
        //            enumToStateClass[currentState].OnStateEnter();
        //            break;
        //    }
        //}
        #endregion

        if (nextState_ != null)
        {
            switch (nextState_)
            {
                case State.Ready:
                    enumToStateClass[currentState].OnStateExit();
                    currentState = nextState_;
                    enumToStateClass[currentState].OnStateEnter(monster);
                    break;

                case State.Patrol:
                case State.Detect:
                case State.Engage:
                case State.Die:
                    // 현재 상태를 종료한다
                    enumToStateClass[currentState].OnStateExit(monster, this);
                    // 전환할 상태를 현재 상태에 저장한다
                    currentState = nextState_;
                    // 전환된 상태를 실행한다
                    enumToStateClass[currentState].OnStateEnter(monster);
                    enumToStateClass[currentState].OnStateStay(monster, this);
                    break;

                case State.Runaway:
                    enumToStateClass[currentState].OnStateExit();
                    currentState = nextState_;
                    enumToStateClass[currentState].OnStateEnter();
                    break;
            }
        }       // if: 다음 State가 존재하는 경우
    }
}

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
        // 복귀
        Return,
        // 죽음
        Die
    }

    public GameObject monster;

    public State currentState;

    private Monster_Ready readyState;
    private Monster_Patrol patrolState;
    private Monster_Detect detectState;
    private Monster_Engage engageState;
    private Monster_Return returnState;
    private Monster_Die dieState;

    public Dictionary<State, MonsterState> enumToStateClass;


    // 생성자에서 초기화를 진행한다
    public MonsterStateMachine()
    {
        readyState = new Monster_Ready();
        patrolState = new Monster_Patrol();
        detectState = new Monster_Detect();
        engageState = new Monster_Engage();
        returnState = new Monster_Return();
        dieState = new Monster_Die();

        enumToStateClass = new Dictionary<State, MonsterState>
        {
            // 딕셔너리를 사용하여 enum 을 각 MonsterState 와 연결해준다
            { State.Ready, readyState },
            { State.Patrol, patrolState },
            { State.Detect, detectState },
            { State.Engage, engageState },
            { State.Return, returnState },
            { State.Die, dieState }
        };
    }


    public void ChangeState(State nextState_)
    {
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

        switch (nextState_)
        {
            case State.Ready:
                enumToStateClass[currentState].OnStateExit(monster, this);
                currentState = nextState_;
                enumToStateClass[currentState].OnStateEnter(monster);
                break;

            case State.Patrol:
            case State.Detect:
            case State.Engage:
            case State.Return:
            case State.Die:
                // 현재 상태를 종료한다
                enumToStateClass[currentState].OnStateExit(monster, this);
                // 전환할 상태를 현재 상태에 저장한다
                currentState = nextState_;
                // 전환된 상태를 실행한다
                enumToStateClass[currentState].OnStateEnter(monster);
                enumToStateClass[currentState].OnStateStay(monster, this);
                break;
        }
    }       // if: 다음 State가 존재하는 경우
}

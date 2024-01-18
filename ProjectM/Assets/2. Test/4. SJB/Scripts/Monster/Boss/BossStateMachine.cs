using UnityEngine;
using System.Collections.Generic;

public class BossStateMachine : MonoBehaviour
{
    // 상태 또는 패턴 정의
    public enum BossState 
    {
        // 보스 대기상태
        BossReady,
        // 예티왕 공격상태
        Yeti_BossAttack,
        // 메카킹 공격상태,

        // 보스 죽음
        BossDie
    }

    public GameObject bossMonster;

    public BossState currentState;

    private Boss_Ready bossReady;
    private Yeti_BossAttack yetiATT;
    private Boss_Die bossDie;

    public Dictionary<BossState, MonsterState> enumToBossState;


    // 생성자에서 초기화를 진행한다
    public BossStateMachine() 
    {
        bossReady = new Boss_Ready();
        yetiATT = new Yeti_BossAttack();
        bossDie = new Boss_Die();

        enumToBossState = new Dictionary<BossState, MonsterState>
        {
            // 딕셔너리를 사용하여 enum 을 각 MonsterState 와 연결해준다
            { BossState.BossReady, bossReady},
            { BossState.Yeti_BossAttack, yetiATT },
            { BossState.BossDie, bossDie}
        };
    }

    public void ChangeState(BossState nextPattern_) 
    {
        switch (nextPattern_) 
        {
            case BossState.BossReady:

                enumToBossState[currentState].OnStateExit(this);
                currentState = nextPattern_;
                enumToBossState[currentState].OnStateEnter(bossMonster);
                break;

            case BossState.Yeti_BossAttack:

                enumToBossState[currentState].OnStateExit(this);
                currentState = nextPattern_;
                enumToBossState[currentState].OnStateEnter(bossMonster);
                enumToBossState[currentState].OnStateStay(this);
                break;

            case BossState.BossDie:

                enumToBossState[currentState].OnStateExit(this);
                currentState = nextPattern_;
                enumToBossState[currentState].OnStateEnter(bossMonster);
                enumToBossState[currentState].OnStateStay(bossMonster, this);
                break;
        }
    }
}

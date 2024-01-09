using UnityEngine;
using System.Collections.Generic;

public class BossStateMachine : MonoBehaviour
{
    // 상태 또는 패턴 정의
    public enum Pattern 
    {
        // 예티왕 돌진패턴
        Yeti_Charge,
        // 예티왕 눈덩이패턴
        Yeti_Snowball,
        // 예티왕 근접공격패턴
        Yeti_Punch,

        Die
    }

    public GameObject bossMonster;

    public Pattern currentPattern;

    private Yeti_Charge yetiCharge;
    private Yeti_Snowball yetiSnowball;
    private Yeti_Punch yetiPunch;

    public Dictionary<Pattern, MonsterState> enumToPattern;


    // 생성자에서 초기화를 진행한다
    public BossStateMachine() 
    {
        yetiCharge = new Yeti_Charge();
        yetiSnowball = new Yeti_Snowball();
        yetiPunch = new Yeti_Punch();

        enumToPattern = new Dictionary<Pattern, MonsterState>
        {
            // 딕셔너리를 사용하여 enum 을 각 MonsterState 와 연결해준다
            { Pattern.Yeti_Charge, yetiCharge },
            { Pattern.Yeti_Snowball, yetiSnowball },
            { Pattern.Yeti_Punch, yetiPunch },
        };
    }

    public void ChangePattern(Pattern nextPattern_) 
    {
        switch (nextPattern_) 
        {
            case Pattern.Yeti_Charge:
            case Pattern.Yeti_Snowball:
            case Pattern.Yeti_Punch:

                enumToPattern[currentPattern].OnStateExit();
                currentPattern = nextPattern_;
                enumToPattern[currentPattern].OnStateEnter();
                enumToPattern[currentPattern].OnStateStay();
                break;
        }
    }
}

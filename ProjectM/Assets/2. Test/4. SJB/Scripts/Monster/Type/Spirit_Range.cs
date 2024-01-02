using System.Collections;
using UnityEngine;

public class Spirit_Range : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Spirit_Range;
        InitMonster(thisMonsterType);
        debuffState = DebuffState.Nothing;
        monsterPatrolRange = 6f;

        monsterSightRange = 12f;
    }

    private void OnEnable()
    {
        monsterFSM.ChangeState(MonsterStateMachine.State.Spawn);
    }
}

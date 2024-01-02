using System.Collections;
using UnityEngine;

public class Mech_Large : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Mech_Large;
        InitMonster(thisMonsterType);
        debuffState = DebuffState.Nothing;
        monsterPatrolRange = 14f;

        monsterSightRange = 9f;
        monsterSonarRange = 11f;
    }

    private void Start()
    {
        monsterFSM.ChangeState(MonsterStateMachine.State.Spawn);
        StartCoroutine(SpawnAndStartPatrol());
    }

    public IEnumerator SpawnAndStartPatrol()
    {
        yield return new WaitForSecondsRealtime(1f);
        monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
    }
}

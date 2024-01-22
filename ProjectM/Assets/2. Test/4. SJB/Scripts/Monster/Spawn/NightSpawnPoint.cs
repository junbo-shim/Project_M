using UnityEngine;

public class NightSpawnPoint : MonoBehaviour
{
    private ObjectPool nightPool;
    private Monster nightMonsterComponent;

    private void Start()
    {
        nightPool = GameObject.Find("Pool_Mech_Large").GetComponent<ObjectPool>();

        MapGameManager.instance.nightStart += TurnMonsterOn;
    }

    private void TurnMonsterOn() 
    {
        GameObject monster = nightPool.ActiveObjFromPool(transform.position);
        nightMonsterComponent = monster.GetComponent<Monster>();
        nightMonsterComponent.monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
    }
}

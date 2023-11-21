using UnityEngine;
using UnityEngine.Events;

public class TestMonster : MonoBehaviour
{
    public MonsterStateMachine monsterFSM;
    public int hp;


    private void Awake()
    {
        hp = 500;
    }

    void Update()
    {
        TestStateChange();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAtk")) 
        {
            TestTrigger();
        }
    }


    private void TestStateChange() 
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Idle);
        }
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Detect);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Runaway);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Die);
        }

        monsterFSM.enumToStateClass[monsterFSM.currentState].OnStateStay();
    }

    private void TestTrigger() 
    {
        hp -= 100;

        if (hp <= 100) 
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Runaway);
        }
        else 
        {
            Debug.LogWarning("HP : " + hp);
        }
    }
}

using UnityEngine;

public class TestMonster : MonoBehaviour
{
    public MonsterStateMachine monsterFSM;
    public CharacterController monsterControl;

    public int hp;
    public float patrolRange;
    public float moveSpeed;


    private void Awake()
    {
        hp = 500;
        patrolRange = 10f;
        moveSpeed = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PatrolBecon"))
        {
            //monsterFSM.enumToStateClass[monsterFSM.currentState].OnStateStay(gameObject);
        }
    }

    private void Update()
    {
        TestStateChange();
    }



    private void TestStateChange()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
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
    }
}

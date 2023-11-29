using UnityEngine;

public class TestMonster : MonoBehaviour
{
    public MonsterStateMachine monsterFSM;
    public CharacterController monsterControl;
    public GameObject monsterSight;
    public GameObject player;

    public int hp;
    public float patrolRange;
    public float moveSpeed;
    public float sightRange;
    public float sightAngle;


    private void Awake()
    {
        hp = 500;
        patrolRange = 10f;
        moveSpeed = 1f;
        sightRange = transform.localScale.y * 4f;
        monsterSight.GetComponent<SphereCollider>().radius = sightRange;
        sightAngle = 90f;
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
        if (monsterSight.GetComponent<MonsterSight>().isInAngle == true)
        {
            monsterSight.SetActive(false);
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

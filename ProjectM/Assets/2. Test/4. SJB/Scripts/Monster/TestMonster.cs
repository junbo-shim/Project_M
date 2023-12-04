using System.Collections;
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
        //monsterSight.GetComponent<SphereCollider>().radius = sightRange;

        // 프로토타입용
        monsterSight.transform.localScale = Vector3.one * sightRange * 2f;
        sightAngle = 90f;
    }

    private void Start()
    {
        monsterFSM.ChangeState(MonsterStateMachine.State.Spawn);
        StartCoroutine(SpawnAndPatrol());
    }

    private IEnumerator SpawnAndPatrol() 
    {
        yield return new WaitForSecondsRealtime(1f);
        monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
    }






    private void Update()
    {
        //TestStateChange();
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

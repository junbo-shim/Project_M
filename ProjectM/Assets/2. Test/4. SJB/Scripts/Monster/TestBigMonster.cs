using System.Collections;
using UnityEngine;

public class TestBigMonster : MonoBehaviour
{
    public MonsterStateMachine monsterFSM;
    public CharacterController monsterControl;
    public Animator monsterAnimator;
    public GameObject monsterSight;
    public GameObject monsterSonar;
    public GameObject detectUI;

    public int hp;
    public float patrolRange;
    public float moveSpeed;
    public float sightRange;
    public float sightAngle;

    public float sonarRange;
    public GameObject sonarTarget;


    private void Awake()
    {
        hp = 300000;
        patrolRange = 10f;
        moveSpeed = 7f;
        sightRange = transform.localScale.y * 2f;

        // 프로토타입용
        monsterSight.transform.localScale = Vector3.one * sightRange * 2f;
        sightAngle = 90f;

        // ALERT : 소나 범위 문제존재
        sonarRange = transform.localScale.y * 2.4f;
        monsterSonar.transform.localScale = Vector3.one * sonarRange * 2f;
    }

    private void Start()
    {
        monsterFSM.ChangeState(MonsterStateMachine.State.Spawn);
        StartCoroutine(SpawnAndStartPatrol());
    }

    private IEnumerator SpawnAndStartPatrol()
    {
        yield return new WaitForSecondsRealtime(1f);
        monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
    }

    public void ChangeSonarTarget(GameObject obj_) 
    {
        sonarTarget = obj_;
    }
}

using System.Collections;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    public MonsterStateMachine monsterFSM;
    public CharacterController monsterControl;
    public Animator monsterAnimator;
    public GameObject monsterSight;

    public int hp;
    public float patrolRange;
    public float moveSpeed;
    public float sightRange;
    public float sightAngle;


    private void Awake()
    {
        hp = 500;
        patrolRange = 10f;
        moveSpeed = 4f;
        sightRange = transform.localScale.y * 4f;

        // ������Ÿ�Կ�
        monsterSight.transform.localScale = Vector3.one * sightRange * 2f;
        sightAngle = 90f;
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
}

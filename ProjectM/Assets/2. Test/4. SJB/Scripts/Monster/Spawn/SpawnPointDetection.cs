using UnityEngine;

public class SpawnPointDetection : MonoBehaviour
{
    public ObjectPool monsterPool;
    public GameObject spawnPoint;

    private Vector3[] pos;

    public int monsterSpawnCount;
    [SerializeField]
    public int monsterMaxCount;

    private void Awake()
    {
        pos = new Vector3[] 
        { 
            new Vector3(0, 0, 12), 
            new Vector3(12, 0, 2), 
            new Vector3(-12, 0, 2), 
            new Vector3(9, 0, -12),
            new Vector3(-9, 0, -12)
        };
    }

    private void OnEnable()
    {
        monsterSpawnCount = 0;
    }

    private void OnTriggerEnter(Collider other_)
    {
        // 플레이어와 닿을 경우
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            // 스폰된 몬스터 수가 0 이고, 밤일 경우만 작동
            if (monsterSpawnCount == 0 && !MapGameManager.instance.currentState.Equals(DayState.NIGHT))
            {
                // 반복문 돌아서 몬스터 생성
                for (monsterSpawnCount = 0; monsterSpawnCount <= monsterMaxCount - 1; monsterSpawnCount++)
                {
                    GameObject monster 
                        = monsterPool.ActiveObjFromPool(spawnPoint.transform.position + pos[monsterSpawnCount]);

                    monster.GetComponent<Monster>().monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
                }
            }
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class SpawnPointDetection : MonoBehaviour
{
    public ObjectPool monsterPool;
    public GameObject spawnPoint;

    public int monsterSpawnCount;
    [SerializeField]
    public int monsterMaxCount;

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
                for (monsterSpawnCount = 0; monsterSpawnCount <= monsterMaxCount; monsterSpawnCount++)
                {
                    GameObject monster 
                        = monsterPool.ActiveObjFromPool(spawnPoint.transform.position + (Vector3.up * 3));

                    monster.GetComponent<Monster>().monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
                }
            }
            gameObject.SetActive(false);
        }
    }
}

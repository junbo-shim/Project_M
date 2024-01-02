using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Monster : MonoBehaviour
{
    // 몬스터 프리팹
    public GameObject monsterPrefab;
    // 몬스터 풀(스택)
    private Stack<GameObject> thisObjectPool;
    // 인스펙터에서 확인할 테스트용 리스트
    public List<GameObject> objPoolTestList;
    // 풀의 크기
    public int monsterCount;


    private void Awake()
    {
        InitPool();
    }

    // 풀 초기화 및 세팅
    private void InitPool()
    {
        thisObjectPool = new Stack<GameObject>();
        objPoolTestList = new List<GameObject>();
        monsterCount = 3;

        // 반복문을 통해 세팅한 크기만큼 풀에 몬스터를 넣어둠
        for (int i = 0; i < monsterCount; i++)
        {
            // 풀 오브젝트 하위에 자식으로 넣기
            // TODO : 생성 위치 설정
            GameObject monster = Instantiate(monsterPrefab, Vector3.zero, Quaternion.identity, transform);
            // 풀(스택)에 넣기
            thisObjectPool.Push(monster);
            objPoolTestList.Add(monster);
            // 몬스터 오브젝트 꺼두기
            monster.SetActive(false);
        }
    }

    public GameObject ActiveMonsterFromPool() 
    {
        // 만약 풀(스택)의 크기가 0 이라면, (가용할 수 있는 오브젝트가 없다면 == 풀에 있는 몬스터가 모두 On 상태라면)
        if (thisObjectPool.Count == 0) 
        {
            // 새로 생성하여 풀에 넣기
            // TODO : 생성 위치 설정
            GameObject monster = Instantiate(monsterPrefab, Vector3.zero, Quaternion.identity, transform);
            thisObjectPool.Push(monster);
            objPoolTestList.Add(monster);

            return monster;
        }
        // 풀(스택)에 몬스터가 남아있다면
        else 
        {
            // 남아있는 몬스터를 꺼내준다
            // TODO : 생성 위치 설정
            GameObject monster = thisObjectPool.Pop();
            //monster.transform.position = 
            objPoolTestList.Remove(monster);
            monster.SetActive(true);

            return monster;
        }
    }

    public void ReturnMonsterToPool(GameObject monster_) 
    {
        thisObjectPool.Push(monster_);
        objPoolTestList.Add(monster_);
        monster_.SetActive(false);
    }
}

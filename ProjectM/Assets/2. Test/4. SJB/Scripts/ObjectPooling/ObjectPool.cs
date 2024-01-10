using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 오브젝트 프리팹
    public GameObject prefab;
    // 오브젝트 풀(스택)
    private Stack<GameObject> thisObjectPool;
    // 풀의 크기
    [SerializeField]
    public int poolCount;


    private void Awake()
    {
        InitPool();
    }

    // 풀 초기화 및 세팅
    private void InitPool()
    {
        thisObjectPool = new Stack<GameObject>();

        // 반복문을 통해 세팅한 크기만큼 풀에 오브젝트를 넣어둠
        for (int i = 0; i < poolCount; i++)
        {
            // 풀 오브젝트 하위에 자식으로 넣기
            // TODO : 생성 위치 설정
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            // 풀(스택)에 넣기
            thisObjectPool.Push(obj);
            // 오브젝트 꺼두기
            obj.SetActive(false);
        }
    }

    // 풀에서 오브젝트 꺼내기
    public GameObject ActiveObjFromPool(Vector3 startPoint_) 
    {
        // 만약 풀(스택)의 크기가 0 이라면, (가용할 수 있는 오브젝트가 없다면 == 풀에 있는 오브젝트가 모두 On 상태라면)
        if (thisObjectPool.Count == 0) 
        {
            // 새로 생성하기
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            // 포지션 조정
            obj.transform.position = startPoint_;
            // 풀에서 제거
            thisObjectPool.Push(obj);
            obj.SetActive(true);

            return obj;
        }
        // 풀(스택)에 오브젝트가 남아있다면
        else 
        {
            // 남아있는 오브젝트를 꺼내준다
            GameObject obj = thisObjectPool.Pop();
            // 포지션 조정
            obj.transform.position = startPoint_;
            obj.SetActive(true);

            return obj;
        }
    }

    // 오브젝트 반납
    public void ReturnObjToPool(GameObject obj_) 
    {
        // 오브젝트 반납
        obj_.transform.SetParent(transform);
        thisObjectPool.Push(obj_);
        obj_.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObj : MonoBehaviour
{
    public float spTime; // 재스폰 타임
    public bool ObjType; // 스크롤은 true로 둘것 이거 여부로 재스폰여부 정함

    public void OnDisable()
    {
        if (!ObjType)
        {
            Invoke("SpObj", spTime);
        }
    }

    void SpObj() // 오브젝트 리스폰
    {
        MapGameManager.instance.OnObj(transform.gameObject);
    }

    public void offObj() 
    {
     
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator trapAni;
    private ObjectPool trapPool;
    public SphereCollider collider;
    public Monster hitMonster;

    private void Awake()
    {
        //trapPool = GameObject.Find("Pool_Trap").GetComponent<ObjectPool>();
    }

    private void Update()
    {
        CheckTrapTime();
    }

    private void OnDisable()
    {
        collider.enabled = true;
        hitMonster = default;
    }

    private void OnTriggerEnter(Collider other_)
    {
        // 몬스터와 맞닿으면
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Monster")))
        {
            // 애니메이션 트리거를 isActivated 로 변경하고
            trapAni.SetTrigger("isActivated");
            // 몬스터 변수에 닿은 몬스터를 담는다
            hitMonster = other_.GetComponent<Monster>();
            // 중복 작동을 피하기위해서 콜라이더는 끈다
            collider.enabled = false;
        }
    }

    private void CheckTrapTime()
    {
        if (hitMonster == null) 
        {
            return;
        }
        if (hitMonster.bindDuration <= 0) 
        {
            trapPool.ReturnObjToPool(gameObject);
        }
    }
}

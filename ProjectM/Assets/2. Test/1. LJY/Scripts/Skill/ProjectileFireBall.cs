using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFireBall : SkillAction
{
    public LayerMask collideLayer;  // 충돌체크할 레이어    
    public GameObject impactParticle;   // 충돌 시 발생할 이펙트
    public BookScript book;         // 방향에 사용할 마법수첩 스크립트
    private Vector3 direction;      // 탄환이 나아갈 방향
    public float speed = 5f;        // 탄환 속도

    public Damage fireInfo;

    // Start is called before the first frame update
    void Start()
    {
        book = transform.parent.GetComponent<BookScript>();
        transform.SetParent(null);
        direction = (book.target - transform.position).normalized;      
        transform.LookAt(book.target);

        fireInfo = ReturnInfo("FireBall") as Damage;    // 파이어인포에 스킬정보 가져오기
        speed = fireInfo.Value1;
        damage = fireInfo.skillDamage;

        // Rigidbody를 사용하여 Projectile에 속도 부여
        GetComponent<Rigidbody>().velocity = direction * speed;

        StartCoroutine(DestroyBall());
    }

    public void DestroyObject(Vector3 hitNor)
    {
        if (gameObject != null)
        {
            Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hitNor));
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((collideLayer & (1 << other.gameObject.layer)) != 0)
        {
            Vector3 hitNormal = transform.position - other.transform.position;
            DestroyObject(hitNormal);
        }
    }

    private IEnumerator DestroyBall()
    {
        yield return new WaitForSecondsRealtime(10f);

        if (this != null)
        {
            Destroy(gameObject);
        }
    }
}

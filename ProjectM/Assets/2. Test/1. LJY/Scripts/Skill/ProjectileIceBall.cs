using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileIceBall : SkillAction
{
    public LayerMask collideLayer;  // 충돌체크할 레이어    
    public GameObject impactParticle;   // 충돌 시 발생할 이펙트
    public BookScript book;         // 방향에 사용할 마법수첩 스크립트
    private Vector3 direction;      // 탄환이 나아갈 방향
    public float speed = 10f;        // 탄환 속도

    public NoDamage iceInfo;
    // Start is called before the first frame update
    void Start()
    {
        book = transform.parent.GetComponent<BookScript>();
        transform.SetParent(null);
        direction = (book.target - transform.position).normalized;
        transform.LookAt(book.target);

        iceInfo = ReturnInfo("IceBall") as NoDamage;    // 아이스인포에 스킬정보 담기
        speed = iceInfo.Value2;
        statusEffId = iceInfo.Value1;
        CheckSkill();

        // Rigidbody를 사용하여 Projectile에 속도 부여
        GetComponent<Rigidbody>().velocity = direction * speed;

        StartCoroutine(DestroyBall());
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((collideLayer & (1 << other.gameObject.layer)) != 0)
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            DestroyObject(hitPoint);
        }
    }

    public void DestroyObject(Vector3 hit)
    {
        if (gameObject != null)
        {
            Instantiate(impactParticle, hit, impactParticle.transform.rotation);
            Destroy(gameObject);
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

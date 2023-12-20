using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerPoison : MonoBehaviour
{
    public Vector3 destination;  // 발사할 목표 지점
    public GameObject poisonMagic;  // 소환할 강화 독장판
    public BookScript book;
    public LayerMask collideLayer;
    public float speed = 10f;  // 발사 속도
    // Start is called before the first frame update
    void Start()
    {
        book = transform.parent.GetComponent<BookScript>();
        destination = book.target;
        transform.SetParent(null);
        transform.LookAt(destination);
        if (destination != null)
        {
            // 목표 지점과 현재 위치 사이의 방향 계산
            Vector3 direction = (destination - transform.position).normalized;

            // Rigidbody를 사용하여 Projectile에 속도 부여
            GetComponent<Rigidbody>().velocity = direction * speed;
        }

        StartCoroutine(DestroyBall());
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((collideLayer & (1 << other.gameObject.layer)) != 0)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        if (gameObject != null)
        {
            Instantiate(poisonMagic, transform.position, poisonMagic.transform.rotation);
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

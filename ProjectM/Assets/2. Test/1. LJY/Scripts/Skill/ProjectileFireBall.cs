using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFireBall : MonoBehaviour
{
    public LayerMask collideLayer;
    public GameObject impactParticle;
    public BookScript book;
    private Vector3 direction;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        book = transform.parent.GetComponent<BookScript>();
        direction = book.rayDirection;
        transform.SetParent(null);
        StartCoroutine(DestroyBall());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
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

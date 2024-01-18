using System.Collections;
using UnityEngine;

public class Yeti_ProjectileATK : MonsterATK
{
    private WaitForSeconds lifeTime;
    private WaitForSeconds chargeTime;

    private GameObject chargeEffect;
    private GameObject projectileEffect;
    private GameObject hitEffect;
    public GameObject target;

    public ObjectPool projectilePool;

    private float speed;


    private void Awake()
    {
        lifeTime = new WaitForSeconds(10f);
        chargeTime = new WaitForSeconds(2f);
        projectilePool = GameObject.Find("Pool_YetiProjectile").GetComponent<ObjectPool>();
    }

    private void OnEnable()
    {
        monster = GameObject.Find("BossMap").transform.Find("Boss_1").transform.Find("Yeti_King").gameObject;
        //transform.position = monster.transform.position + (Vector3.up * 3f);
        damage = monster.GetComponent<Monster>().monsterData.Skill2Damage;

        chargeEffect = transform.GetChild(0).gameObject;
        projectileEffect = transform.GetChild(1).gameObject;
        hitEffect = transform.GetChild(2).gameObject;
        target = monster.GetComponent<Monster>().target;

        speed = 10f;

        gameObject.GetComponent<SphereCollider>().enabled = true;

        StartCoroutine(ProjectileRoutine());
        StartCoroutine(LifeTime());
    }

    private void OnDisable()
    {
        StopCoroutine(ProjectileRoutine());
        StopCoroutine(LifeTime());
        StopCoroutine(HitEffectPlay());

        monster = default;
        damage = default;

        chargeEffect = default;
        projectileEffect = default;
        hitEffect = default;
        target = default;

        speed = default;
    }

    private void OnTriggerEnter(Collider other_)
    {
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Player")) || other_.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain")) 
            || other_.gameObject.layer.Equals(LayerMask.NameToLayer("Breakable")))
        {
            StartCoroutine(HitEffectPlay());
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private IEnumerator ProjectileRoutine() 
    {
        chargeEffect.SetActive(true);
        yield return chargeTime;
        chargeEffect.SetActive(false);
        projectileEffect.SetActive(true);

        while (Vector3.Distance(transform.position, target.transform.position) > 10f) 
        {
            yield return null;

            gameObject.GetComponent<CharacterController>().Move((target.transform.position - transform.position).normalized * speed * Time.deltaTime);
            transform.LookAt(target.transform);
        }

        Vector3 lastPos = target.transform.position;

        while (Vector3.Distance(transform.position, lastPos) >= 0f)
        {
            yield return null;

            gameObject.GetComponent<CharacterController>().Move((lastPos - transform.position).normalized * speed * 1.2f * Time.deltaTime);
        }
    }

    private IEnumerator LifeTime() 
    {
        yield return lifeTime;
        projectileEffect.SetActive(false);
        projectilePool.ReturnObjToPool(gameObject);
    }

    private IEnumerator HitEffectPlay() 
    {
        projectileEffect.SetActive(false);
        hitEffect.SetActive(true);
        yield return chargeTime;
        hitEffect.SetActive(false);
        projectilePool.ReturnObjToPool(gameObject);
    }
}

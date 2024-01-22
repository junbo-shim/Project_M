using UnityEngine;

public class Yeti_Wall : MonoBehaviour
{
    public ObjectPool effectPool;
    private ParticleSystem effect;
    private MeshRenderer wallMesh;
    private MeshCollider wallCollider;

    private void Awake()
    {
        effectPool = GameObject.Find("Pool_Wall").GetComponent<ObjectPool>();
    }

    private void OnEnable()
    {
        wallMesh = gameObject.GetComponent<MeshRenderer>();
        wallCollider = gameObject.GetComponent<MeshCollider>();
        effect = effectPool.ActiveObjFromPool(transform.position).GetComponent<ParticleSystem>();
    }

    private void OnDisable()
    {
        wallMesh = default;
        wallCollider = default;
        effect = default;
    }

    private void OnTriggerEnter(Collider other_)
    {
        if (other_.gameObject.GetComponent<Yeti_ChargeATK>() == true) 
        {
            effect.Play();
            wallMesh.enabled = false;
            wallCollider.enabled = false;
            effectPool.ReturnObjToPool(effect.gameObject);
        }
    }
}

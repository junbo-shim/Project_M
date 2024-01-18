using UnityEngine;

public class Yeti_Wall : MonoBehaviour
{
    public ParticleSystem effect;

    private MeshRenderer wallMesh;
    private MeshCollider wallCollider;

    private void OnEnable()
    {
        wallMesh = gameObject.GetComponent<MeshRenderer>();
        wallCollider = gameObject.GetComponent<MeshCollider>();
    }

    private void OnDisable()
    {
        wallMesh = default;
        wallCollider = default;
    }

    private void OnTriggerEnter(Collider other_)
    {
        if (other_.gameObject.GetComponent<Yeti_ChargeATK>() == true) 
        {
            effect.Play();
            wallMesh.enabled = false;
            wallCollider.enabled = false;
        }
    }
}

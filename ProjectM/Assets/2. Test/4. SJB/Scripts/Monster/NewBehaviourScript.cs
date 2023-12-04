using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector3 frontPoint;
    private float angled;
    public bool isIn;

    void Start()
    {
        GameObject sphere = GameObject.Find("Sphere");
        isIn = false;
        angled = 90f;
    }

    private void Update()
    {
        Debug.DrawRay(transform.parent.position, transform.parent.forward * 15, Color.red);
        frontPoint = transform.parent.position + transform.parent.forward * gameObject.GetComponent<SphereCollider>().radius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.LogWarning(other.transform.position);

            float angle = Mathf.Atan2(other.transform.position.z - transform.position.z, other.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

            Debug.LogError(angle);

            if (angle > angled * 0.5f && angle < 180f - (angled * 0.5f)) 
            {
                Debug.LogWarning("!!!");
            }
        }
    }
}

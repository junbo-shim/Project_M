using UnityEngine;

public class TestSight : MonoBehaviour
{
    private Vector3 frontPoint;
    public bool isIn;

    void Start()
    {
        GameObject sphere = GameObject.Find("Sphere");
        isIn = false;
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
            Debug.LogError(other.name);
            Debug.LogError(other.transform.position);

            float test = Vector3.Angle(frontPoint, other.transform.position);

            Debug.LogError(frontPoint);
            Debug.LogError(test);

            if (Check(other, test) == true)
            {
                transform.parent.transform.LookAt(other.transform);
            }
        }
    }

    private bool Check(Collider other, float angle) 
    {
        if (angle > 60) 
        {
            isIn = false;
        }
        else 
        {
            isIn = true;
        }
        return isIn;
    }
}

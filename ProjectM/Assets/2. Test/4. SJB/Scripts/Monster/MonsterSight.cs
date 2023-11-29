using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    public GameObject monster;
    public float viewRange;
    public float viewAngle;
    public Vector3 frontPoint;
    public bool isInAngle;


    private void Start()
    {
        InitSight();
    }

    private void FixedUpdate()
    {
        frontPoint = transform.parent.position + transform.parent.forward * gameObject.GetComponent<SphereCollider>().radius;
    }

    private void InitSight() 
    {
        monster = transform.parent.gameObject;
        viewRange = monster.GetComponent<TestMonster>().sightRange;
        viewAngle = monster.GetComponent<TestMonster>().sightAngle * 0.5f;

        isInAngle = false;
    }

    private void OnTriggerStay(Collider other_)
    {
        if (other_.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.LogError(other_.name);
            Debug.LogError(other_.transform.position);

            float angle = Vector3.Angle(frontPoint, other_.transform.position);

            Debug.LogError(frontPoint);
            Debug.LogError(angle);

            if (Check(angle) == true)
            {
                transform.parent.transform.LookAt(other_.transform);
            }
        }
    }

    private bool Check(float angle_)
    {
        if (angle_ > viewAngle)
        {
            isInAngle = false;
        }
        else
        {
            isInAngle = true;
        }
        return isInAngle;
    }
}

using UnityEngine;

public class Test3 : MonoBehaviour
{
    public SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Test1>() == true) 
        {
            other.GetComponent<Test2>().TestFunc();
        }
    }
}

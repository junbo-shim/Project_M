using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Teleport"))
        {
            Debug.Log("텔포에닿았음"); 
            transform.position = other.GetComponent<TPObject>().tpPoint.position;
        }
    }
}

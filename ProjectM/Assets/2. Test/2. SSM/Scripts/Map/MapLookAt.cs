using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLookAt : MonoBehaviour
{
    [SerializeField] GameObject targetObj;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(targetObj.transform.position);
    }

   
}

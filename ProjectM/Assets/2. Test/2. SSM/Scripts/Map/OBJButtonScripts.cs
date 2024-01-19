using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJButtonScripts : MonoBehaviour
{
    public GameObject GameObject_1;
    public void OnTriggerEnter(Collider other)
    {
        if(!GameObject_1.activeSelf)
        {
            if(other.CompareTag("Player"))
            {
                GameObject_1.SetActive(true);

            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                GameObject_1.SetActive(false);

            }
        }
      
 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject_1.SetActive(false);
        }

    }
}

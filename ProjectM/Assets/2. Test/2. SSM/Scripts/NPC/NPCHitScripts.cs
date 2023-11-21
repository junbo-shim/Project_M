using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHitScripts : MonoBehaviour
{
    private NPCManager nPCManager;

 
    // Start is called before the first frame update
    void Start()
    {
        nPCManager = transform.root.GetChild(4).GetComponent<NPCManager>();

    }

    public void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.layer > 9)
        {
            nPCManager.HitNPC();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChildSet : MonoBehaviour
{
    public  List<GameObject> targetOBj;

 
    public void Awake()
    {

        targetOBj = new List<GameObject>();
        Transform target = transform.GetChild(2);

        int targetChildCount = 0;
        if (target.transform != null)
        {
            targetChildCount = target.childCount;
        }

        for (int i = 0; i < targetChildCount; i++)
        {
            targetOBj.Add(target.GetChild(i).gameObject);
        }
    }
}

    

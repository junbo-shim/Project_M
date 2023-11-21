using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NPCManager : MonoBehaviour
{

    private Transform playerPos;
 
    public void Awake()
    {
       
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    public void NPCCrash() // 플레이어 충돌시 
    {
        float PlayerDisrance = Vector3.Distance(transform.position, playerPos.position);
        if(PlayerDisrance < 0.3f)
        {
            Debug.Log("a");
        }
    }

    public void HitNPC() // 플레이어 충돌시 
    {
       
        Debug.Log("b");
        
    }
  
}
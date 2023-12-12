using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NPCManager : MonoBehaviour
{

    private Transform playerPos;
 
    public void Awake()
    {
       
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("npc로드중");
    }

    public void HitNPC() // 플레이어 충돌시 
    {
       
        Debug.Log("b");
        
    }

    public void SelectTack()
    {
        
    }
}
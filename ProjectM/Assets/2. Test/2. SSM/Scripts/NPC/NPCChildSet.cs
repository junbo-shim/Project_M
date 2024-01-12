using System                                                                                                                                                                 .Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCChildSet : MonoBehaviour
{
    public  List<GameObject> targetOBj;

    public Transform ChoiceTransform;

    public NPCTack npcAction;
    public void Awake()
    {


        Caching();
    }

  
    private void Caching()
    {
        //npcTackUI 내부 Transform 저장용 {
        targetOBj = new List<GameObject>();
        Transform target = transform.Find("npcTackUI"); //npcTackUI 위치 가리키도록 
        if(target != null)
        {
              int targetChildCount = 0;
        if (target.transform != null)
        {
            targetChildCount = target.childCount;
        }

        for (int i = 0; i < targetChildCount; i++)
        {
            targetOBj.Add(target.GetChild(i).gameObject);
        }
        //npcTackUI 내부 Transform 저장용 }
        //선택지 박스 위치 저장용 {
        ChoiceTransform = transform.Find("npcTackUI").transform.GetChild(4).transform.GetChild(1);

        }
      
        //선택지 박스 위치 저장용 }
        // npcAction 스크립트 저장용
        npcAction = transform.Find("NpcAction").GetComponent<NPCTack>();
        // npcAction 스크립트 저장용끝

    }
}

    

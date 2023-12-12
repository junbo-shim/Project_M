 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpScripts : MonoBehaviour
{
    public int spCount;// 스폰 수
    public int lv; // 어디선가 레벨정보 넣기
    int[] spObj;// 스폰오브젝트 수
    List <int> spCountList;
   
    public void OnEnable()
    {
        spCountList = new List<int>();
  
        itemSp();
    }

    public void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetChild(lv).gameObject.activeSelf)
            {
                transform.GetChild(i).GetChild(lv).gameObject.SetActive(false);
            }

        }       
    }
    public void itemSp() 
    {

        if (MapGameManager.instance.currentState == DayState.NIGHT)
        { 
            while(spCountList.Count < spCount)
            {
                
                int rendSP = Random.Range(0, transform.childCount);
                if(!spCountList.Contains(rendSP))
                {
                    spCountList.Add(rendSP);
                }
            }
             
            for (int i = 0; i < spCountList.Count; i++)
            {               
                transform.GetChild(spCountList[i]).GetChild(lv).gameObject.SetActive(true);
            }

        }
       
    }
}

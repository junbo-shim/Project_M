using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpScripts : MonoBehaviour
{
    public int spCount;// 스폰 수
    public int lv;
    int[] spObj;
    List <int> spCountList;
   
    public void OnEnable()
    {
        spCountList = new List<int>();
        spObj = new int[transform.childCount];
        for (int i = 0; i < spObj.Length; i++)
        {
            spObj[i] = i;

        }
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
                
                int w = Random.Range(0, spObj.Length);
                if(!spCountList.Contains(w))
                {
                    spCountList.Add(w);
                }
            }
             
            for (int i = 0; i < spCountList.Count; i++)
            {               
                transform.GetChild(spCountList[i]).GetChild(lv).gameObject.SetActive(true);
            }

        }
       
    }
}

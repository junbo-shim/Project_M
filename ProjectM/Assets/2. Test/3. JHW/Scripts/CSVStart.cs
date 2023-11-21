using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVStart : MonoBehaviour
{
    /* [SerializeField]
     private ScriptableObject scriptableObject;*/

 


    public ItemData scriptableObject;

    public void Start()
    {

        //csv 읽고
        List<Dictionary<string, object>> data_Dalog = CSVReader.Read("Item");
        //ItemDataType itemDataType = new ItemDataType();

        foreach(var a in data_Dalog)
        {

            Debug.Log(a.Values);
        }

        //세 데이터 생성
        ItemDataType itemDataType = new ItemDataType();
        for (int i = 0; i < data_Dalog.Count; i++)
        {
            //데이터 복사
            itemDataType.ID = Convert.ToInt32(data_Dalog[i]["ID"]);
           
            itemDataType.itemName = (string)data_Dalog[i]["ItemName"];

            itemDataType.count = (int)data_Dalog[i]["Count"];


            //Debug.Log(scriptableObject.ItemDataList.Count);
            scriptableObject.ItemDataList.Add(itemDataType);

        }
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Item> itemDB = new List<Item>();
    [Space(32)]
    public GameObject fieldItemPrefab;
    public Vector3[] pos;
    

    public void Start()
    {
        MakeRandomPos();
    }


    // LEGACY
    //public void Start()
    //{
    //    for (int i = 0; i < 32; i++)
    //    {
    //        GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
    //        go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 13)]); 
    //    }
    //}


    // DEMO : 12.9 SJB Editted
    public GameObject player;
    private void MakeRandomPos() 
    {
        pos = new Vector3[32];

        for (int i = 0; i < 32; i++) 
        {
            float randomX = Random.Range(-5, 5);
            float randomZ = Random.Range(-5, 5);
            pos[i] = player.transform.position + new Vector3(randomX, 0f, randomZ);

            GameObject testItem = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            testItem.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 13)]);
        }
    }
}

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
        for (int i = 0; i < 32; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 13)]); 
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;
    // 23.12.20 SJB Editted
    public Transform player;


    private void Awake()
    {
        Instance = this;
    }

    public List<Item> itemDB = new List<Item>();


    [Space(32)]
    public GameObject fieldItemPrefab;

    public void Start()
    {
        SpreadItem();

    }

    //23.12.20 SJB Editted
    private void SpreadItem()
    {
        for (int i = 0; i < 32; i++)
        {
            Vector3 pos = new Vector3(player.position.x + Random.Range(-3, 4), player.position.y, player.position.z + Random.Range(-3, 4));

            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 13)]);
        }
    }

}

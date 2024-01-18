using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;
    // 23.12.20 SJB Editted
    public Transform player;


    public Transform[] recipes;

    public Transform herb;


    private void Awake()
    {
        Instance = this;
    }

    public List<Item> itemDB = new List<Item>();


    [Space(32)]
    public GameObject fieldItemPrefab;

    public void Start()
    {
        //SpreadItem();
        SpawnRecipe();
        SpawnHerb();
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


    private void SpawnRecipe()
    {
        for (int i = 0; i < recipes.Length; i++)
        {

            Vector3 pos = new Vector3(recipes[i].position.x + Random.Range(-3, 4), recipes[i].position.y, recipes[i].position.z + Random.Range(-3, 4));
            Vector3 pos2 = new Vector3(recipes[i].position.x + Random.Range(-3, 4), recipes[i].position.y, recipes[i].position.z + Random.Range(-3, 4));
            Vector3 pos3 = new Vector3(recipes[i].position.x + Random.Range(-3, 4), recipes[i].position.y, recipes[i].position.z + Random.Range(-3, 4));


            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            GameObject go2 = Instantiate(fieldItemPrefab, pos2, Quaternion.identity);
            GameObject go3 = Instantiate(fieldItemPrefab, pos3, Quaternion.identity);

            go.GetComponent<FieldItem>().SetItem(itemDB[i]);
            go2.GetComponent<FieldItem>().SetItem(itemDB[i]);
            go3.GetComponent<FieldItem>().SetItem(itemDB[i]);

        }
    }

    private void SpawnHerb()
    {
        Vector3 pos = new Vector3(herb.position.x + Random.Range(-3, 4), herb.position.y, herb.position.z + Random.Range(-3, 4));
        Vector3 pos2 = new Vector3(herb.position.x + Random.Range(-3, 4), herb.position.y, herb.position.z + Random.Range(-3, 4));
        Vector3 pos3 = new Vector3(herb.position.x + Random.Range(-3, 4), herb.position.y, herb.position.z + Random.Range(-3, 4));


        GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
        GameObject go2 = Instantiate(fieldItemPrefab, pos2, Quaternion.identity);
        GameObject go3 = Instantiate(fieldItemPrefab, pos3, Quaternion.identity);

        go.GetComponent<FieldItem>().SetItem(itemDB[8]);
        go2.GetComponent<FieldItem>().SetItem(itemDB[8]);
        go3.GetComponent<FieldItem>().SetItem(itemDB[8]);
    }


}

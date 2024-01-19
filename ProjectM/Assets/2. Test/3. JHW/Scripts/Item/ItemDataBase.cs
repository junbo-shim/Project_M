using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase Instance;
    // 23.12.20 SJB Editted
    public Transform player;


    public Transform[] recipes;

    public Transform[] herb;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


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

            Vector3 pos = new Vector3(recipes[i].position.x + Random.Range(-70, 70), recipes[i].position.y, recipes[i].position.z + Random.Range(-70, 70));
            Vector3 pos2 = new Vector3(recipes[i].position.x + Random.Range(-70, 70), recipes[i].position.y, recipes[i].position.z + Random.Range(-70, 70));
            Vector3 pos3 = new Vector3(recipes[i].position.x + Random.Range(-70, 70), recipes[i].position.y, recipes[i].position.z + Random.Range(-70, 70));


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
        for (int i = 0; herb.Length > i; i++)
        {
            Vector3 pos = new Vector3(herb[i].position.x + Random.Range(-50, 50), herb[i].position.y, herb[i].position.z + Random.Range(-50, 50));
            Vector3 pos2 = new Vector3(herb[i].position.x + Random.Range(-50, 50), herb[i].position.y, herb[i].position.z + Random.Range(-50, 50));
            Vector3 pos3 = new Vector3(herb[i].position.x + Random.Range(-50, 50), herb[i].position.y, herb[i].position.z + Random.Range(-50, 50));

            Vector3 vector3 = new Vector3(-90, 0, 0);

            Quaternion quaternion =Quaternion.Euler(vector3);

            GameObject go = Instantiate(fieldItemPrefab, pos, Quaternion.identity);
            GameObject go2 = Instantiate(fieldItemPrefab, pos2, Quaternion.identity);
            GameObject go3 = Instantiate(fieldItemPrefab, pos3, Quaternion.identity);

         
            go.GetComponent<FieldItem>().SetItem(itemDB[8]);
            go2.GetComponent<FieldItem>().SetItem(itemDB[8]);
            go3.GetComponent<FieldItem>().SetItem(itemDB[8]);
            go.transform.GetChild(2).transform.rotation = quaternion;
            go2.transform.GetChild(2).transform.rotation = quaternion;
            go3.transform.GetChild(2).transform.rotation = quaternion;
            Rigidbody rb1 = go.GetComponent<Rigidbody>();
            rb1.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
          
            Rigidbody rb2 = go2.GetComponent<Rigidbody>();
            rb2.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

            Rigidbody rb3 = go3.GetComponent<Rigidbody>();
            rb3.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        }

    }


}

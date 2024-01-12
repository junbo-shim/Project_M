using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayModeSave : MonoBehaviour
{
    public Rigidbody[] trees;
    public string path;

    // Start is called before the first frame update
    void Start()
    {
        trees = gameObject.transform.GetComponentsInChildren<Rigidbody>();
        path = "Assets/1. Main/Prefabs/TreesOnGround/";
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P)) 
        {
            RenameAndSave();
        }
    }

    private void RenameAndSave() 
    {
        for (int i = 1; i < trees.Length + 1; i++) 
        {
            trees[i].gameObject.name = i.ToString();

            bool prefabSuccess;
            PrefabUtility.SaveAsPrefabAsset(trees[i].gameObject, path + trees[i].gameObject.name + ".prefab", out prefabSuccess);

            if (prefabSuccess.Equals(true))
            {
                Debug.LogFormat("저장성공:" + gameObject.name);
            }
            else
            {
                Debug.LogWarningFormat("저장실패:" + gameObject.name);
            }
        }
    }
}

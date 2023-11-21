using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SaveDataFile testSaveFile;
    public List<Item> testItemList;

    private void Awake()
    {
        testSaveFile = new SaveDataFile();
        testItemList = new List<Item>();
    }
}

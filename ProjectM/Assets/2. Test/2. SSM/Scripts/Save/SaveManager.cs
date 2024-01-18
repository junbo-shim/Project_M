using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class SaveManager : MonoBehaviour
{
    public MoveObjSaveData[] data;

    public string posStr;
    public string actStr;
    public string rotStr;
  
    string path;
    string filename = "obj_";
    public static SaveManager instance;

    public void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(instance);
        }
        #endregion
        data = FindObjectsOfType<MoveObjSaveData>();
        path = Application.persistentDataPath + "/";

    }


    public void saveData()
    {
        for (int i = 0; i < data.Length; i++)
        {

            data[i].SaveObj();
            string saveData = JsonUtility.ToJson(data[i]);
            File.WriteAllText(path + filename + i, saveData);
        }

        DictData dictData = new DictData();

        dictData.SetArray(SetDicArray(MBTIScripts.Instance.MBTiScore_));
        string str = JsonUtility.ToJson(dictData);
        File.WriteAllText(path + "MBTI", str);


      
    }




    public void loadData()
    {
        for (int i = 0; i < data.Length; i++)
        {
            string saveData1 = File.ReadAllText(path + filename + i);
            JsonUtility.FromJson<MoveObjSaveData>(saveData1);
        }
        string saveData2 = File.ReadAllText(path + "MBTI");
        JsonUtility.FromJson<DictData>(saveData2);
      
    }

    public string[] SetDicArray(Dictionary<string, int> dictionary)
    {
        string[] str1 = new string[dictionary.Count];
        string[] str2 = new string[dictionary.Count];

        int i = 0;
        foreach (var value in dictionary)
        {
            str1[i] = value.Key;
            str2[i] = value.Value.ToString();
            i++;
        }
        string[] strValue = new string[str1.Length];
        for (int j = 0; j < str1.Length; j++)
        {
            strValue[j] = str1[j] + "," + str2[j];
        }
        return strValue;
    }


}

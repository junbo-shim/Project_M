using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestMananger : MonoBehaviour // 저장할 스크립트
{

    public Dictionary<string, QuestData> playerQuest;

    private static QuestMananger Instance;

    public static QuestMananger instance
    {
        get
        {
            if (Instance == null)
            {

                GameObject singletonObject = new GameObject("QuestMananger");
                Instance = singletonObject.AddComponent<QuestMananger>();
            }
            return Instance;
        }
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
        playerQuest = new Dictionary<string, QuestData>();
    }
    
    public void AddPlayerQuest(string str)
    {
        if(!playerQuest.ContainsKey(str))
        {
            playerQuest.Add(str, CSVRead.instance.QuestDatas[str]);
        }
    }


}

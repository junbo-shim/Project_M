using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestMananger : MonoBehaviour // 저장할 스크립트
{

    public Dictionary<string, QuestData> playerQuest;  // 플레이어가 수락한 퀘스트 정보

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
    
    public void AddPlayerQuest(string str) // 퀘스트 수주
    {
        if(!playerQuest.ContainsKey(str))
        {
            playerQuest.Add(str, CSVRead.instance.QuestDatas[str]);
            playerQuest[str].Situation = "Progress";

        }
    }

    public void QuestComplete(string str) // 퀘스트 완료
    {
        if (!playerQuest.ContainsKey(str))
        {
            playerQuest[str].Situation = "Complete";
        }
        
    }

}

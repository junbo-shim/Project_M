using UnityEngine;
using UnityEngine.UI;



public class ChoiceScripts : MonoBehaviour
{
    public int id; // 대사 id
    public int number; // 선택지 1 대답
    public string Choice_Text_Answer;
    public int mbti_ID; // mbti1 값
    public NPCChildSet npcChildSet;
    public int[] scores; // 전역 변수 선언

    //public int Num => _num;
    // private int _num;
    // public System.Collections.Generic.Dictionary<string, BasicQuest> PlayerQuest => QuestMananger.instance.playerQuest;



    public void OnEnable()
    {

        GameObject parentGameObject = ReParentSerach(gameObject);
        npcChildSet = parentGameObject.GetComponent<NPCChildSet>();

    }
    public void ChoiceClick()
    {
        npcChildSet.npcAction.ChoiseClick(number, Choice_Text_Answer ,mbti_ID);
        GetMBTIDatas();// mbti 에 점수 추가

     
    }

    public void GetMBTIDatas() // mbti 에 점수 추가
    {
        if(mbti_ID == -1)
        {
            return;
        }

        var mbtiDatas =  CSVRead.instance.MBTIDatas[mbti_ID.ToString()];
        var playerMBTI = MBTIScripts.Instance;
        scores = new[] { mbtiDatas.MBTiScore_I, mbtiDatas.MBTiScore_N,
                         mbtiDatas.MBTiScore_S, mbtiDatas.MBTiScore_J,
                         mbtiDatas.MBTiScore_P, mbtiDatas.MBTiScore_E,
                         mbtiDatas.MBTiScore_T, mbtiDatas.MBTiScore_F };
       
        for (int i = 0; i < scores.Length; i++)
        {
            
            if (scores[i] > 0)
            {
            
                switch (i)
                {
                    case 0:
                        playerMBTI.MBTI_I_Add(scores[i]);
                        break;
                    case 1:
                        playerMBTI.MBTI_N_Add(scores[i]);
                        break;
                    case 2:
                        playerMBTI.MBTI_S_Add(scores[i]);
                        break;
                    case 3:
                        playerMBTI.MBTI_J_Add(scores[i]);
                        break;
                    case 4:
                        playerMBTI.MBTI_P_Add(scores[i]);
                        break;
                    case 5:
                        playerMBTI.MBTI_E_Add(scores[i]);
                        break;
                    case 6:
                        playerMBTI.MBTI_T_Add(scores[i]);
                        break;
                    case 7:
                        playerMBTI.MBTI_F_Add(scores[i]);
                        break;
                    default:
                        Debug.LogError(i + "MBTI Value Error");
                        break;
                }


            }
        }
    }


    public GameObject ReParentSerach(GameObject gameObject) // 최상위 부모 찾기 재귀
    {
        if (gameObject.transform.parent != null)
        {
            return ReParentSerach(gameObject.transform.parent.gameObject);
        }
        else
        {
            return gameObject;
        }


    }
}

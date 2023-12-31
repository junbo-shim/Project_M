using UnityEngine;
using UnityEngine.UI;



public class ChoiceScripts : MonoBehaviour
{
    public int id; // 대사 id
    public int number; // 선택지 1 대답
    public string Choice_Text_Answer;
    public int mbti_ID; // mbti1 값
    public NPCChildSet npcChildSet;
  

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

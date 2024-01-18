using UnityEngine;
using UnityEngine.UI;



public class ChoiceScripts : MonoBehaviour
{
    public int id; // 대사 id
    public int number; // 선택지 1 대답
    public string Choice_Text_Answer;
    public int mbti_ID; // mbti1 값
    public NPCChildSet npcChildSet;
    public Inventory inventory;

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
        if(id == 400040)
        {
            if(inventory != null)
            {
                for (int i = 0; inventory.items.Count > 0; i++)
                {
                    if (inventory.items[i].itemName == "SymbolStone_A")
                    {
                        inventory.items[i].itemCount = inventory.items[i].itemCount + 1;
                    }
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

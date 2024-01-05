using UnityEngine;

public class QuestUI : MonoBehaviour
{

    public GameObject[] quest;


    public void MainQuest1()
    {
        quest[0].SetActive(true);
        quest[1].SetActive(false);
        quest[2].SetActive(false);
    }

    public void MainQuest2()
    {
        quest[0].SetActive(false);
        quest[1].SetActive(true);
        quest[2].SetActive(false);
    }

    public void MainQuest3()
    {
        quest[0].SetActive(false);
        quest[1].SetActive(false);
        quest[2].SetActive(true);
    }
}

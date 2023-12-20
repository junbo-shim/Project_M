using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialCanvas;


    public Image tutorialImg;
    public Text tutorialText;

    public Sprite[] newImage; // �� �̹����� ����ϱ� ���� ����


    private void OnTriggerEnter(Collider other)
    {
        

        if(other.CompareTag("CheckPoint"))
        {

            //if (���� == 0)
            //{
            //    tutorialImg.sprite = newImage[0];
            //    tutorialText.text = newText[0];

            //}
            //else if ()
            //{

            //}
            //else if ()
            //{

            //}
            //else if ()
            //{

            //}
            //else if ()
            //{

            //}
            tutorialImg.sprite = newImage[0];
            tutorialText.text = "���긦 ���� �ֺ��� �ѷ�������";

            tutorialCanvas.transform.position = new Vector3(other.transform.position.x , other.transform.position.y+4, other.transform.position.z + 18);
            tutorialCanvas.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            tutorialCanvas.SetActive(false);
        }
    }
}

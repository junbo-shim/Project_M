using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialCanvas;


    public Image tutorialImg;
    public Text tutorialText;

    public Sprite[] newImage; // 새 이미지를 사용하기 위한 변수


    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("CheckPoint"))
        {

            //if (조건 == 0)
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
            tutorialText.text = "오브를 꺼내 주변을 둘러보세요";

            tutorialCanvas.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 4, other.transform.position.z + 18);
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

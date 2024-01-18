using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Ending : MonoBehaviour
{


    public GameObject mbtiUI;

    public GameObject endingCreditUI;

    public float fadeSpeed = 0.00f;

    public float upSpeed = 10;

    public RectTransform rectTransform;

    public GameObject endingUIOffButton;

    public GameObject endingUI;


    public Button button;

    private void Start()
    {
        StartCoroutine(StartEnd());
    }

    IEnumerator StartEnd()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeInMBTI());
        yield return new WaitForSeconds(10f);
        StartCoroutine(FadeOutMBTI());
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInEndingCredit());
        yield return new WaitForSeconds(2f);
        StartCoroutine(UpEndingCredit());
        yield return null;  
    }

    IEnumerator FadeInMBTI()
    {
        float mbtiAlpha = mbtiUI.GetComponent<CanvasGroup>().alpha;


        while (mbtiAlpha < 1.0f)
        {
            mbtiAlpha += fadeSpeed * Time.deltaTime;

            mbtiUI.GetComponent<CanvasGroup>().alpha = mbtiAlpha;

            yield return null;
        }
    }

    IEnumerator FadeOutMBTI()
    {
        float mbtiAlpha = mbtiUI.GetComponent<CanvasGroup>().alpha;


        while (mbtiAlpha > 0.001f)
        {
            mbtiAlpha -= fadeSpeed * Time.deltaTime;

            mbtiUI.GetComponent<CanvasGroup>().alpha = mbtiAlpha;

            yield return null;
        }
    }

    IEnumerator FadeInEndingCredit()
    {
        float mbtiAlpha = endingCreditUI.GetComponent<CanvasGroup>().alpha;


        while (mbtiAlpha < 1.0f)
        {
            mbtiAlpha += fadeSpeed * Time.deltaTime;

            endingCreditUI.GetComponent<CanvasGroup>().alpha = mbtiAlpha;

            yield return null;
        }
    }

    IEnumerator UpEndingCredit()
    {
        Vector3 endingPos = endingCreditUI.GetComponent<RectTransform>().anchoredPosition;
        Debug.Log(endingCreditUI.name);

        while (endingPos.y < 2250)
        {
            endingPos.y += upSpeed * Time.deltaTime;

            rectTransform.anchoredPosition = new Vector3(0, endingPos.y, 0);

            yield return null;
        }

        endingUIOffButton.SetActive(true);

    }


    public void OffEndingUI()
    {
        endingUI.SetActive(false);
    }

}

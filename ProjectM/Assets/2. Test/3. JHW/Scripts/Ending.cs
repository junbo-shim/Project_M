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

    public GameObject endingUI;

    public Button button;

    public GameObject cameraObj;

    private Camera cullingCamera;

    public LayerMask layerMask;

    public Vector3 newPosition;
    public Vector3 orgPosition;

    public GameObject player;



    private void Start()
    {
        cullingCamera = cameraObj.GetComponent<Camera>();
        //StartEndCoroutine();
    }

    public void StartEndCoroutine()
    {

        endingUI.SetActive(true);
        //SetCullingCamera();
        StartCoroutine(StartEnd());
    }

    public void SetCullingCamera()
    {
        cullingCamera.cullingMask = 0;
        cullingCamera.cullingMask = layerMask;
    }

    IEnumerator StartEnd()
    {
        //player.transform.localPosition = newPosition;
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeInMBTI());
        yield return new WaitForSeconds(10f);
        StartCoroutine(FadeOutMBTI());
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInEndingCredit());
        yield return new WaitForSeconds(2f);
        StartCoroutine(UpEndingCredit());
        //player.transform.localPosition = orgPosition;

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



        while (endingPos.y < 700)
        {
            endingPos.y += upSpeed * Time.deltaTime;

            rectTransform.anchoredPosition = new Vector3(0, endingPos.y, 0);

            yield return null;
        }

        button.enabled = true;
        endingUI.SetActive(false);
        cullingCamera.cullingMask = -1;

    }


    public void OffEndingUI()
    {
        //button.enabled = true;
        //endingUI.SetActive(false);
        //cullingCamera.cullingMask = -1;
    }

}
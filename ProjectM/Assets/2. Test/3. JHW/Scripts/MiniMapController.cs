using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    private int id = Shader.PropertyToID("Vector1_98d33b1d219b486e97f4a6d459a007a3");
    private int direction = Shader.PropertyToID("Vector1_d6c62a52b7fe47f88987fd02c26c39fe");

    private Material m = default;
    private float number = 0f;
    private float testFloat = 0f;
    public float speed = 0.1f;

    public float fadeSpeed = 0.5f;    //불투명 해지는 속도
    //private bool fadeInStarted = false;


    public GameObject miniMap;
    public GameObject miniMap2;





    public bool isacting = false;

    void Start()
    {
        m = GetComponent<Renderer>().material;
        m.SetFloat(id, number);


        miniMap = this.transform.GetChild(0).gameObject;
        miniMap2 = this.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (isacting == false)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isacting = true;
                OpenMap();
                Debug.Log(testFloat);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                isacting = true;
                CloseMap();
                Debug.Log(testFloat);
            }
        }
    }

    public void OpenMap()
    {
        m.SetFloat(direction, -1);

        StartCoroutine(OpenM());
    }


    IEnumerator OpenM()
    {
        while (testFloat < 1f)
        {

            testFloat += speed * 0.1f;
            if (testFloat > 0.9f)
            {
                StartCoroutine(FadeIn());
            }

            m.SetFloat(id, testFloat);

            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        isacting = false;
    }







    public void CloseMap()
    {
        m.SetFloat(direction, -1);
        StartCoroutine(Close());
    }


    IEnumerator FadeIn()
    {
        
        float mapAlpha = miniMap.GetComponent<CanvasGroup>().alpha;
        float mapAlpha2 = miniMap2.GetComponent<CanvasGroup>().alpha;


        while (mapAlpha < 1.0f && mapAlpha2 < 1.0f)
        {
            mapAlpha += fadeSpeed * Time.deltaTime;
            mapAlpha2 += fadeSpeed * Time.deltaTime;

            miniMap.GetComponent<CanvasGroup>().alpha = mapAlpha;
            miniMap2.GetComponent<CanvasGroup>().alpha = mapAlpha2;

            yield return null;
        }

    }


    IEnumerator Close()
    {

        float mapAlpha = miniMap.GetComponent<CanvasGroup>().alpha;
        float mapAlpha2 = miniMap2.GetComponent<CanvasGroup>().alpha;


        while (mapAlpha > 0.001f && mapAlpha2 > 0.001f)
        {
            mapAlpha -= fadeSpeed * Time.deltaTime;
            mapAlpha2 -= fadeSpeed * Time.deltaTime;

            miniMap.GetComponent<CanvasGroup>().alpha = mapAlpha;
            miniMap2.GetComponent<CanvasGroup>().alpha = mapAlpha2;


            yield return null;
        }



        if (mapAlpha < 0.1f && mapAlpha2 < 0.1f)
        {
            {
                while (testFloat > 0f)
                {
                    testFloat -= speed * 0.1f;
                    m.SetFloat(id, testFloat);
                    yield return new WaitForSeconds(0.01f);
                }
                yield return null;
                isacting = false;
            }
        }

    }

}
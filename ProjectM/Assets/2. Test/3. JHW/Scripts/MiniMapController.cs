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

    public RawImage rawImage;
    public RawImage rawImage2;

    public float fadeSpeed = 0.5f;    //불투명 해지는 속도
    private bool fadeInStarted = false;

    


    public bool isacting = false;

    void Start()
    {
        m = GetComponent<Renderer>().material;
        m.SetFloat(id, number);

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
        // RawImage의 초기 불투명도
        float currentAlpha = rawImage.color.a;
        float currentAlpha2 = rawImage2.color.a;


        // 불투명도가 1까지 서서히 올라가도록 반복
        while (rawImage.color.a < 1.0f && rawImage2.color.a < 1.0f)
        {
            currentAlpha += fadeSpeed * Time.deltaTime;
            currentAlpha2 += fadeSpeed * Time.deltaTime;

            Color newColor = rawImage.color;
            Color newColor2 = rawImage2.color;

            newColor.a = Mathf.Clamp01(currentAlpha);
            newColor2.a = Mathf.Clamp01(currentAlpha2);

            rawImage.color = newColor;
            rawImage2.color = newColor;

            yield return null;
        }
    }


    IEnumerator Close()
    {
        // RawImage의 초기 불투명도
        float currentAlpha = rawImage.color.a;
        float currentAlpha2 = rawImage2.color.a;


        // 불투명도가 1까지 서서히 올라가도록 반복
        while (rawImage.color.a > 0.001f && rawImage2.color.a > 0.001f)
        {
            currentAlpha -= fadeSpeed * Time.deltaTime;
            currentAlpha2 -= fadeSpeed * Time.deltaTime;

            Color newColor = rawImage.color;
            Color newColor2 = rawImage2.color;

            newColor.a = Mathf.Clamp01(currentAlpha);
            newColor2.a = Mathf.Clamp01(currentAlpha2);

            rawImage.color = newColor;
            rawImage2.color = newColor;

            yield return null;
        }


        if (rawImage.color.a < 0.1f && rawImage2.color.a < 0.1f)
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
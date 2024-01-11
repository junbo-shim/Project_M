using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TempViewUI : MonoBehaviour
{
    public Image backGround;
    public Image viewHP;
    public Image hp;

    private WaitForSeconds duration = new WaitForSeconds(1f);

    private float afterTime;    // 보여지고 난 후 흐른 시간
    private float viewTime = 1f;// 이 시간이상 흐르면 UI 페이드아웃

    private Color bGColor;
    private Color viewHpColor;
    private Color hpColor;

    private void Start()
    {
        bGColor = Color.white;
        viewHpColor = new Color(viewHP.color.r, viewHP.color.g, viewHP.color.b, 1);
        hpColor = Color.white;

        backGround.DOFade(0f, 0.01f);
        viewHP.DOFade(0f, 0.01f);
        hp.DOFade(0f, 0.01f);
    }

    public void ChangeColorToZero()
    {
        backGround.color = bGColor;
        viewHP.color = viewHpColor;
        hp.color = hpColor;
        backGround.DOFade(0f, 1f);
        viewHP.DOFade(0f, 1f);
        hp.DOFade(0f, 1f);
        //StartCoroutine(ChangeColorCT());
    }

    public IEnumerator ChangeColorCT()
    {      
        yield return duration;  // WaitForSeconds 캐싱

    }


}

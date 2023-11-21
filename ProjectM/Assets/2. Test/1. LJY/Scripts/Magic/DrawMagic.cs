using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrawMagic : MonoBehaviour
{
    [SerializeField]
    private List<Magicdot> dots;    // 마법진 그리는데 이용할 점들의 리스트
    public GameObject uiCursur;

    private LineRenderer lineRenderer; // 마법진 그리는데 사용할 라인렌더러

    private int preDotsId;
    private int dotsIndex = 0;
    public InputActionReference UIInputAction;  // 인풋 액션 중 UI 클릭 인풋
    private bool isPressing = false;


    void Start()
    {
        // Magicdot은 점들이 가지고있는 클래스로, 그 클래스들을 모을 리스트 공간 생성
        dots = new List<Magicdot>();    

        // 오브젝트들의 자식 카운트만큼 반복하여 자식들의 MagicDot 컴포넌트를 찾아와 id를 입력해주고 리스트에 추가
        for(int i = 0; i < transform.childCount; i++)
        {
            var dot = transform.GetChild(i);

            var magicdot = dot.GetComponent<Magicdot>();

            magicdot.id = i;

            dots.Add(magicdot);
        }

        // LineRenderer 초기화
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // 초기에는 아무 점도 없으므로 0으로 설정
    }

    void Update()
    {
        if (UIInputAction != null && UIInputAction.action.ReadValue<float>() == 1f)
        {      
            isPressing = true;
        }
        else if(isPressing == true)
        {
            isPressing = false;
            lineRenderer.positionCount = 0;
            dotsIndex = 0;
        }

        if(isPressing == true && lineRenderer.positionCount > 0 && uiCursur.activeSelf == true)
        {
            lineRenderer.SetPosition(dotsIndex, uiCursur.transform.position);
        }
    }

    void DrawMagicLine()
    {
        // LineRenderer를 사용하여 마법진 그리기
        
      
    }

    public void OnMouseEnterDot(Magicdot dot_)
    {
        Debug.Log("Enter 찍힘");
        if (isPressing)
        {
            Debug.Log("누른상태로 들어감");
            if (lineRenderer.positionCount == 0)
            {
                lineRenderer.positionCount += 2;
                lineRenderer.SetPosition(0, dot_.transform.position);
                dotsIndex += 1;
                preDotsId = dot_.id;
            }
            else if (dot_.id != preDotsId)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(dotsIndex, dot_.transform.position);
                dotsIndex += 1;
                preDotsId = dot_.id;
            }
        }
    }

    public void OnMouseExitDot(Magicdot dot_)
    {
        //Debug.Log(dot_.id);
    }

    public void OnMouseDownDot(Magicdot dot_)
    {
        //Debug.Log(dot_.id);
    }

    public void OnMouseUpDot(Magicdot dot_)
    {
        DrawMagicLine();
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrawMagic : MonoBehaviour
{
    [SerializeField]
    private List<Magicdot> dots;    // 마법진 그리는데 이용할 점들의 리스트
    public GameObject uiCursur;
    public List<int> firePattern = new() { 0, 3, 4, 0 };
    public List<int> posionPattern = new() { 0, 3, 2, 1, 4, 0 };
    public List<int> icePattern = new() { 1, 3, 2, 4, 1 };
    public List<int> flyPattern = new() { 0, 1, 2, 3 };
    public List<int> protectPattern = new() { 0, 1, 3, 4, 2, 0 };
    public List<int> invisiblePattern = new() { 1, 3, 4, 2, 1 };

    private List<List<int>> skillPatterns = new List<List<int>>();
    private List<int> myPattern = new List<int>();

    private LineRenderer lineRenderer; // 마법진 그리는데 사용할 라인렌더러

    private int preDotsId;
    private int dotsIndex = 0;
    public InputActionReference UIInputAction;  // 인풋 액션 중 UI 클릭 인풋
    private bool isPressing = false;

    public GameObject FireSkill;

    private Transform playerCamera;     // UI Rotate에 사용할 플레이어 카메라
    private float playerYRotation;      // UI Rotate에 사용할 플레이어 Y 회전축
    private Vector3 point;               // Line Renderer 회전에 사용할 점 포지션
    private List<Transform> dotsTransform;    // 점들의 트랜스폼 리스트

    void Start()
    {
        AddSkillList();
        // Magicdot은 점들이 가지고있는 클래스로, 그 클래스들을 모을 리스트 공간 생성
        dots = new List<Magicdot>();
        dotsTransform = new List<Transform>();

        // 오브젝트들의 자식 카운트만큼 반복하여 자식들의 MagicDot 컴포넌트를 찾아와 id를 입력해주고 리스트에 추가
        for (int i = 0; i < transform.childCount; i++)
        {
            var dot = transform.GetChild(i);

            var magicdot = dot.GetComponent<Magicdot>();

            magicdot.id = i;

            dots.Add(magicdot);
        }

        // 플레이어 카메라 트랜스폼 받아오기
        playerCamera = Camera.main.transform;

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
        else if (isPressing == true)
        {
            isPressing = false;
            CompareSkillPattern(myPattern);
            myPattern.Clear();
            dotsTransform.Clear();
            lineRenderer.positionCount = 0;
            dotsIndex = 0;
        }

        if (isPressing == true && lineRenderer.positionCount > 0 && uiCursur.activeSelf == true)
        {
            lineRenderer.SetPosition(dotsIndex, uiCursur.transform.position);
        }

        // UI, 라인렌더러 회전시키기
        playerYRotation = playerCamera.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, playerYRotation, 0);
        UpdateLine();
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
                dotsTransform.Add(dot_.transform);
                dotsIndex += 1;
                myPattern.Add(dot_.id);
                preDotsId = dot_.id;
            }
            else if (dot_.id != preDotsId)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(dotsIndex, dot_.transform.position);
                dotsTransform.Add(dot_.transform);
                dotsIndex += 1;
                myPattern.Add(dot_.id);
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
        Debug.Log("Enter 찍힘");
        if (isPressing)
        {
            Debug.Log("누른상태로 들어감");
            if (lineRenderer.positionCount == 0)
            {
                lineRenderer.positionCount += 2;
                lineRenderer.SetPosition(0, dot_.transform.position);
                dotsTransform.Add(dot_.transform);
                dotsIndex += 1;
                myPattern.Add(dot_.id);
                preDotsId = dot_.id;
            }
        }
    }

    public void OnMouseUpDot(Magicdot dot_)
    {

    }

    private void AddSkillList()
    {
        skillPatterns.Add(firePattern);
        skillPatterns.Add(posionPattern);
        skillPatterns.Add(icePattern);
        skillPatterns.Add(flyPattern);
        skillPatterns.Add(protectPattern);
        skillPatterns.Add(invisiblePattern);
    }

    public void CompareSkillPattern(List<int> myPattern_)
    {
        //for(int i = 0; i < skillPatterns.Count; i++)
        //{
        //    myPattern_.SequenceEqual(skillPatterns[i]);
        //}

        // TODO : 스킬 분류 구조를 제작해서 패턴리스트와 스킬을 연동하고, 패턴마다 다른스킬을 사용할 수 있도록 해야한다.
        if (myPattern_.SequenceEqual(firePattern))
        {
            Debug.Log("파이어패턴과 일치");
            FireSkill.SetActive(true);
        }
        else
        {
            Debug.Log("파이어패턴과 불일치");
        }
    }

    private void UpdateLine()
    {
        for(int i = 0; i < dotsTransform.Count; i++)
        {
            lineRenderer.SetPosition(i, dotsTransform[i].position);
        }
    }
}

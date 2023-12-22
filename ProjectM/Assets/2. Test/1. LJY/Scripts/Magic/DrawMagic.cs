using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class DrawMagic : MonoBehaviour
{
    //[SerializeField]
    private List<Magicdot> dots;    // 마법진 그리는데 이용할 점들의 리스트
    public GameObject uiCursur;
    public List<int> firePattern = new() { 0, 3, 4, 0 };
    // 23.12.11 SJB Editted for Prototype

    public List<int> poisonPattern = new() { 3, 4 };
    public List<int> icePattern = new() { 1, 3, 2, 4, 1 };
    public List<int> flyPattern = new() { 0, 1, 2, 3 };
    public List<int> protectPattern = new() { 0, 1, 3, 4, 2, 0 };
    public List<int> invisiblePattern = new() { 1, 3, 4, 2, 1 };

    public List<List<int>> skillPatterns = new List<List<int>>();
    private List<int> myPattern = new List<int>();

    private LineRenderer lineRenderer; // 마법진 그리는데 사용할 라인렌더러

    private int preDotsId;
    private int dotsIndex = 0;
    public InputActionReference UIInputAction;  // 인풋 액션 중 UI 클릭 인풋
    private bool isPressing = false;

    public GameObject FireSkill;

    private Damage skillParent;

    private Transform playerCamera;     // UI Rotate에 사용할 플레이어 카메라
    private float playerYRotation;      // UI Rotate에 사용할 플레이어 Y 회전축
    private Vector3 point;               // Line Renderer 회전에 사용할 점 포지션
    private List<Transform> dotsTransform;    // 점들의 트랜스폼 리스트

    public MagicSpawner magicSpawner;

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
        if(lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }      
        lineRenderer.positionCount = 0; // 초기에는 아무 점도 없으므로 0으로 설정
    }

    private void OnEnable()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
        }
    }

    void Update()
    {
        if (UIInputAction != null && UIInputAction.action.ReadValue<float>() == 1f)
        {
            isPressing = true;
        }
        else if (isPressing == true)
        {
            ClearMagicUi();
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
                dot_.enterParticle.Play();
            }
            else if (dot_.id != preDotsId)
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(dotsIndex, dot_.transform.position);
                dotsTransform.Add(dot_.transform);
                dotsIndex += 1;
                myPattern.Add(dot_.id);
                preDotsId = dot_.id;
                dot_.enterParticle.Play();
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
                dot_.enterParticle.Play();
            }
        }
    }

    public void OnMouseUpDot(Magicdot dot_)
    {

    }

    private void AddSkillList()
    {
        //리스트로 그리고 -> 순서를 스트링으로 바꿔서
        //스트링을 기반으로 class 반환
        #region Legacy
        //skillPatterns.Add(firePattern);
        //skillPatterns.Add(posionPattern);
        //skillPatterns.Add(icePattern);
        //skillPatterns.Add(flyPattern);
        //skillPatterns.Add(protectPattern);
        //skillPatterns.Add(invisiblePattern);




        //Damage damage;
        //NoDamage noDamage;

        ////List<string> temp = new List<string>();
        //foreach (var key in CSVConverter_JHW.Instance.skillDic.Keys) 
        //{
        //    temp.Add(key);
        //}

        //for (int i = 0; i < temp.Count; i++) 
        //{
        //    damage = CSVConverter_JHW.Instance.skillDic[temp[i]] as Damage;
        //    if(damage != null)
        //    {
        //        skillPatterns.Add(damage.skillPattern);
        //    }
        //    else
        //    {
        //        noDamage = CSVConverter_JHW.Instance.skillDic[temp[i]] as NoDamage;
        //        if (noDamage != null)
        //        {
        //            skillPatterns.Add(noDamage.skillPattern);
        //        }
        //    }         
        //}

        //skillPatterns = skillPatterns.Distinct().ToList();
        //foreach (SkillParent skill in CSVConverter_JHW.Instance.skillDic.Values)
        //{
        //    Damage damageSkill = skill as Damage;
        //    if (damageSkill != null)
        //    {
        //        // Damage 클래스에 대한 처리
        //        StringBuilder sb = new StringBuilder();
        //        foreach (int temp in damageSkill.skillPattern)
        //        {
        //            sb.Append(temp);
        //            sb.Append(" ");
        //        }
        //        Debug.Log($"List 내부값 : {sb}");
        //        skillPatterns.Add(damageSkill.skillPattern);
        //    }
        //    else
        //    {
        //        NoDamage noDamageSkill = skill as NoDamage;
        //        if (noDamageSkill != null)
        //        {
        //            // NoDamage 클래스에 대한 처리
        //            StringBuilder sb1 = new StringBuilder();
        //            foreach (int temp in noDamageSkill.skillPattern)
        //            {
        //                sb1.Append(temp);
        //                sb1.Append(" ");
        //            }
        //            Debug.Log($"No_List 내부값 : {sb1}");
        //            skillPatterns.Add(noDamageSkill.skillPattern);
        //        }
        //    }
        //}
        #endregion
    }    
    public void CompareSkillPattern(List<int> myPattern_)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < myPattern_.Count; i++)
        {
            stringBuilder.Append(myPattern_[i]);
        }

        string stringPattern = stringBuilder.ToString();
        #region 디버깅 로그
        //Debug.Log(stringPattern);
        //Debug.Log(stringPattern.Length);
        //string s = CSVConverter_JHW.Instance.tempString;
        //Debug.Log(s.Length);
        //Debug.Log(s);



        //Debug.Log(stringPattern.Equals(CSVConverter_JHW.Instance.tempString.Trim()));
        #endregion
        if (CSVConverter_JHW.Instance.patternDic.ContainsKey(stringPattern))
        {
            if (CSVConverter_JHW.Instance.patternDic[stringPattern] != null)
            {
                Debug.Log("들어왔다 준보");

                GameObject magicObject = magicSpawner.ReturnMagic(CSVConverter_JHW.Instance.patternDic[stringPattern].ID);
                magicObject.SetActive(true);
                magicObject.transform.SetParent(transform.parent);
                magicObject.transform.localPosition = transform.localPosition;               
                magicObject.GetComponent<MagicBase>().magicUi = gameObject;
                stringPattern = default(string);
                lineRenderer.enabled = false;
                gameObject.SetActive(false);
            }
        }
        else
        {
            stringPattern = default(string);
        }
        // TODO : 스킬 분류 구조를 제작해서 패턴리스트와 스킬을 연동하고, 패턴마다 다른스킬을 사용할 수 있도록 해야한다.

    }

    private void UpdateLine()
    {
        for(int i = 0; i < dotsTransform.Count; i++)
        {
            lineRenderer.SetPosition(i, dotsTransform[i].position);
        }
    }

    // 패턴 리스트, 라인렌더러 그림, 버튼 눌림등을 초기화
    private void ClearMagicUi()
    {
        isPressing = false;
        CompareSkillPattern(myPattern);
        myPattern.Clear();
        dotsTransform.Clear();
        lineRenderer.positionCount = 0;
        dotsIndex = 0;
    }
}

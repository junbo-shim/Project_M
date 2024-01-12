using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public enum Type
{
    SLICEOBJ,
    Move,
    SPAWNOBJ,
    SPINOBJ,
    HIT,
    ROTATELOOPS,
    LOOK
        
}

public class InteractionObjScripts : MonoBehaviour
{

    [HideInInspector] public Type objType; //오브젝트 타입 
    [HideInInspector] public int intValue1; // 인트 값1
    [HideInInspector] public int intValue2; // 인트 값2
    [HideInInspector] public int intValue3; // 인트 값3  
    [HideInInspector] public float floatValue1; //실수 값 
    [HideInInspector] public float floatValue2; //실수 값 
    [HideInInspector] public float floatValue3; //실수 값 
    [HideInInspector] public string strValue; // 문자열 값    
    [HideInInspector] public bool movement; // 동작여부
    [HideInInspector] public bool isHit; // 충돌여부
    [HideInInspector] public List<Mesh> Objmeshes; // 동작여부
    [HideInInspector] public bool isHitEventEnabled; // 히트 이벤트 사용여부
    [HideInInspector] public bool onMoving; // 이동 변수이름
    [HideInInspector] public LayerMask layerMask; // 레이어 선택용
    private WaitForSeconds setSeconds; // 반복 속도용    
    private WaitForSeconds defultSeconds; // 기본시간초
    private int OrgValue1; // intValue1원래 값 저장용
    private int OrgValue2; // intValue2원래 값 저장용
    private int OrgValue3; // intValue3원래 값 저장용
    private Rigidbody rb;// rigidbody 용
    private Sequence sequence; // DoTeewing
    [HideInInspector] public Vector3 planeNormal = Vector3.up; // 평면의 법선 벡터
    [HideInInspector] public Vector3 pointOnPlane = Vector3.zero; // 평면 위의 한 점


    [System.Serializable] public class BoolEvent : UnityEvent<bool> { } // onMoving 변경 이벤트
    public BoolEvent onMovementChanged;
    [System.Serializable] public class HitEvent : UnityEvent<bool> { } //isColliding 작동시 이벤트
    public HitEvent onHitEvent;

    [System.Serializable] public class CompletedEvent : UnityEvent<bool> { } //isColliding 작동시 이벤트
    public CompletedEvent onCompleted;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Objmeshes = new List<Mesh> ();
        OrgValue1 = intValue1;
        OrgValue2 = intValue2;
        OrgValue3 = intValue3;
        defultSeconds = new WaitForSeconds(0.1f);
        setSeconds = new WaitForSeconds(floatValue1);
        ObjEvent();
       
   
    }

    public void ObjEvent()
    {
        switch (objType)
        {
            case Type.SLICEOBJ:
                SplitObject();
                break;

            case Type.Move:
                StartCoroutine(MoveObj());
                break;

            case Type.SPAWNOBJ:

                break;

            case Type.SPINOBJ:
                setSeconds = new WaitForSeconds(0.01f);
                StartCoroutine(OBJRotate());
                break;
            case Type.HIT:
                break;
            case Type.ROTATELOOPS:
           
                RotateLoops();
                break;
            case Type.LOOK:

                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (objType == Type.LOOK)
        {
            Debug.Log(other.name);
            Debug.Log((1 << layerMask.value) == (1 << other.gameObject.layer));
            if ((1 << layerMask.value) == (1 << other.gameObject.layer))
            {
                transform.LookAt(other.transform.position);
            }

        }
    
    }

    private void RotateLoops()
    {
        sequence = DOTween.Sequence();
        // 우측으로 회전
        sequence.Append(transform.DOLocalRotate(new Vector3(floatValue1, 45f, 0), 0.3f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            // 좌측으로 회전
            transform.DOLocalRotate(new Vector3(floatValue1, -45f, 0), 0.3f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                RotateLoopsStopLoop();
            });
        }));
;
    }
    private void RotateLoopsStopLoop()
    {
     
   
        RotateLoops();
        Debug.Log("작동");

    }
    private void SplitObject()
    {

        Plane cuttingPlane = new Plane(planeNormal, pointOnPlane);

        // 평면의 방정식
        Debug.Log("평면 방정식: " + cuttingPlane.ToString());

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            Mesh originalMesh = meshFilter.sharedMesh;

            // Mesh의 정점과 삼각형을 가져옴
            Vector3[] vertices = originalMesh.vertices;
            int[] triangles = originalMesh.triangles;
            int splitIndex = triangles.Length / intValue1;
          
            List<Mesh> Objmeshes = new List<Mesh>();

            // 분할을 위해 Mesh를 두 개로 나누기
            for (int i = 0; i < intValue1; i++)
            {
                Mesh mesh = new Mesh();
                Objmeshes.Add(mesh);
            }

            // Mesh를 분할
            for (int i = 0; i < intValue1; i++)
            {
              
                int[] trianglesList = new int[splitIndex *3]; // 예상 크기로 배열 초기화
            
             
                System.Array.Copy(triangles, i * splitIndex, trianglesList, 0, splitIndex);

                // 정점과 삼각형 설정
                Objmeshes[i].vertices = vertices;
                Objmeshes[i].triangles = trianglesList;

                // 메시 갱신
                Objmeshes[i].RecalculateNormals();
                Objmeshes[i].RecalculateBounds();
            }


            for (int i = 0; i < intValue1; i++)
            {
                // 분할된 Mesh를 적용할 GameObjects 생성
                GameObject Splitobject = new GameObject("SplitObject1");
                Splitobject.AddComponent<MeshFilter>().mesh = Objmeshes[i];
                Splitobject.AddComponent<MeshRenderer>().sharedMaterial = meshFilter.GetComponent<MeshRenderer>().sharedMaterial;
            //    Splitobject.AddComponent<Rigidbody>().useGravity =true;
    
                Splitobject.transform.position = transform.position;
            }

        }
    }
    private IEnumerator OBJRotate() // 오브젝트 회전
    {

        while (movement)
        {
            transform.Rotate(intValue1, intValue2, intValue3);
            if(onMoving)
            {
                if(rb != null)
                {
                    rb.AddForce(Vector3.forward * floatValue1, ForceMode.Impulse);
                }              
            }
            yield return setSeconds;
        }
        StartCoroutine(ToggleMovement());

    }

    private IEnumerator MoveObj() // 오브젝트 회전
    {

        while (movement)
        {

            Vector3 diagonalDirection = new Vector3(intValue1, intValue2, intValue3).normalized;// 대각선방향의 정규화 백터
            transform.Translate(diagonalDirection * floatValue1 * Time.deltaTime);
            yield return defultSeconds;
        }

    }

    private IEnumerator ToggleMovement() // 움직임 변경 자연스럽게
    {

        if (!movement)
        {
            while (intValue1 >= 0)
            {
                transform.Rotate(intValue1, intValue2, intValue3);
                if (intValue1 > 0)
                {
                    intValue1 = intValue1 - 1;
                }
                if (intValue2 > 0)
                {
                    intValue2 = intValue2 - 1;
                }
                if (intValue3 > 0)
                {
                    intValue3 = intValue3 - 1;
                }

                yield return setSeconds;
            }

            intValue1 = OrgValue1;
            intValue2 = OrgValue2;
            intValue3 = OrgValue3;
        }


    }
    public void ChangeMovement() //movement 값 변경시 작동함수
    {
        onMovementChanged?.Invoke(movement);
    }

    public void ChangeHit() // hit 값 변경시 작동함수
    {
        onHitEvent?.Invoke(isHit);
    }

    public void OnDisable()// 종료시 켜짐
    {
        if (movement)// 동작 off시 작동안함
        {
            if (objType == Type.SPAWNOBJ)// 스폰오브젝트 시 켜짐
            {
                Invoke("SpObjActTrue", OrgValue1);
            }
        }
  
    }

    void SpObjActTrue() // 오브젝트 리스폰
    {
        gameObject.SetActive(true);
    }

    public void SpObjActFalse() // 오브젝트 비활성화
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)// 충돌
    {
        if (objType == Type.HIT)
        {
            if ((1 << layerMask.value) == (1 << other.gameObject.layer))
            {
                if (isHitEventEnabled) // 히트 이벤트 사용여부 체크
                {
                    ChangeHit(); // 충돌시 이벤트
                }
            }
        }
    }


    public void DisableObject()
    {
 
        gameObject.SetActive(false);
    }
}

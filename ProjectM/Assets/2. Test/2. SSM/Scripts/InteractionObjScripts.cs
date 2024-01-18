using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum Type
{
    SLICEOBJ,
    Move,
    SPAWNOBJ,
    SPINOBJ,
    HIT,
    ROTATELOOPS,
    TELEPORT,
    OBJACT

}

public class InteractionObjScripts : MonoBehaviour
{
    [HideInInspector] public bool startEvent;
    [HideInInspector] public Type objType; //오브젝트 타입 
    [HideInInspector] public int intValue1; // 인트 값1
    [HideInInspector] public int intValue2; // 인트 값2
    [HideInInspector] public int intValue3; // 인트 값3  
    [HideInInspector] public int intValue4; // 인트 값3  
    [HideInInspector] public int intValue5; // 인트 값3  
    [HideInInspector] public int intValue6; // 인트 값3  
    [HideInInspector] public float floatValue1; //실수 값 
    [HideInInspector] public float floatValue2; //실수 값 
    [HideInInspector] public float floatValue3; //실수 값 
    [HideInInspector] public string strValue; // 문자열 값    
    [HideInInspector] public bool movement; // 동작여부
    [HideInInspector] public bool isEnable; // 활성화 여부
    [HideInInspector] public bool isDisable; // 비활성화 여부
    [HideInInspector] public bool isHit; // 충돌여부
    [HideInInspector] public bool isInteraction; // 상호작용체크 

    [HideInInspector] public List<Mesh> Objmeshes; // 동작여부
    [HideInInspector] public bool isHitEventEnabled; // 히트 이벤트 사용여부
    [HideInInspector] public bool onMoving; // 이동 변수이름
    [HideInInspector] public LayerMask layerMask; // 레이어 선택용
    [HideInInspector] public LayerMask layerMask2; // 레이어 선택용2
    [HideInInspector] public GameObject gameObject1; // 게임오브젝트

    private WaitForSeconds setSeconds; // 반복 속도용    
    private WaitForSeconds defultSeconds; // 기본시간초
    private int OrgValue1; // intValue1원래 값 저장용
    private int OrgValue2; // intValue2원래 값 저장용
    private int OrgValue3; // intValue3원래 값 저장용
    private Rigidbody rb;// rigidbody 용
    private Sequence sequence; // DoTeewing
    private bool act; //활성화 on off
    private Vector3 orgPos; // 원래 위치 저장용
    [HideInInspector] public Vector3 planeNormal = Vector3.up; // 평면의 법선 벡터
    [HideInInspector] public Vector3 pointOnPlane = Vector3.zero; // 평면 위의 한 점

    [System.Serializable] public class DisableEventList : UnityEvent<bool> { } //isColliding 작동시 이벤트
    public DisableEventList onDisable;

    [System.Serializable] public class EnableEventList : UnityEvent<bool> { } // 작동시 이벤트
    public EnableEventList onEnable;
    [System.Serializable] public class BoolEvent : UnityEvent<bool> { } // onMoving 변경 이벤트
    public BoolEvent onMovementChanged;
    [System.Serializable] public class HitEvent : UnityEvent<bool> { } //isColliding 작동시 이벤트
    public HitEvent onHitEvent;
    [System.Serializable] public class HitEvent2 : UnityEvent<bool> { } //isColliding 작동시 이벤트
    public HitEvent2 onHitEvent2;
    [System.Serializable] public class CompletedEvent : UnityEvent<bool> { } //isColliding 작동시 이벤트
    public CompletedEvent onCompleted;



    [System.Serializable] public class OBjACt : UnityEvent<bool> { }
    public OBjACt objAct;

    private void Start()
    {

        orgPos = transform.position;

        rb = GetComponent<Rigidbody>();
        Objmeshes = new List<Mesh>();
        OrgValue1 = intValue1;
        OrgValue2 = intValue2;
        OrgValue3 = intValue3;
        defultSeconds = new WaitForSeconds(0.1f);
        setSeconds = new WaitForSeconds(floatValue1);
        if (startEvent)
        {
            ObjEvent();
        }



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

            case Type.OBJACT:

                break;
            case Type.TELEPORT:
                break;
        }
    }

    private void OnEnable()
    {
        if (isEnable)
        {
            onEnable?.Invoke(act);

        }
    }
    private void OnDisable()
    {
        if (isDisable)
        {
            onDisable?.Invoke(act);

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

                int[] trianglesList = new int[splitIndex * 3]; // 예상 크기로 배열 초기화


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
            if (onMoving)
            {
                if (rb != null)
                {
                    rb.AddForce(Vector3.left * floatValue1, ForceMode.Impulse);
                }
            }
            yield return setSeconds;
        }
        StartCoroutine(ToggleMovement());

    }

    public void EventMove() // 외부 작동용 
    {
        StartCoroutine(MoveObj());
    }
    private IEnumerator MoveObj() // 오브젝트 회전
    {

        while (floatValue2 > floatValue3)
        {
            floatValue3 += 0.1f;
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

    public void ChangeHit2() // hit 값 변경시 작동함수
    {
        onHitEvent2?.Invoke(isHit);
    }
    public void ReturnToOriginalPosition() // 원래 위치로 돌아가는 함수
    {

        transform.position = orgPos;
    }

    public void RespawnObject()
    {

        Invoke("SpObjActTrue", intValue4);
    }

    public void ObjActOn()//gameObject1 활성화
    {
        gameObject1.SetActive(true);
        Debug.Log(intValue6);
        if (intValue6 != 0)
        {
            QuestMananger.instance.intetactionQuestAdd(intValue6.ToString()); //상호작용 id 상호작용퀘스트에 추가
            ParticleSystem particleSystem = gameObject1.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }


    }
    public void ObjActOff()//gameObject1 비활성화
    {
        if (Type.OBJACT == objType)
        {
            gameObject1.SetActive(false);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }

    }
    public void SelfObjOn()
    {
        gameObject.SetActive(true);
        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

    }
    private void SpObjActTrue() // 오브젝트 리스폰
    {

        gameObject.SetActive(true);
    }

    public void SpObjActFalse() // 오브젝트 비활성화
    {

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)// 충돌
    {
        if (isHitEventEnabled) // 히트 이벤트 사용여부 체크
        {

            if ((1 << layerMask.value) == (1 << other.gameObject.layer))
            {
                if (objType == Type.TELEPORT)
                {
                    other.transform.parent.position = gameObject1.transform.position;
                }
                else
                {
                    ChangeHit(); // 충돌시 이벤트
                }


            }
            if (layerMask2 != 0)
            {

                if ((1 << layerMask2.value) == (1 << other.gameObject.layer))
                {

                    CharacterController characterController = other.transform.parent.GetComponent<CharacterController>();

                    if (characterController != null)
                    {
                        ChangeHit2();

                        characterController.Move(Vector3.back * floatValue2 * Time.deltaTime);
                    }

                }
            }

        }
    }
    public void OnRigdbody()
    {
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
    public void MovementChang() //movement 작동 
    {
        movement = !movement;
        if (intValue6 != 0)
        {
            QuestMananger.instance.intetactionQuestAdd(intValue6.ToString());

        }
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}

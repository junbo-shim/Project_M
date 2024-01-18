using BNG;
using UnityEngine;
using UnityEngine.InputSystem;

public class NpcAction : NpcActionBase
{
    public NPCTack npcTack; // npc대화 스크립트
    private float playerDis = 0;// 플레이어와 npc거리 저장용
    private bool retalkChk; // 다시 말걸경우 체크
    [SerializeField] private float NPCTalkDis; // 플레이어와 npc 거리
    [SerializeField] private GameObject TalkCanvas; // 대화 캔버스
    [SerializeField] private GameObject ChoicsObj; // 선택지 
    [SerializeField] private GameObject icon; // npc아이콘;
    public int Talki = 0; // 대화가 몇번째인지 (50글자씩 자른대화)
    [SerializeField] private bool isSave; // save npc 여부
    private SaveNpc saveNpc;
    public bool ragdoll = false; // 레그돌 상태 여부

    [Tooltip("Unity Input Action used to move the player up")]
    public InputActionReference PlayerUpAction;


    public void Start()
    {
        Collider[] colliders = transform.parent.GetComponentsInChildren<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), colliders[i]);
            // Physics.IgnoreCollision(colliders[i], trigger);
        }

        saveNpc = GetComponent<SaveNpc>();
        retalkChk = false;
        npcTack = GetComponent<NPCTack>();

    }


    public void OnTriggerStay(Collider other)
    {

        if (other.tag.Equals("Player")) // 플레이어에게 대사 보여주기 바라보기
        {
            // 조건 추가 

            if (!ragdoll)
            {

                transform.LookAt(other.transform);
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hitInfo; // 레이캐스트에 의한 충돌 정보를 저장할 변수
                playerDis = Vector3.Distance(other.transform.position, transform.position);

                if (playerDis < NPCTalkDis)
                {
                    if (!isSave)
                    {
                        if (Talki != -2)
                        {
                            if (!npcTack.textimage.gameObject.activeSelf)
                            {
                                npcTack.IconOn(); // 아이콘표시
                            }


                            else
                            {
                                npcTack.IconOff();
                            }
                    
                          
                        }
                    
                        if (Physics.Raycast(ray, out hitInfo, NPCTalkDis))
                        {
                            //animator.enabled = false;

                            if (hitInfo.collider.CompareTag("Player"))
                            {

                                NPCTalkStart();// 대화로직
                            }


                        }
                    }
                    else if (isSave)
                    {
                        if (!icon.activeSelf)
                        {
                            icon.SetActive(true);
                        }

                        if (Physics.Raycast(ray, out hitInfo, NPCTalkDis))
                        {
                            if (hitInfo.collider.CompareTag("Player"))
                            {
                                if (PlayerUpAction.action.ReadValue<float>() == 1)
                                {

                                    if (ClickBool == false)
                                    {

                                        BoolChange();
                                        saveNpc.TalkChange();
                                    }
                                }
                            }
                        }
                    }

                }
                else if (playerDis > NPCTalkDis) //거리 멀어지면 종료
                {
                    if (!isSave)
                    {

                        npcTack.TalkExit();//대화창off
                    }
                    else
                    {
                        if (TalkCanvas.activeSelf)
                        {

                            TalkCanvas.SetActive(false);
                        }
                        if (!ChoicsObj.activeSelf)
                        {
                            ChoicsObj.SetActive(true);
                        }
                    }


                }
                NpcLook(other.gameObject);//플레이어 처다보기
            }


        }


    }


    public void TalkiClear() // 대화 선택지 클릭시
    {
        Talki = -1;
    }
    public void TalkiZero() // 대화 처음으로
    {
        Talki = 0;
    }
    public void TalkiEnd() // 대화 종료
    {
        Talki = -2;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player")) // npc 대기상태로
        {
            playerDis = 0;


            NpcLookClear(); // npc처다보기 종료
        }
    }
    private void NPCTalkStart() // 대화시작
    {

        if (Talki != -2)
        {
            if (PlayerUpAction.action.ReadValue<float>() == 1)
            {

                if (ClickBool == false)
                {

                    BoolChange();
             
                    Talki = npcTack.WordChange(Talki);
                  
                }

            }
            if (!npcTack.textimage.gameObject.activeSelf)
            {
                Talki = 0;
            }
        }
        else if (Talki == -2)
        {
            if (PlayerUpAction.action.ReadValue<float>() == 1)
            {
                if (ClickBool == false)
                {

                    BoolChange();
                    if (!npcTack.textimage.gameObject.activeSelf)
                    {
                    
                        npcTack.TalkOn();//대화창on
                        npcTack.IconOff();
                    }
                  
                }
            }
        }


    }


    #region npc처다보는 함수
    private void NpcLook(GameObject gameObject)
    {

        Vector3 vector3 = new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z);
        transform.parent.LookAt(vector3); // 플레이어 처다보기
        Vector3 eulerAngles = transform.parent.eulerAngles;
        eulerAngles.x = 0f;
        transform.parent.eulerAngles = eulerAngles;
    }
    #endregion
    #region npc처다보기 종료 함수
    private void NpcLookClear()
    {
        if (!isSave)
        {
            Talki = 0; // npc 워드 초기화
            npcTack.ExitTalk();

            npcTack.TalkExit();//대화창off
        }
        if (icon != null)
        {
            if (icon.activeSelf)
            {
                icon.SetActive(false);
                saveNpc.ExitTalk();
            }

        }
    }
    #endregion
}

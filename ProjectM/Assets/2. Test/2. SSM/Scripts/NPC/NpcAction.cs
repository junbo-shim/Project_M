using UnityEngine;
using UnityEngine.InputSystem;

public class NpcAction : NpcActionBase
{
    private NPCTack npcTack; // npc대화 스크립트
    private float playerDis = 0;// 플레이어와 npc거리 저장용
    public bool tackChk; // 대화창 한번 만사용하도록 쓰는 변수
    private bool retalkChk; // 다시 말걸경우 체크
    [SerializeField] private float NPCTalkDis; // 플레이어와 npc 거리
    public int Talki = 0; // 대화가 몇번째인지 (50글자씩 자른대화)

    [Tooltip("Unity Input Action used to move the player up")]
    public InputActionReference PlayerUpAction;
    public void Start()
    {
        tackChk = false;
        retalkChk = false;
        npcTack = GetComponent<NPCTack>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player")) // 플레이어에게 대사 보여주기 바라보기
        {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo; // 레이캐스트에 의한 충돌 정보를 저장할 변수
            playerDis = Vector3.Distance(other.transform.position, transform.position);
            
            if (playerDis < NPCTalkDis)
            {
                if (!tackChk && Talki != -2)
                {
                    npcTack.IconOn(); // 아이콘표시
                }
                if (Physics.Raycast(ray, out hitInfo, NPCTalkDis))
                {

                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        NPCTalkStart();// 대화로직
                    }


                }
            }
            else if (playerDis > NPCTalkDis) //거리 멀어지면 종ㄽ
            {
                npcTack.TalkExit();//대화창off
            }
            NpcLook(other.gameObject);//플레이어 처다보기
        }


    }
    public void TalkiClear()
    {
        Talki = -1;
    }
    public void TalkiEnd()
    {
        Talki = -2;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player")) // npc 대기상태로
        {
            playerDis = 0;
            tackChk = false;

            NpcLookClear(); // npc처다보기 종료
        }
    }
    private void NPCTalkStart() // 대화시작
    {
       
        if ( Talki != -2)
        {
            if (PlayerUpAction.action.ReadValue<float>() == 1)
            {
                if (ClickBool == false)
                {
                    Debug.Log("????");
                    BoolChange();
                    Talki = npcTack.WordChange(Talki);
                    
                }
              
            }
            if (!npcTack.textimage.gameObject.activeSelf)
            {
                tackChk = false;
            }
        }
        else if(Talki == -2)
        {
            if (PlayerUpAction.action.ReadValue<float>() == 1)
            {
                if (ClickBool == false)
                {

                    BoolChange();
                    if (!npcTack.textimage.gameObject.activeSelf)
                    {
                        Debug.Log("-2");
                        npcTack.TalkOn();//대화창on

                    }
                    else
                    {
                        npcTack.TalkOff();
                    }
                }
            }
        }
     

    }


    private void TalkBoolChange()
    {
        retalkChk = false;
        tackChk = false;
    }
    #region npc처다보는 함수
    private void NpcLook(GameObject gameObject)
    {
        
        Vector3 vector3 = new Vector3(gameObject.transform.position.x,transform.position.y, gameObject.transform.position.z);
        transform.parent.LookAt(vector3); // 플레이어 처다보기
        Vector3 eulerAngles = transform.parent.eulerAngles;
        eulerAngles.x = 0f;
        transform.parent.eulerAngles = eulerAngles;
    }
    #endregion
    #region npc처다보기 종료 함수
    private void NpcLookClear()
    {
        Talki = 0; // npc 워드 초기화
        npcTack.ExitTalk();
        npcTack.WordChange(Talki);// text교체
        npcTack.TalkExit();//대화창off
    }
    #endregion
}

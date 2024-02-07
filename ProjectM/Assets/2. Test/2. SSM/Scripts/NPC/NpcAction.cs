using BNG;
using UnityEngine;
using UnityEngine.InputSystem;
public enum NpcState
{
    IDLE,
    TALk,
    COMPLET,
    RAGDOLL
}
public class NpcAction : NpcActionBase
{


    public NpcState _npcState;

    [HideInInspector]public NPCTalk NPCTalk; // npc대화 스크립트
    private float playerDis;// 플레이어와 npc거리 저장용
    [SerializeField] private float NPCTalkDis; // 플레이어와 npc 설정거리
    public int Talki = 0; // 대화가 몇번째인지 (50글자씩 자른대화)


    [Tooltip("Unity Input Action used to move the player up")]
    public InputActionReference PlayerUpAction;


    public void Start()
    {
        _npcState = NpcState.IDLE;
        Collider[] colliders = transform.parent.GetComponentsInChildren<Collider>(); // 인식안할 콜라이더 추가

        for (int i = 0; i < colliders.Length; i++)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), colliders[i]);
            // Physics.IgnoreCollision(colliders[i], trigger);
        }

        //   saveNpc = GetComponent<SaveNpc>(); // 이곳에서 saveNPC추가
        NPCTalk = GetComponent<NPCTalk>();

    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player")) // 플레이어에게 대사 보여주기 바라보기
        {   
            if (playerDis < NPCTalkDis)
            {
                switch (_npcState)
                {
                    case NpcState.TALk:
                        NPCTalkStart(other);// 대화로직                            
                        break;

                    case NpcState.IDLE:
                        NPCTalk.IconOn(); // 아이콘 활성화
                        NPCTalkStart(other);// 대화로직 
                        break;

                    case NpcState.RAGDOLL:
                        NPCTalk.IconOff();
                        break;
                }

            }
        }
    }

    public void NPCStateChange(NpcState newState) // 상태 변경
    {
        _npcState = newState;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player")) // npc 대기상태로
        {
            playerDis = 0;

            NPCStateChange(NpcState.IDLE);
            NpcLookClear(); // npc처다보기 종료
        }
    }
    private void NPCTalkStart(Collider other) // 대화시작
    {
        NpcLook(other.gameObject); // 대상 바라보기 
        if (PlayerUpAction.action.ReadValue<float>() == 1)
        {          
            if (ClickBool == false)
            {
                BoolChange();              
                Talki = NPCTalk.WordChange(Talki);
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

    private void NpcLookClear()
    {

        Talki = 0; // npc 워드 초기화
        NPCTalk.ExitTalk();

        NPCTalk.TalkExit();//대화창off

     
    }
    #endregion
}

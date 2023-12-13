using UnityEngine;

public class NpcAction : MonoBehaviour
{
    private NPCTack nPCTack; // npc대화 스크립트
    private float playerDis = 0;// 플레이어와 npc거리 저장용
    public bool tackChk = false; // 대화창 한번 만사용하도록 쓰는 변수
    [SerializeField] private float NPCTalkDis;
    int i = 0; // 대화가 몇번째인지 (50글자씩 자른대화)

    public void Start()
    {

        nPCTack = GetComponent<NPCTack>();
    }

    public void OnTriggerStay(Collider other)
    {

        if (other.tag.Equals("Player")) // 플레이어에게 대사 보여주기 바라보기
        {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo; // 레이캐스트에 의한 충돌 정보를 저장할 변수
            playerDis = Vector3.Distance(other.transform.position, transform.position);
            //Debug.DrawRay(ray.origin, ray.direction * NPCTalkDis, Color.green); 레이
            if (playerDis < NPCTalkDis)
            {
                nPCTack.IconOn();
                if (Physics.Raycast(ray, out hitInfo, NPCTalkDis))
                {
                    Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);
                    if (hitInfo.collider.gameObject.name.Equals("Player"))
                    {
                        if (!tackChk)
                        {
                          

                            if (Input.GetKeyDown(KeyCode.Z))
                            {

                                tackChk = true;
                                nPCTack.TalkOn();//대화창on
                            }
                        }
                    }
                  

                }
            }
            else if(playerDis > NPCTalkDis)
            {
                nPCTack.TalkExit();//대화창off
            }
            NpcLook(other.gameObject);//플레이어 처다보기
        }

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


    #region npc처다보는 함수
    private void NpcLook(GameObject gameObject)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            i = nPCTack.wordChange(i); // text교체
        }
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
        i = 0; // npc 워드 초기화
        nPCTack.wordChange(i);// text교체
        nPCTack.TalkExit();//대화창off
    }
    #endregion
}

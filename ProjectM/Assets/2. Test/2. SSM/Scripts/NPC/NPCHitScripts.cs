using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;

public class NPCHitScripts : MonoBehaviour
{
    private Rigidbody rb; // 자신의rigbody 
    [SerializeField] private GameObject npcObj; //최상위 부모오브젝트 
    public NpcAction npcAction; // npcAction 저장용
    private Animator animator; // 최상위 부모Animator
    public bool isGround = true; // 땅접촉여부 판단
    public bool isGrap = false;  // 잡혔는지 여부
    Rigidbody[] parntRb;

    public void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        npcObj = ReParntOBJ(gameObject);
        parntRb = npcObj.transform.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < parntRb.Length; i++)
        {
            parntRb[i].isKinematic = true;
        }

        animator = npcObj.GetComponent<Animator>();
        npcAction = npcObj.transform.Find("NpcAction").GetComponent<NpcAction>();

    }

    public void OnCollisionEnter(Collision collision) // 땅에 접촉시
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            if (!isGround && !isGrap)
            {
                isGround = true;
                Invoke("ragdollOff", 5f);
            }

        }
    }
    private void OnTriggerEnter(Collider other) // 공격 접촉시
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerATK"))
        {
            if (npcAction._npcState != NpcState.RAGDOLL)
            {
                isGround = false;
                ragdollOn();
            }

            rb.AddForce(new Vector3(0, 200f, 0), ForceMode.Impulse);
        }
    }
    public void GrapChange()// isGrap 의 false true 전환
    {
     
        isGrap = isGrap == false ? true : false; // isGrap 의 false true 전환
        if (!isGrap)
        {
            isGround = true;
            Invoke("ragdollOff", 5f);
        }
        else
        {
            isGround = false;
            ragdollOn();
        }

       
    }

    public void ragdollOn() // 렉돌상태on
    {
        for (int i = 0; i < parntRb.Length; i++)
        {
            parntRb[i].isKinematic = false;
        }
        animator.enabled = false;
        npcAction.NPCStateChange(NpcState.RAGDOLL);
        npcAction.NPCTalk.TalkOff();
        if (isGround)
        {
            Invoke("ragdollOff", 5f);
        }
    }

    public void ragdollOff() // 렉돌상태로off
    {
        if (!isGround || isGrap)
        {
            Invoke("ragdollOff", 5f);
            return;
        }
        for (int i = 0; i < parntRb.Length; i++) // 박다통과현상으로 인해 물리연산 처리 안하려고 사용
        {
            parntRb[i].isKinematic = true;
        }

       
        animator.enabled = true;
        npcAction.NPCStateChange(NpcState.IDLE);
        return;
    }

    private GameObject ReParntOBJ(GameObject targetObject) // 부모함수 찾기 재귀
    {
        if (targetObject.transform.parent != null && !targetObject.transform.name.Equals("NPC_Save"))
        {
            return ReParntOBJ(targetObject.transform.parent.gameObject);
        }
        else
        {
            return targetObject;
        }
    }
}

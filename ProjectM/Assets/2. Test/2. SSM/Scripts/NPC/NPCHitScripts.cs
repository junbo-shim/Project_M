using Oculus.Interaction;
using UnityEngine;

public class NPCHitScripts : MonoBehaviour
{
    private Rigidbody rb; // 자신의rigbody 
    [SerializeField] private GameObject npcObj; //최상위 부모오브젝트 
    public NpcAction npcAction; // npcAction 저장용
    private Animator animator; // 최상위 부모Animator
    public bool isGround = true; // 땅접촉여부 판단
    public bool isGrap = false;  // 잡혔는지 여부

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        npcObj = ReParntOBJ(gameObject);
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
            if (!npcAction.ragdoll)
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
        animator.enabled = false;
        npcAction.ragdoll = true;
        npcAction.npcTack.TalkOff();
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
        animator.enabled = true;
        npcAction.ragdoll = false;
        return;
    }

    private GameObject ReParntOBJ(GameObject targetObject) // 부모함수 찾기 재귀
    {
        if (targetObject.transform.parent != null)
        {
            return ReParntOBJ(targetObject.transform.parent.gameObject);
        }
        else
        {
            return targetObject;
        }
    }
}

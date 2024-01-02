using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPoison : SkillAction
{
    private Ray ray;
    private RaycastHit hit;
    private Vector3 targetDirection;
    private Quaternion targetRotation;


    private float duration;         // 스킬 지속시간   
    public LayerMask collideLayer;  // 충돌검사를 할 레이어

    private Damage poisonInfo;

    // Start is called before the first frame update
    void Start()
    {
        poisonInfo = ReturnInfo("Poison") as Damage;
        duration = poisonInfo.Value1;
        statusEffId = poisonInfo.Value2;
        CheckSkill();      // 스킬 분류 확인
        StartCoroutine(DestroyPoison());
    }

    // Update is called once per frame
    void Update()
    {       
        ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if((collideLayer & (1 << other.gameObject.layer)) != 0)
        {
            // TODO : 몬스터에게 데미지를 주는 내용 작성
        }
    }

    private IEnumerator DestroyPoison()
    {
        yield return new WaitForSecondsRealtime(duration);

        if (this != null)
        {
            Destroy(gameObject);
        }
    }

}

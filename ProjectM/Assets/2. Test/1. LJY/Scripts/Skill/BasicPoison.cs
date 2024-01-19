using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPoison : SkillAction
{
    private Ray ray;
    private RaycastHit hit;
    private Vector3 targetDirection;
    private Quaternion targetRotation;
    public BookScript book;
    public Vector3 destination;  // 발사할 목표 지점


    private float duration;         // 스킬 지속시간   

    private Damage poisonInfo;

    // Start is called before the first frame update
    void Start()
    {
        book = transform.parent.GetComponent<BookScript>();
        destination = book.target;
        transform.SetParent(null);

        transform.position = destination;
        transform.position += transform.up * 1.0f;

        poisonInfo = ReturnInfo("Poison") as Damage;
        duration = poisonInfo.Value1;
        statusEffId = poisonInfo.Value2;
        CheckSkill();      // 스킬 분류 확인
        StartCoroutine(DestroyPoison());
    }

    // Update is called once per frame
    void Update()
    {
        if(poisonInfo != null)
        {
            ray = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }
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

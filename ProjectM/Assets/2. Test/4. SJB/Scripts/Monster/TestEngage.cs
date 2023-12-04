using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEngage : MonoBehaviour
{
    private CharacterController control;
    private float speed;
    private float radius;

    private bool isOnState;
    private bool isAttacking;

    public GameObject target;


    private void Start()
    {
        control = GetComponent<CharacterController>();
        speed = 1f;
        radius = 10f;
        isOnState = false;
        isAttacking = false;


        isOnState = true;
        StartCoroutine(EngageRoutine());
        Debug.Log("상태시작");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            isOnState = false;
            StopCoroutine(EngageRoutine());
            Debug.Log("상태종료");
        }
    }


    private IEnumerator EngageRoutine() 
    {
        while (isOnState == true) 
        {
            Debug.Log("?");
            yield return StartCoroutine(Move());
            yield return StartCoroutine(CheckAttack());
        }
    }


    private IEnumerator Move() 
    {
        CheckTarget();

        while (Vector3.Distance(target.transform.position, gameObject.transform.position) > 3f) 
        {
            yield return null;
            transform.LookAt(target.transform.position + new Vector3(0f, target.transform.position.y, 0f));
            control.Move((target.transform.position - gameObject.transform.position) * speed * Time.deltaTime);
        }
    }

    private IEnumerator CheckAttack()
    {
        CheckTarget();

        if (target == null) 
        {
            Debug.LogWarning("null");
            yield return StartCoroutine(WaitForTarget());
        }
        else if (target != null) 
        {
            Debug.LogWarning("Attack 시작");
            yield return StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack() 
    {
        Debug.Log("true");
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("false");
        target = default;
    }

    private IEnumerator WaitForTarget()
    {
        int i = 0;

        while (i < 5)
        {
            yield return new WaitForSecondsRealtime(1f);
            CheckTarget();

            if (target != null)
            {
                StopAllCoroutines();
                StartCoroutine(EngageRoutine());
            }

            Debug.Log("Now :" + i);
            i++;
            Debug.Log("Next :" + i);
        }

        if (target == null) 
        {
            Debug.Log("목표 상실, 정찰로 변경");
            StopAllCoroutines();
        }
    }

    private void CheckTarget() 
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius, LayerMask.GetMask("Player"));

        Debug.LogError(colliders.Length);

        foreach (var collider in colliders)
        {
            Debug.LogError(collider.name);

            if (collider.GetComponent<CharacterController>() == true)
            {
                Debug.LogWarning("플레이어 확인" + collider.name);
                target = collider.gameObject;
            }
        }
    }
}

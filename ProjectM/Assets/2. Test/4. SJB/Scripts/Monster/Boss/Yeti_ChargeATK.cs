using BNG;
using System.Collections;
using UnityEngine;

public class Yeti_ChargeATK : MonsterATK
{
    private WaitForSeconds knuckbackTime;
    public BoxCollider hitCollider;

    private CharacterController playerController;
    private int force;

    private void Awake()
    {
        knuckbackTime = new WaitForSeconds(force * 0.05f);
        playerController = GameObject.Find("Player_LJY").transform.Find("PlayerController").GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        damage = monster.GetComponent<Monster>().monsterData.Skill3Damage;
        force = monster.GetComponent<Monster>().monsterData.Force2;
    }

    private void OnDisable()
    {
        StopCoroutine(TurnOffLocomotion());
        StopCoroutine(Knuckback());

        damage = default;
        force = default;
        knuckbackTime = default;
    }

    private void OnTriggerEnter(Collider other_)
    {
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            StartCoroutine(TurnOffLocomotion());
            StartCoroutine(Knuckback());
        }
    }

    private void OnTriggerExit(Collider other_)
    {
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            hitCollider.enabled = false;
        }
    }

    private IEnumerator TurnOffLocomotion()
    {
        playerController.GetComponent<SmoothLocomotion>().enabled = false;

        yield return knuckbackTime;

        playerController.GetComponent<SmoothLocomotion>().enabled = true;
        hitCollider.enabled = true;
    }

    private IEnumerator Knuckback()
    {
        float timer = default;

        while (force * 0.05f > timer)
        {
            yield return null;
            playerController.Move((playerController.transform.position - monster.transform.position).normalized * force * 7f * Time.deltaTime);
            timer += 1f;
        }
    }
}

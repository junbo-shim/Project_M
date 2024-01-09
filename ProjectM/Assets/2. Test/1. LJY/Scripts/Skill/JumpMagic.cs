using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BNG;

public class JumpMagic : MonoBehaviour
{
    private Transform player;   // 플레이어의 트랜스폼
    private SmoothLocomotion smoothLocomotion;  // 이동관련 클래스
    private ParticleSystem particle;    // 점프 마법의 파티클

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        DoJump();
    }

    // Update is called once per frame
    void Update()
    {
        if (!particle.isPlaying && smoothLocomotion.useMagicJump)
        {
            smoothLocomotion.useMagicJump = false;
            gameObject.SetActive(false);
        }
    }

    private void DoJump()
    {
        if(player == null)
        {
            // 이동시킬 플레이어 컨트롤러 트랜스폼 얻어오기
            player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
            particle = GetComponent<ParticleSystem>();
            smoothLocomotion = player.GetComponent<SmoothLocomotion>();
        }
        smoothLocomotion.useMagicJump = true;
    }
}

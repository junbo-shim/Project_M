using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpMagic : MonoBehaviour
{
    private Transform player;   // 플레이어의 트랜스폼
    // Start is called before the first frame update
    void Start()
    {
        // 이동시킬 플레이어 컨트롤러 트랜스폼 얻어오기
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
        Debug.Log(player.name);
    }

    private void OnEnable()
    {
        DoJump();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoJump()
    {
        player.DOLocalMoveY(30f, 0.5f);
    }
}

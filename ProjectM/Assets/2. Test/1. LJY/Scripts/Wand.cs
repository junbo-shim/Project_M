using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class Wand : GrabbableEvents
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnSnapZoneEnter()
    {
        Debug.Log("스냅존 이벤트 발생");
        base.OnSnapZoneEnter();
    }

    public override void OnSnapZoneExit()
    {
        Debug.Log("스냅존 탈출");
        base.OnSnapZoneExit();
    }

    public override void OnRelease()
    {
        // 마법봉 놓았을 시 트리거들 꺼버리기
        MagicBase magicBase = FindAnyObjectByType<MagicBase>();
        magicBase.gameObject.SetActive(false);
        base.OnRelease();
    }
}

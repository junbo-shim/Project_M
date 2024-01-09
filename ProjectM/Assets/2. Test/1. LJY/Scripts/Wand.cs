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
}

using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrabEvent : GrabbableEvents
{
    public MiniMapController mc;
    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTrigger(float triggerValue)
    {
        if(!mc.isacting && triggerValue >= 0.75f)
        {
            if(!isOpened)
            {
                isOpened = true;
                mc.OpenMap();
            }
            else
            {
                isOpened = false;
                mc.CloseMap();
            }        
        }
    }

    public override void OnRelease()
    {
        if (isOpened)
        {
            isOpened = false;
            mc.CloseMap();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BedOBJScripts : NpcActionBase
{
    public InputActionReference PlayerUpAction;
    public void OnTriggerStay(Collider other)
    {
        if (PlayerUpAction.action.ReadValue<float>() == 1)
        {
            if (ClickBool == false)
            {

                BoolChange();

                MapGameManager.instance.BedChange();
            }
        }
    }
}

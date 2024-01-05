using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerHand : MonoBehaviour
{

    public Material normal;
    public Material invisible;

    private SkinnedMeshRenderer smr;

    private void Start()
    {
        smr = GetComponent<SkinnedMeshRenderer>();
    }

    public void ChangeHand(bool isInvisible)
    {
        if (isInvisible)
        {
            smr.material = invisible;
        }
        else
        {
            smr.material = normal;
        }
    }
}


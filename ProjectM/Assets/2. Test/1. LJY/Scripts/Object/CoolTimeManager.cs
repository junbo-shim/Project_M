using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTimeManager : MonoBehaviour
{
    public float scarecrowCooltime = 30f;
    public bool isScarecrowCool;
    public float currentSCCool;

    public float trapCooltime = 5f;
    public bool isTrapCool;
    public float currentTrCool;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public IEnumerator ScarecrowCoolTime()
    {
        isScarecrowCool = true;
        currentSCCool = scarecrowCooltime;
        while (currentSCCool > 0)
        {
            currentSCCool -= Time.deltaTime;
            yield return null;
        }
        isScarecrowCool = false;
    }

    public IEnumerator TrapCoolTime()
    {
        isTrapCool = true;
        currentTrCool = trapCooltime;
        while (currentTrCool > 0)
        {
            currentTrCool -= Time.deltaTime;
            yield return null;
        }
        isTrapCool = false;
    }
}

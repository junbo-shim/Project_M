using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class PlayerScript : MonoBehaviour
{
    private Vector3 beforePos;
    public GameObject playerFootStep;
    public GameObject playerOrb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InputBridge.Instance.YButtonUp)
        {
            if(!MapGameManager.instance.currentState.Equals(DayState.NIGHT))
            {
                if(!playerOrb.activeSelf)
                {
                    playerOrb.SetActive(true);
                }
                else
                {
                    playerOrb.SetActive(false);
                }
            }
            else
            {
                //비행 마법
            }
        }
        SwitchFootStep(beforePos);
        beforePos = transform.position;
    }

    private void FixedUpdate()
    {
        
    }

    private void SwitchFootStep(Vector3 beforePos_)
    {
        if(beforePos_ != transform.position)
        {
            playerFootStep.SetActive(true);
        }
        else
        {
            playerFootStep.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector3 beforePos;
    public GameObject playerFootStep;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayStateScripts : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    bool night = true;
    bool dayChk = false;
    bool stateChk = false;
    public void FixedUpdate()
    {
        Vector3 diagonal = new Vector3(1,0,0);
        //오브젝트 회전
        transform.Rotate(diagonal * rotationSpeed * Time.deltaTime);

        //각도에 따라 값 변경
        Quaternion objRotate = transform.rotation;
        float XRotate = Mathf.Abs(objRotate.x);
        if (XRotate > 0.985f && !dayChk || XRotate < 0.1f && !dayChk)
        {
            dayChk = true;
            night = !night;
            stateChk = true;
            MapGameManager.instance.DayStateChange(DayState.SUNSET);
        }
        else if (XRotate < 0.985f && dayChk || XRotate > 0.1f && dayChk)
        {
            if (XRotate > 0.5f && XRotate < 0.6f)
            {
                dayChk = false;
            }

            if (stateChk)
            {
                stateChk = false;
                DayState dayState = night ? DayState.NIGHT : DayState.MORNING;
                MapGameManager.instance.DayStateChange(dayState);
            }
         
        }


    }
  
}

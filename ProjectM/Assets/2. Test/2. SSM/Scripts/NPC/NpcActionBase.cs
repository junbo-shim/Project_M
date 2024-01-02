using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcActionBase : MonoBehaviour
{
 

    protected bool ClickBool = false;// 클릭용 불 
    protected void BoolChange()
    {
        ClickBool = true;
        Invoke("BoolReChange", 0.5f);
    }
    private void BoolReChange()
    {
        ClickBool = false;
    }

 

}

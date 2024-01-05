using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHandManager : MonoBehaviour
{
    public ChangePlayerHand leftCPH;
    public ChangePlayerHand rightCPH;

    public void ChangeHandMat(bool isInvisible)
    {
        leftCPH.ChangeHand(isInvisible);
        rightCPH.ChangeHand(isInvisible);
    }
}

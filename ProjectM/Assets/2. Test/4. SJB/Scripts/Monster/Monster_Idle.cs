using UnityEngine;

public class Monster_Idle : MonsterState
{
    private Vector3 patrolCenterPoint;
    private float patrolRange;
    private Vector3 destination;
    

    public override void OnStateEnter() 
    {
        Debug.LogWarning("Á¤Âû ½ÃÀÛ");
    }

    public override void OnStateStay() 
    {
        Debug.Log("Á¤Âû Áß");
    }

    public override void OnStateExit() 
    {
        Debug.LogError("Á¤Âû Å»Ãâ");
    }


    private void SetPatrolPoint() 
    {
        
    }

    private void SetDestination() 
    {
    
    }
}

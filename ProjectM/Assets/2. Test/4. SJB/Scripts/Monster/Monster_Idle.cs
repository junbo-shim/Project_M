using UnityEngine;

public class Monster_Idle : MonsterState
{
    private Vector3 patrolCenterPoint;
    private float patrolRange;
    private Vector3 destination;
    

    public override void OnStateEnter() 
    {
        Debug.LogWarning("���� ����");
    }

    public override void OnStateStay() 
    {
        Debug.Log("���� ��");
    }

    public override void OnStateExit() 
    {
        Debug.LogError("���� Ż��");
    }


    private void SetPatrolPoint() 
    {
        
    }

    private void SetDestination() 
    {
    
    }
}

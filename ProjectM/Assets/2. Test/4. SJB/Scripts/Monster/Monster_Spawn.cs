using UnityEngine;

public class Monster_Spawn : MonsterState
{
    public override void OnStateEnter() 
    {
        Debug.LogWarning("Spawn Start");
    }
    public override void OnStateStay() 
    {
        Debug.Log("Spawning");
    }
    public override void OnStateExit()
    {
        Debug.LogError("Spawn Complete");
    }
}

using System.Collections;
using UnityEngine;

public class Human_Melee : Monster
{
    public int hp;
    public float patrolRange;
    public float moveSpeed;
    public float sightRange;
    public float sightAngle;


    private void Awake()
    {
        hp = 500;
        patrolRange = 10f;
        moveSpeed = 4f;
        sightRange = transform.localScale.y * 4f;

        // 프로토타입용
        monsterSight.transform.localScale = Vector3.one * sightRange * 2f;
        sightAngle = 90f;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{
    public enum MonsterType
    {
        Human_Melee = 701,
        Mech_Melee,
        Mech_Range,
        Mech_Large,
        Orc_Melee,
        Spirit_Range,
        Orc_Large,
        Yeti_Melee,
        Yeti_Range,
        Yeti_Large,
        
        YetiPrince_Boss = 801,
        MechKing_Boss
    }

    public MonsterData monsterData;
    public MonsterStateMachine monsterFSM;
    public CharacterController monsterControl;
    public Animator monsterAnimator;
    public GameObject monsterSight;


    private void Awake()
    {

    }

    private void AddDatas() 
    {
        
    }
}

using UnityEngine;

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
        Spirit_Melee,

        YetiPrince_Boss = 801,
        MechKing_Boss
    }

    public enum DebuffState
    {
        Nothing = 500,
        Toxic,
        Slow,
        Freeze,
        Bind
    }

    public MonsterData monsterData;
    public MonsterStateMachine monsterFSM;
    public CharacterController monsterControl;
    public Animator monsterAnimator;
    public GameObject monsterSight;

    public MonsterType thisMonsterType;
    public DebuffState debuffState;

    public int monsterHP;
    public float monsterMoveSpeed;

    protected void InitMonster(MonsterType inputType_)
    {
        monsterData = MonsterCSVReader.Instance.MonsterDataDic[inputType_];
        debuffState = default;
        monsterHP = monsterData.MonsterHP;
        monsterMoveSpeed = monsterData.MonsterMoveSpeed;
    }

    protected void SetMonsterStat(MonsterType inputType_)
    {
        if ((int)inputType_ > 800)
        {

        }
        else
        {

        }
    }

    protected void GetMonsterHit(int damage_)
    {
        monsterHP -= damage_;

        if (monsterHP <= 0f)
        {
            DieMonster();
        }
        else
        {
            /* Do Nothing*/
        }
    }

    protected void DieMonster()
    {

    }
}

public class Orc_Melee : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Orc_Melee;
        InitMonster(thisMonsterType);
        debuffState = DebuffState.Nothing;
        monsterPatrolRange = 7f;

        monsterSightRange = 8f;

        monsterATKspeed = 3f;
    }
}

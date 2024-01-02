public class Spirit_Melee : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Spirit_Melee;
        InitMonster(thisMonsterType);
        debuffState = DebuffState.Nothing;
        monsterPatrolRange = 8f;

        monsterSightRange = 10f;

        monsterATKspeed = 3f;
    }
}

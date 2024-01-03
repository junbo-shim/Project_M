public class Yeti_Melee : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Yeti_Melee;
        InitMonster(thisMonsterType);
        debuffState = DebuffState.Nothing;
        monsterPatrolRange = 10f;

        monsterSightRange = 14f;

        monsterATKSpeed = 2.2f;
    }
}

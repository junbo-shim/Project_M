public class Orc_Melee : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Orc_Melee;
        InitMonster(thisMonsterType);
    }
}

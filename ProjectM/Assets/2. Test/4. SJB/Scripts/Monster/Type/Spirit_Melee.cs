public class Spirit_Melee : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Spirit_Melee;
        InitMonster(thisMonsterType);
    }
}

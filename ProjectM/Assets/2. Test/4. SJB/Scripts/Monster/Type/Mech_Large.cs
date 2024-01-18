public class Mech_Large : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Mech_Large;
        InitMonster(thisMonsterType);
    }
}

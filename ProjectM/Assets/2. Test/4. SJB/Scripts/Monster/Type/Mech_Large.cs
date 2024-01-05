public class Mech_Large : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Mech_Large;
        InitMonster(thisMonsterType);
        monsterPatrolRange = 14f;

        monsterSightRange = 9f;
        monsterSonarRange = 11f;
    }
}

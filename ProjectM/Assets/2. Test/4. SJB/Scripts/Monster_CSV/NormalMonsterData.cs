public class NormalMonsterData : MonsterData
{
    public NormalMonsterData(string csvData_)
    {
        CreateThisMonster(csvData_);
    }

    private NormalMonsterData CreateThisMonster(string csvData_)
    {
        string[] splitData = default;

        splitData = csvData_.Split(",");

        MonsterID = int.Parse(splitData[0]);
        MonsterDescription = splitData[1];
        MonsterType = int.Parse(splitData[2]);
        MonsterAttackRange = float.Parse(splitData[3]);
        MonsterMoveSpeed = float.Parse(splitData[4]);
        MonsterHP = int.Parse(splitData[5]);
        MonsterDamage = int.Parse(splitData[6]);
        MonsterRunSpeed = int.Parse(splitData[7]);

        return this;
    }
}

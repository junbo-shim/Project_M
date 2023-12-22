public class NormalMonsterData : MonsterData
{
    public NormalMonsterData(string csvData, int optionCount)
    {
        CreateThisMonster(csvData, optionCount);
    }

    private NormalMonsterData CreateThisMonster(string csvData, int optionCount)
    {
        string[] splitData = default;

        splitData = csvData.Split(",");

        for (int i = 0; i < optionCount; i++)
        {
            MonsterID = int.Parse(splitData[i]);
            MonsterDescription = splitData[i + 1];
            MonsterType = int.Parse(splitData[i + 2]);
            MonsterAttackRange = int.Parse(splitData[i + 3]);
            MonsterMoveSpeed = int.Parse(splitData[i + 4]);
            MonsterHP = int.Parse(splitData[i + 5]);
            MonsterDamage = int.Parse(splitData[i + 6]);
            MonsterRunSpeed = int.Parse(splitData[i + 7]);
        }
        return new NormalMonsterData(csvData, optionCount);
    }
}

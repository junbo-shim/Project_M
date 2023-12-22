public class BossMonsterData : MonsterData
{
    public BossMonsterData(string csvData, int optionCount)
    {
        CreateThisMonster(csvData, optionCount);
    }

    private BossMonsterData CreateThisMonster(string csvData, int optionCount)
    {
        string[] splitData = default;

        splitData = csvData.Split(",");

        for (int i = 0; i < optionCount; i++)
        {
            MonsterID = int.Parse(splitData[i]);
            MonsterDescription = splitData[i + 1];
            Pattern1Range = int.Parse(splitData[i + 2]);
            Pattern2Range = int.Parse(splitData[i + 3]);
            Pattern3Range = int.Parse(splitData[i + 4]);
            MonsterMoveSpeed = int.Parse(splitData[i + 5]);
            MonsterHP = int.Parse(splitData[i + 6]);
            MonsterDamage = int.Parse(splitData[i + 7]);
        }
        return new BossMonsterData(csvData, optionCount);
    }
}

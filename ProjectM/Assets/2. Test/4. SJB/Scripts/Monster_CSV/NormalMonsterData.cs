public class NormalMonsterData : MonsterData
{
    public NormalMonsterData(string csvData_, int optionCount_)
    {
        CreateThisMonster(csvData_, optionCount_);
    }

    private NormalMonsterData CreateThisMonster(string csvData_, int optionCount_)
    {
        string[] splitData = default;

        splitData = csvData_.Split(",");

        for (int i = 0; i < optionCount_; i++)
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
        return this;
    }
}

public class BossMonsterData : MonsterData
{
    public BossMonsterData(string csvData_, int optionCount_)
    {
        CreateThisMonster(csvData_, optionCount_);
    }

    private BossMonsterData CreateThisMonster(string csvData_, int optionCount_)
    {
        string[] splitData = default;

        splitData = csvData_.Split(",");

        for (int i = 0; i < optionCount_; i++)
        {
            MonsterID = int.Parse(splitData[i]);
            MonsterDescription = splitData[i + 1];
            Skill1Priority = int.Parse(splitData[i + 2]);
            Skill2Priority = int.Parse(splitData[i + 3]);
            Skill3Priority = int.Parse(splitData[i + 4]);
            MonsterMoveSpeed = int.Parse(splitData[i + 5]);
            MonsterHP = int.Parse(splitData[i + 6]);
            Skill1Cooltime = float.Parse(splitData[i + 7]);
            Skill2Cooltime = float.Parse(splitData[i + 8]);
            Skill3Cooltime = float.Parse(splitData[i + 9]);
            Skill1Damage = int.Parse(splitData[i + 10]);
            Skill2Damage = int.Parse(splitData[i + 11]);
            Skill3Damage = int.Parse(splitData[i + 12]);
            Force1 = int.Parse(splitData[i + 13]);
            Force2 = int.Parse(splitData[i + 14]);
        }
        return this;
    }
}

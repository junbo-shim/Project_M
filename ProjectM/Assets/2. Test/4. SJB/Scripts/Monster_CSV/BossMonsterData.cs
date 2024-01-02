public class BossMonsterData : MonsterData
{
    public BossMonsterData(string csvData_)
    {
        CreateThisMonster(csvData_);
    }

    private BossMonsterData CreateThisMonster(string csvData_)
    {
        string[] splitData = default;

        splitData = csvData_.Split(",");

        MonsterID = int.Parse(splitData[0]);
        MonsterDescription = splitData[1];
        Skill1Priority = int.Parse(splitData[2]);
        Skill2Priority = int.Parse(splitData[3]);
        Skill3Priority = int.Parse(splitData[4]);
        MonsterMoveSpeed = float.Parse(splitData[5]);
        MonsterHP = int.Parse(splitData[6]);
        Skill1Cooltime = float.Parse(splitData[7]);
        Skill2Cooltime = float.Parse(splitData[8]);
        Skill3Cooltime = float.Parse(splitData[9]);
        Skill1Damage = int.Parse(splitData[10]);
        Skill2Damage = int.Parse(splitData[11]);
        Skill3Damage = int.Parse(splitData[12]);
        Force1 = int.Parse(splitData[13]);
        Force2 = int.Parse(splitData[14]);

        return this;
    }
}

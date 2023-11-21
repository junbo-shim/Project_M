public class MonsterData
{
    public MonsterData(string csvData, int column, int row) 
    {
        CreateThisMonster(csvData, column, row);
    }


    public int MonsterID { get; private set; }

    public string MonsterName { get; private set; }

    public int MonsterHP { get; private set; }

    public int MonsterDamage { get; private set; }

    public float MonsterAtkCooltime { get; private set; }

    public float MonsterMoveSpeed { get; private set; }

    public float MonsterRunSpeed { get; private set; }

    public float MonsterMoveAngle { get; private set; }

    public float MonsterVisionRange { get; private set; }

    public float MonsterDetectionRange { get; private set; }

    public float MonsterStatusChangeTime { get; private set; }


    private MonsterData CreateThisMonster(string csvData, int column, int row) 
    {
        string[] splitData = default;

        splitData = csvData.Split(new char[] { '\n', ',' });

        for (int i = 0; i < row - 1; i++)
        {
            int idx = (1 + i) * column;

            MonsterID = int.Parse(splitData[idx + 0]);
            MonsterName = splitData[idx + 1];
            MonsterHP = int.Parse(splitData[idx + 2]);
            MonsterDamage = int.Parse(splitData[idx + 3]);
            MonsterAtkCooltime = float.Parse(splitData[idx + 4]);
            MonsterMoveSpeed = float.Parse(splitData[idx + 5]);
            MonsterRunSpeed = float.Parse(splitData[idx + 6]);
            MonsterMoveAngle = float.Parse(splitData[idx + 7]);
            MonsterVisionRange = float.Parse(splitData[idx + 8]);
            MonsterDetectionRange = float.Parse(splitData[idx + 9]);
            MonsterStatusChangeTime = float.Parse(splitData[idx + 10]);
        }

        return new MonsterData(csvData, column, row);
    }
}

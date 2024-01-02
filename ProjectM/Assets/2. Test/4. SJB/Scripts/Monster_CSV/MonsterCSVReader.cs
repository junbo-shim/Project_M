using UnityEngine;
using System.Collections.Generic;

public class MonsterCSVReader : MonoBehaviour
{
    #region Singleton
    private static MonsterCSVReader converter_Instance;
    // 싱글턴으로 만들기
    public static MonsterCSVReader Instance
    {
        get
        {
            if (converter_Instance == null)
            {
                converter_Instance = FindObjectOfType<MonsterCSVReader>();
            }
            return converter_Instance;
        }
    }
    #endregion


    // 읽어온 TextAsset 파일의 정보를 담을 변수
    private TextAsset dataFile = default;
    // 데이터 파일을 쪼개서 담을 배열 (csv 한 칸에 해당)
    public string[] ItemDataList { get; private set; }
    // csv 파일 행 크기
    public int MonsterCSVCounts { get; private set; }
    // csv 파일 열 크기
    public int OptionCSVCounts { get; private set; }

    // 몬스터의 데이터를 담아둘 Dictionary
    public Dictionary<Monster.MonsterType, MonsterData> MonsterDataDic { get; private set; }
    public List<string> monsterNameList;

    // 몬스터 디버프를 담아둘 Dictionary
    public Dictionary<Monster.DebuffState, DebuffData> MonsterDebuffDic { get; private set; }
    public List<string> debuffList;


    private void Awake()
    {
        MonsterDataDic = new Dictionary<Monster.MonsterType, MonsterData>();
        monsterNameList = new List<string>();

        MonsterDebuffDic = new Dictionary<Monster.DebuffState, DebuffData>();
        debuffList = new List<string>();

        GetAndReadCSV();
    }

    private void GetAndReadCSV()
    {
        // csvs 배열에 Resources/CSVData 폴더 안의 모든 TextAsset 을 저장한다
        TextAsset[] csvs = Resources.LoadAll<TextAsset>("CSVData_SJB");

        // csvs 배열크기만큼 반복문을 실행한다
        // Resources/CSVData 폴더 안의 CSV 파일 수만큼 반복한다
        for (int i = 0; i < csvs.Length; i++)
        {
            // dataFile 변수에 특정 배열 요소를 저장한다
            // 반복문을 돌 때마다 csv 파일을 바꿔서 저장한다
            dataFile = csvs[i];
            SaveMonsterData();
        }
    }

    private void SaveMonsterData()
    {
        // { dataList 가로 세로 저장하기
        string dataTrimNull = dataFile.text.TrimEnd();
        string[] tempRow = default;
        string[] tempColumn = default;
        string[] splitdata = default;

        tempRow = dataTrimNull.Split("\n");
        MonsterCSVCounts = tempRow.Length;

        tempColumn = tempRow[0].Split(",");
        OptionCSVCounts = tempColumn.Length;
        // } itemDataList 가로 세로 저장하기


        // { ID 값의 열만 얻어오기 위해서 \n와 , 로 데이터 한 칸씩만 남기기
        dataTrimNull = dataFile.text.TrimEnd();
        splitdata = dataTrimNull.Split("\n");
        // } ID 값의 열만 얻어오기


        // { Monster CSV 의 행(몬스터 개체별 데이터)을 이용하여 MonsterData 생성
        for (int i = 1; i < MonsterCSVCounts; i++)
        {
            switch (CheckID(dataTrimNull, OptionCSVCounts))
            {
                case 5:
                    // DebuffData 생성
                    DebuffData debuffData = new DebuffData(splitdata[i]);
                    int key = debuffData.DebuffID;
                    // MonsterDebuff Data 저장
                    MonsterDebuffDic.Add((Monster.DebuffState)key, debuffData);
                    debuffList.Add(debuffData.Description);
                    break;
                case 7:
                    // MonsterData 생성
                    NormalMonsterData monsterData = new NormalMonsterData(splitdata[i]);
                    key = monsterData.MonsterID;
                    // MonsterData 저장
                    MonsterDataDic.Add((Monster.MonsterType)key, monsterData);
                    monsterNameList.Add(monsterData.MonsterDescription);
                    break;
                case 8:
                    // BossData 생성
                    BossMonsterData bossData = new BossMonsterData(splitdata[i]);
                    key = bossData.MonsterID;
                    // BossData 저장
                    MonsterDataDic.Add((Monster.MonsterType)key, bossData);
                    monsterNameList.Add(bossData.MonsterDescription);
                    break;
            }
        }
    }


    // 뒷 3 자리 없애오는 함수 : ID 의 앞자리로 어떤 요소인지 판단
    private int DropLastThree(string number_)
    {
        string dropped = number_.Substring(0, number_.Length - 2);
        return int.Parse(dropped);
    }


    // ID 값에서 일반몬스터 테이블인지, 보스몬스터 테이블인지 확인할 함수
    private int CheckID(string data_, int columnCount_)
    {
        string[] split = default;
        split = data_.Split(new char[] { '\n', ',' });

        // ID 값에서 뒷 3자리를 없애고 어떤 것에 대한 데이터인지 판별할 변수
        int num = default;

        for (int i = columnCount_; i < split.Length; i += columnCount_)
        {
            // ID 값에서 뒷 3자리를 없애주는 함수의 결과값을 number 변수에 저장
            num = DropLastThree(split[i]);
        }

        return num;
    }
}

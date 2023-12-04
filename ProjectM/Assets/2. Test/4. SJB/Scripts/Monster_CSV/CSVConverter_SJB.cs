using System.Collections.Generic;
using UnityEngine;

public class CSVConverter_SJB : MonoBehaviour
{
    #region Singleton
    private static CSVConverter_SJB converter_Instance;
    // 싱글턴으로 만들기
    public static CSVConverter_SJB Instance
    {
        get
        {
            if (converter_Instance == null)
            {
                converter_Instance = FindObjectOfType<CSVConverter_SJB>();
            }
            return converter_Instance;
        }
    }
    #endregion

    #region 프로퍼티 및 변수
    // 읽어온 TextAsset 파일의 정보를 담을 변수
    private TextAsset itemDataFile = default;
    // 데이터 파일을 쪼개서 담을 배열 (csv 한 칸에 해당)
    public string[] ItemDataList { get; private set; }
    // csv 파일 행 크기
    public int CSVRowCount { get; private set; }
    // csv 파일 열 크기
    public int CSVColumnCount { get; private set; }
    // csv 파일에서 읽어낸 ID 값을 Key 로 하는 Dictionary (Key : ID_int, Value : MonsterData_Class)
    public Dictionary<int, MonsterData> MonsterDataDictionary { get; private set; }
    // csv 파일 저장 Class 리스트 (inspector 에서 확인용)
    public List<MonsterData> MonsterDataList;
    // MonsterData 의 key 변수
    private int key;
    // switch case 문을 통해 분류할 number 변수
    private int num;
    #endregion


    private void Awake()
    {
        ReadCSV();
        //Test();
    }


    #region 메서드
    // csv 파일을 읽어오는 함수
    private void ReadCSV()
    {
        // csvs 배열에 Resources/CSVData 폴더 안의 모든 TextAsset 을 저장한다
        TextAsset[] csvs = Resources.LoadAll<TextAsset>("CSVData_SJB");
        // Dictionary 를 초기화 한다
        MonsterDataDictionary = new Dictionary<int, MonsterData>();
        // List 를 초기화 한다
        MonsterDataList = new List<MonsterData>();

        // csvs 배열크기만큼 반복문을 실행한다
        // Resources/CSVData 폴더 안의 CSV 파일 수만큼 반복한다
        for (int i = 0; i < csvs.Length; i++) 
        {
            // itemDataFile 변수에 특정 배열 요소를 저장한다
            // 반복문을 돌 때마다 csv 파일을 바꿔서 저장한다
            itemDataFile = csvs[i];
            // itemDataFile 에 저장한 csv 파일을 기준으로 함수를 실행한다
            CheckAndSaveID();
        }
    }

    // csv 파일에서 ID 값만 추출해서 int 로 변환, List 에 저장하는 함수
    private void CheckAndSaveID()
    {
        // { itemDataList 가로 세로 저장하기
        string dataTrimNull = itemDataFile.text.TrimEnd();
        string[] tempRow = default;
        string[] tempColumn = default;
        string[] splitdata = default;

        tempRow = dataTrimNull.Split("\n");
        CSVRowCount = tempRow.Length;

        tempColumn = tempRow[0].Split(",");
        CSVColumnCount = tempColumn.Length;
        // } itemDataList 가로 세로 저장하기


        // { ID 값만 얻어오기 위해서 \n와 , 로 데이터 한 칸씩만 남기기
        dataTrimNull = itemDataFile.text.TrimEnd();
        splitdata = dataTrimNull.Split(new char[] { '\n', ',' });
        // } ID 값만 얻어오기

        //Debug.LogWarning(dataTrimNull);

        // { ID 값을 Dictionary 에 저장하기
        for (int i = CSVColumnCount; i < splitdata.Length; i += CSVColumnCount)
        {
            // ID 값에서 뒷 3자리를 없애고 어떤 것에 대한 데이터인지 판별할 변수
            num = default;
            // ID 값에서 뒷 3자리를 없애주는 함수의 결과값을 number 변수에 저장
            num = DropLastThree(splitdata[i]);

            // ScriptableObjDictionary 에 저장할 Key : 1001, 2001, 3001, 3002 etc
            key = int.Parse(splitdata[i]);

            // MonsterData 에 저장할 Value 초기화
            MonsterData monsterData = new MonsterData(dataTrimNull, CSVColumnCount, CSVRowCount);

            // Key 값과 Value 값을 저장한다
            MonsterDataDictionary.Add(key, monsterData);
            MonsterDataList.Add(monsterData);
        }
        // } ID 값을 Dictionary 에 저장하기
    }

    // 뒷 3 자리 없애오는 함수 : ID 의 앞자리로 어떤 요소인지 판단
    private int DropLastThree(string number) 
    {
        string dropped = number.Substring(0, number.Length - 3);
        return int.Parse(dropped);
    }
    #endregion


    private void Test() 
    {
        foreach (var a in MonsterDataDictionary)
        {
            int b = a.Key;
            MonsterData c = a.Value;
            Debug.LogWarning(b);
            Debug.LogWarning(c);
        }
    }
}

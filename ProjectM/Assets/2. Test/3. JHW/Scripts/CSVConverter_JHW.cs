using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CSVConverter_JHW : MonoBehaviour
{
    #region Singleton
    private static CSVConverter_JHW converter_Instance;
    // 싱글턴으로 만들기
    public static CSVConverter_JHW Instance
    {
        get
        {
            if (converter_Instance == null)
            {
                converter_Instance = FindObjectOfType<CSVConverter_JHW>();
            }
            return converter_Instance;
        }
    }
    #endregion

    #region 프로퍼티 및 변수
    // 읽어온 TextAsset 파일의 정보를 담을 변수
    private TextAsset damageSkillData = default;
    private TextAsset noDamageSkillData = default;

    // 데이터 파일을 쪼개서 담을 배열 (csv 한 칸에 해당)
    public string[] ItemDataList { get; private set; }
    // csv 파일 행 크기
    public int CSVRowCount { get; private set; }
    // csv 파일 열 크기
    public int CSVColumnCount { get; private set; }

    public List<Damage> DamageSkillList;
    public List<NoDamage> NoDamageSkillList;

    public Dictionary<string, SkillParent> skillDic = new Dictionary<string, SkillParent>();
    public Dictionary<string, SkillParent> patternDic = new Dictionary<string, SkillParent>();

    public string tempString;

    #endregion


    private void Awake()
    {
        ReadCSV();
        CheckAndSaveID(damageSkillData);
        //CheckAndSaveID(noDamageSkillData);
        CheckAndSaveID2(noDamageSkillData);
        //Test();

        // 딕셔너리의 키 값 출력
        //foreach (string key in skillDic.Keys)
        //{
        //    Debug.Log($"Key: {key}");
        //}
    }


    #region 메서드
    // csv 파일을 읽어오는 함수
    private void ReadCSV()
    {
        // csvs 배열에 Resources/CSVData 폴더 안의 모든 TextAsset 을 저장한다
        damageSkillData = Resources.Load<TextAsset>("DamageSkill");
        noDamageSkillData = Resources.Load<TextAsset>("NoDamage");

        DamageSkillList = new List<Damage>();
        NoDamageSkillList = new List<NoDamage>();
    }

    // csv 파일에서 ID 값만 추출해서 int 로 변환, List 에 저장하는 함수
    private void CheckAndSaveID(TextAsset data)
    {
        // { itemDataList 가로 세로 저장하기
        string dataTrimNull = data.text.TrimEnd();
        string[] tempRow = default;
        string[] tempColumn = default;
        string[] splitdata = default;

        tempRow = dataTrimNull.Split("\n");
        CSVRowCount = tempRow.Length;

        tempColumn = tempRow[0].Split(",");
        CSVColumnCount = tempColumn.Length;
        // } itemDataList 가로 세로 저장하기


        // { ID 값만 얻어오기 위해서 \n와 , 로 데이터 한 칸씩만 남기기
        dataTrimNull = data.text.TrimEnd();
        splitdata = dataTrimNull.Split(new char[] { '\n', ',' });
        // } ID 값만 얻어오기

        //Debug.LogWarning(dataTrimNull);


        for (int i = CSVColumnCount; i < splitdata.Length; i += CSVColumnCount)
        {
            Damage damageSkill = new Damage();


            damageSkill.ID = int.Parse(splitdata[i]);
            damageSkill.skillName = splitdata[i + 1].ToString();
            damageSkill.description = splitdata[i + 2].ToString();
            damageSkill.skillDamage = int.Parse(splitdata[i + 3]);
            damageSkill.skillCritical = float.Parse(splitdata[i + 4]);
            damageSkill.skillCriAdd = float.Parse(splitdata[i + 5]);
            damageSkill.skillRange = int.Parse(splitdata[i + 6]);
            damageSkill.Value1 = float.Parse(splitdata[i + 7]);
            damageSkill.Value2 = float.Parse(splitdata[i + 8]);
            //패턴 스트링 저장
            damageSkill.skillPattern = new List<int>(); //리스트는 체크용
            damageSkill.skillPattern = SpiltSkillPattern(splitdata[i + 9]);

            DamageSkillList.Add(damageSkill);
            skillDic.Add(damageSkill.skillName, damageSkill);
            if (damageSkill.skillName[0] != '_')
            {
                Debug.Log("들어왔다 데미지");
                patternDic.Add((splitdata[i + 9]).Trim(), damageSkill);
                tempString = splitdata[i + 9];
            }
        }

    }

    //// 뒷 3 자리 없애오는 함수 : ID 의 앞자리로 어떤 요소인지 판단
    //private int DropLastThree(string number)
    //{
    //    string dropped = number.Substring(0, number.Length - 3);
    //    return int.Parse(dropped);
    //}

    private List<int> SpiltSkillPattern(string pattern)
    {
        List<int> result = new List<int>();

        for (int idx = 0; idx < pattern.Length - 1; idx++)
        {
            double num = char.GetNumericValue(pattern[idx]);
            result.Add((int)num);
        }

        return result;
    }
    #endregion










    // csv 파일에서 ID 값만 추출해서 int 로 변환, List 에 저장하는 함수
    private void CheckAndSaveID2(TextAsset data)
    {
        // { itemDataList 가로 세로 저장하기
        string dataTrimNull = data.text.TrimEnd();
        string[] tempRow = default;
        string[] tempColumn = default;
        string[] splitdata = default;

        tempRow = dataTrimNull.Split("\n");
        CSVRowCount = tempRow.Length;

        tempColumn = tempRow[0].Split(",");
        CSVColumnCount = tempColumn.Length;
        // } itemDataList 가로 세로 저장하기


        // { ID 값만 얻어오기 위해서 \n와 , 로 데이터 한 칸씩만 남기기
        dataTrimNull = data.text.TrimEnd();
        splitdata = dataTrimNull.Split(new char[] { '\n', ',' });
        // } ID 값만 얻어오기

        //Debug.LogWarning(dataTrimNull);


        for (int i = CSVColumnCount; i < splitdata.Length; i += CSVColumnCount)
        {
            NoDamage NodamageSkill = new NoDamage();

            NodamageSkill.ID = int.Parse(splitdata[i]);
            NodamageSkill.skillName = splitdata[i + 1].ToString();
            NodamageSkill.description = splitdata[i + 2].ToString();
            NodamageSkill.SkillDuration = float.Parse(splitdata[i + 3]);
            NodamageSkill.Target = splitdata[i + 4].ToString();
            NodamageSkill.SkillRange = int.Parse(splitdata[i + 5]);
            NodamageSkill.Value1 = float.Parse(splitdata[i + 6]);
            NodamageSkill.Value2 = float.Parse(splitdata[i + 7]);
            NodamageSkill.Value3 = float.Parse(splitdata[i + 8]);


            NodamageSkill.skillPattern = new List<int>();
            NodamageSkill.skillPattern = SpiltSkillPattern2(splitdata[i + 9]);

            NoDamageSkillList.Add(NodamageSkill);
            skillDic.Add(NodamageSkill.skillName, NodamageSkill);

            if (NodamageSkill.skillName[0] != '_')
            {
                Debug.Log("들어왔다 수민");
                patternDic.Add((splitdata[i + 9]).Trim(), NodamageSkill);
                tempString = splitdata[i + 9];
            }
        }

    }

    //// 뒷 3 자리 없애오는 함수 : ID 의 앞자리로 어떤 요소인지 판단
    //private int DropLastThree2(string number)
    //{
    //    string dropped = number.Substring(0, number.Length - 3);
    //    return int.Parse(dropped);
    //}

    private List<int> SpiltSkillPattern2(string pattern)
    {
        List<int> result = new List<int>();

        for (int idx = 0; idx < pattern.Length - 1; idx++)
        {
            double num = char.GetNumericValue(pattern[idx]);
            result.Add((int)num);
        }

        return result;
    }
    private void Test()
    {
        foreach (var a in NoDamageSkillList)
        {
            Debug.LogWarning(a.skillName);
            Debug.LogWarning(a.description);
        }
    }
}
using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SkillManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("SkillManager").AddComponent<SkillManager>();
                }
            }
            return _instance;
        }
    }
    private static SkillManager _instance;
    #endregion

    public Dictionary<string, int> skillCheckDict;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 스킬 이름을 입력받아 가지고 있는지 체크해 bool값을 반환하는 함수
    public bool HasSkillByName(string skillName_)
    {
        if(skillCheckDict.ContainsKey(skillName_))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 스킬 이름을 입력받아 강화가 됐는지 체크해 bool값을 반환하는 함수
    public bool HasSkillEnhancedByName(string skillName_)
    {
        string enhancedName = "_" + skillName_;
        if(skillCheckDict.ContainsKey(enhancedName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

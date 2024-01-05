using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBTIScripts : MonoBehaviour
{
    private static MBTIScripts _instance;

    public int mbtiScore_E =0;
    public int mbtiScore_I = 0;
    public int mbtiScore_S = 0;
    public int mbtiScore_N = 0;
    public int mbtiScore_F = 0;
    public int mbtiScore_T = 0;
    public int mbtiScore_J = 0;
    public int mbtiScore_P = 0;

    public string maxKey;
    public int maxValue;
    public string minKey;
    public int minValue;

    public Dictionary<string, int> MBTiScore_;
    public static MBTIScripts Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MBTIScripts();
            }

            return _instance;
        }
    }
    public void Awake()
    {
        
        if (this != Instance)
        {
            Destroy(this);
        }
    }
    public string MaxMbti() // 저장시 최대값 뽑기
    {
        MBTiScore_ = new Dictionary<string, int>
        {
            { "E", mbtiScore_E },
            { "I", mbtiScore_I },
            { "S", mbtiScore_S },
            { "N", mbtiScore_N },
            { "F", mbtiScore_F },
            { "T", mbtiScore_T },
            { "J", mbtiScore_J },
            { "P", mbtiScore_P }
        };

        foreach(var dic in MBTiScore_)
        {
            if(maxValue < dic.Value)
            {
                maxKey = dic.Key;
                maxValue = dic.Value;
            }
            if (minValue > dic.Value)
            {
                minKey = dic.Key;
                minValue = dic.Value;
            }
        }
        if (maxValue == 0)
        {
            maxKey = "I";
            maxValue = 0;
        }
        return maxKey;
    }

    public void MBTI_E_Add(int value)
    {
        mbtiScore_E += value;
        
    }
    public void MBTI_I_Add(int value)
    {
        mbtiScore_I += value;
        
    }
    public void MBTI_S_Add(int value)
    {
        mbtiScore_S += value;
     
    }
    public void MBTI_N_Add(int value)
    {
        mbtiScore_N += value;
     
    }
    public void MBTI_F_Add(int value)
    {
        mbtiScore_F += value;

    }
    public void MBTI_T_Add(int value)
    {
        mbtiScore_T += value;

    }
    public void MBTI_J_Add(int value)
    {
        mbtiScore_J += value;

    }
    public void MBTI_P_Add(int value)
    {
        mbtiScore_P += value;
        
    }
}
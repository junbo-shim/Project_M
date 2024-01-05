using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MBTIScripts : MonoBehaviour
{
    private static MBTIScripts _instance;

    public int mbtiScore_E;
    public int mbtiScore_I;
    public int mbtiScore_S;
    public int mbtiScore_N;
    public int mbtiScore_F;
    public int mbtiScore_T;
    public int mbtiScore_J;
    public int mbtiScore_P;

    public string maxKey;
    public int maxValue;
    public string minKey;
    public int minValue;
    public StringBuilder finalMBTI;
    public Dictionary<string, int> MBTiScore_;
    public static MBTIScripts Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("MBTIScripts").AddComponent<MBTIScripts>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    public void Awake()
    {
        
         MBTiScore_ = new Dictionary<string, int>();
    }
    void Start()
    {
        maxValue = int.MinValue;
        minValue = int.MaxValue;
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
      
   
        
        foreach (var dic in MBTiScore_)
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
    public string FinalMBTIDerive()
    {
        if(mbtiScore_E > mbtiScore_I)
        {
            finalMBTI.Append("E");
        }
        else
        {
            finalMBTI.Append("I");
        }
        if (mbtiScore_S > mbtiScore_N)
        {
            finalMBTI.Append("S");
        }
        else
        {
            finalMBTI.Append("N");
        }
        if (mbtiScore_F > mbtiScore_T)
        {
            finalMBTI.Append("F");
        }
        else
        {
            finalMBTI.Append("T");
        }
        if (mbtiScore_P > mbtiScore_J)
        {
            finalMBTI.Append("P");
        }
        else
        {
            finalMBTI.Append("J");
        }
        return finalMBTI.ToString();
    }
}
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
        if (MBTiScore_.ContainsKey("E"))
        {
            MBTiScore_["E"] += value; 
        }
        else
        {
            MBTiScore_.Add("E", value); 
        }
   
       
    }
    public void MBTI_I_Add(int value)
    {
   
        if (MBTiScore_.ContainsKey("I"))
        {
            MBTiScore_["I"] += value;
        }
        else
        {
            MBTiScore_.Add("I", value);
        }
    }
    public void MBTI_S_Add(int value)
    {
        if (MBTiScore_.ContainsKey("S"))
        {
            MBTiScore_["S"] += value;
        }
        else
        {
            MBTiScore_.Add("S", value);
        }

    }
    public void MBTI_N_Add(int value)
    {
        if (MBTiScore_.ContainsKey("N"))
        {
            MBTiScore_["N"] += value;
        }
        else
        {
            MBTiScore_.Add("N", value);
        }

    }
    public void MBTI_F_Add(int value)
    {
        if (MBTiScore_.ContainsKey("F"))
        {
            MBTiScore_["F"] += value;
        }
        else
        {
            MBTiScore_.Add("F", value);
        }

    }
    public void MBTI_T_Add(int value)
    {
        if (MBTiScore_.ContainsKey("T"))
        {
            MBTiScore_["T"] += value;
        }
        else
        {
            MBTiScore_.Add("T", value);
        }

    }
    public void MBTI_J_Add(int value)
    {
        if (MBTiScore_.ContainsKey("J"))
        {
            MBTiScore_["J"] += value;
        }
        else
        {
            MBTiScore_.Add("J", value);
        }

    }
    public void MBTI_P_Add(int value)
    {
        if (MBTiScore_.ContainsKey("P"))
        {
            MBTiScore_["P"] += value;
        }
        else
        {
            MBTiScore_.Add("P", value);
        }

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
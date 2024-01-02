using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBTIScripts : MonoBehaviour
{
    private static MBTIScripts _instance;

    public string MBTiScore_E;
    public string MBTiScore_I;
    public string MBTiScore_S;
    public string MBTiScore_N;
    public string MBTiScore_F;
    public string MBTiScore_T;
    public string MBTiScore_J;
    public string MBTiScore_P;

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
    
    public void MBTI_E_Add(int value)
    {
        MBTiScore_E += value;
        Debug.Log(MBTiScore_E);    
    }
    public void MBTI_I_Add(int value)
    {
        MBTiScore_I += value;
        Debug.Log(MBTiScore_I);
    }
    public void MBTI_S_Add(int value)
    {
        MBTiScore_S += value;
        Debug.Log(MBTiScore_S);
    }
    public void MBTI_N_Add(int value)
    {
        MBTiScore_N += value;
        Debug.Log(MBTiScore_N);
    }
    public void MBTI_F_Add(int value)
    {
        MBTiScore_F += value;
        Debug.Log(MBTiScore_F);
    }
    public void MBTI_T_Add(int value)
    {
        MBTiScore_T += value;
        Debug.Log(MBTiScore_T);
    }
    public void MBTI_J_Add(int value)
    {
        MBTiScore_J += value;
        Debug.Log(MBTiScore_J);
    }
    public void MBTI_P_Add(int value)
    {
        MBTiScore_P += value;
        Debug.Log(MBTiScore_P);
    }
}
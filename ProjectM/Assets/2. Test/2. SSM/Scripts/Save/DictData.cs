using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictData
{
   public string[] mbti;


    public void SetArray(string[] strings)
    {
        mbti = new string[strings.Length];
        Array.Copy(strings, mbti, strings.Length);
    }

    public string[] GetArray()
    {
        return mbti;
    }

}
  

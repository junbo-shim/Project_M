using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbtTitleData 
{
    private int iD;
    public int Id
    {
        get { return iD; }
        set { iD = value; }
    }
    private int mbtiStep;
    public int MBtiStep
    {
        get { return mbtiStep; }
        set { mbtiStep = value; }
    }
    private string category1;
    public string Category1
    {
        get { return category1; }
        set { category1 = value; }
    }
    private string script;
    public string Script
    {
        get { return script; }
        set { script = value; }
    }
}

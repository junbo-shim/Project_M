using System.Collections.Generic;


//[Serializable]
public class Damage : SkillParent
{
    public string description;
    public int skillDamage;
    public float skillCritical;
    public float skillCriAdd;
    public string icon;
    public int skillRange;
    public float Value1;
    public float Value2;
    public List<int> skillPattern;
}


//[Serializable]
public class NoDamage : SkillParent
{

    public string description;
    public float SkillDuration;
    public string Target;
    public int SkillRange;
    public float Value1;
    public float Value2;
    public float Value3;


    public List<int> skillPattern;

}


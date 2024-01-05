public class MonsterData
{
    // 일반 몬스터
    public virtual int MonsterID { get; protected set; }
    public virtual string MonsterDescription { get; protected set; }
    public virtual int MonsterType { get; protected set; }
    public virtual float MonsterAttackRange { get; protected set; }
    public virtual float MonsterMoveSpeed { get; protected set; }
    public virtual int MonsterHP { get; protected set; }
    public virtual int MonsterDamage { get; protected set; }
    public virtual float MonsterRunSpeed { get; protected set; }


    // 보스 몬스터
    public virtual int Skill1Priority { get; protected set; }
    public virtual int Skill2Priority { get; protected set; }
    public virtual int Skill3Priority { get; protected set; }
    public virtual float Skill1Cooltime { get; protected set; }
    public virtual float Skill2Cooltime { get; protected set; }
    public virtual float Skill3Cooltime { get; protected set; }

    public virtual int Skill1Damage { get; protected set; }
    public virtual int Skill2Damage { get; protected set; }
    public virtual int Skill3Damage { get; protected set; }

    public virtual int Force1 { get; protected set; }
    public virtual int Force2 { get; protected set; }
}

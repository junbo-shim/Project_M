public class MonsterData
{
    // 일반 몬스터
    public virtual int MonsterID { get; protected set; }
    public virtual string MonsterDescription { get; protected set; }
    public virtual int MonsterType { get; protected set; }
    public virtual int MonsterAttackRange { get; protected set; }
    public virtual int MonsterMoveSpeed { get; protected set; }
    public virtual int MonsterHP { get; protected set; }
    public virtual int MonsterDamage { get; protected set; }
    public virtual int MonsterRunSpeed { get; protected set; }


    // 보스 몬스터
    public virtual int Pattern1Range { get; protected set; }
    public virtual int Pattern2Range { get; protected set; }
    public virtual int Pattern3Range { get; protected set; }
    public virtual float Pattern1Cooltime { get; protected set; }
    public virtual float Pattern2Cooltime { get; protected set; }
    public virtual int Pattern1Damage { get; protected set; }
    public virtual int Pattern2Damage { get; protected set; }
    public virtual int Pattern1PushForce { get; protected set; }
    public virtual int Pattern2PushForce { get; protected set; }




    //public float MonsterPatrolTimeMin { get; private set; }

    //public float MonsterPatrolTimeMax { get; private set; }

    //public float MonsterSightRange { get; private set; }

    //public float MonsterSightAngle { get; private set; }



    //public float MonsterSonarRange { get; private set; }

    //public float MonsterAtkCooltime { get; private set; }

    //public float MonsterMoveAngle { get; private set; }

    //public float MonsterStatusChangeTime { get; private set; }
}

using UnityEngine;

public class Yeti_Boss : Monster
{
    public BossStateMachine bossFSM;

    public float ChargeCooltime;
    public float SnowballCooltime;
    public float PunchCooltime;

    public int isChargingID;
    public int isSnowballingID;
    public int isPunchingID;


    private void Awake() 
    {
        thisMonsterType = MonsterType.YetiKing_Boss;
        InitMonster(thisMonsterType);

        //isPunchingID
        //isSnowballingID
        //isChargingID

        target = 
            GameObject.Find("Player_LJY").transform.Find("PlayerController").transform.Find("PlayerRigid").gameObject;
    }

    protected override void OnEnable()
    {
        base.InitDebuffTimer();
    }

    protected override void OnDisable()
    {
        base.InitDebuffTimer();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPoison : MonoBehaviour
{
    private Damage poisonInfo;
    public float duration;
    private float statusEffectID;           // 스킬 상태이상 ID

    // Start is called before the first frame update
    void Start()
    {
        poisonInfo = CSVConverter_JHW.Instance.skillDic["_Poison"] as Damage;
        duration = poisonInfo.Value1;
        statusEffectID = poisonInfo.Value2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterHPGauge : MonoBehaviour
{
    public Image hpGauge;
    public Monster monster;
    private int monsterMaxHP;

    [SerializeField]
    private float gaugeSpeed;


    private void OnEnable()
    {
        monsterMaxHP = monster.monsterHP;
        hpGauge.fillAmount = (float) monster.monsterHP / monsterMaxHP;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        hpGauge.fillAmount = Mathf.Lerp(hpGauge.fillAmount,
            (float)monster.monsterHP / monsterMaxHP, Time.deltaTime * gaugeSpeed);
    }
}

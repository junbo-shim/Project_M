using UnityEngine;

public class MonsterATK : MonoBehaviour
{
    public GameObject monster;
    public int damage;

    private void OnEnable()
    {
        damage = monster.GetComponent<Monster>().monsterData.MonsterDamage;
    }
}

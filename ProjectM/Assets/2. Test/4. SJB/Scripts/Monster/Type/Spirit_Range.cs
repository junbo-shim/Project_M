using System.Collections;
using UnityEngine;

public class Spirit_Range : Monster
{
    private void Awake()
    {
        thisMonsterType = MonsterType.Spirit_Range;
        InitMonster(thisMonsterType);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpawner : MonoBehaviour
{
    public GameObject[] MagicObjects;
    public GameObject ReturnMagic(int id)
    {
        switch(id)
        {
            case 1000:  // 파이어볼
                return MagicObjects[0];
            case 1002:  // 포이즌
                return MagicObjects[1];
            case 1100:  // 아이스볼
                return MagicObjects[2];
            case 1104: // 점프
                return MagicObjects[3];
            case 1107: // 힐
                return MagicObjects[4];
            default:
                return null;
        }
    }
}

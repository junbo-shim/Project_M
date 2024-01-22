using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleEvent_SSM : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {

   
        other.gameObject.GetComponent<MonsterRigid>()?.GetMonsterDamage(gameObject);
    }

   
    
}

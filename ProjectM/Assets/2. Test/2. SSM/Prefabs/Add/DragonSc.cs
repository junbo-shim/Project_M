using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DragonSc : MonoBehaviour
{
    public enum MonsterState
    {
        SLEEP,
        IDLE,
        MOVE,        
        FLY,
        FLY_FLAME,
        HIT,
        BASIC,
        FLAME
    }

    [SerializeField] private GameObject playerObj;
    private Animator animator;

    private MonsterState _state;


    // Start is called before the first frame update
    void Start()
    {

        SetStart();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            switch (_state)
            {
                case MonsterState.SLEEP:
                    animator.SetTrigger("Start");
                    ChangState(MonsterState.IDLE);
                    break;

                case MonsterState.IDLE:

                    break;
                    
                case MonsterState.MOVE:

                    break;

                case MonsterState.FLY:

                    break;

                case MonsterState.HIT:

                    break;

                case MonsterState.BASIC:

                    break;

                case MonsterState.FLAME:

                    break;
            }
        }
       
    }

    private void ChangState(MonsterState newState)
    {
        _state = newState;
    }
  
    private void SetStart() //시작 정보 세팅
    {
        _state = MonsterState.SLEEP;
        animator = GetComponent<Animator>();
    }
}

using UnityEngine;

public class MonsterRigid : MonoBehaviour
{
    // 몬스터
    public GameObject monster;
    // 몬스터 FSM
    public MonsterStateMachine monsterFSM;
    // 몬스터 컴포넌트
    private Monster monsterComponent;
    // 부딪힌 SkillAction 컴포넌트
    private SkillAction skillAction;
    // 플레이어 리지드 캐싱용 변수
    private GameObject playerRigid;


    private void OnEnable()
    {
        monsterComponent = monster.GetComponent<Monster>();
        playerRigid = 
            GameObject.Find("XR Rig Advanced_LJY").transform.Find("PlayerController").transform.Find("PlayerRigid").gameObject;
    }

    private void OnTriggerEnter(Collider other_)
    {
        // 만약 충돌한 물체가 PlayerATK 레이어를 가지고 있을 경우 + 몬스터의 체력이 0 이상일 경우에만 체크
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerATK")) 
            && monsterComponent.monsterHP > 0) 
        {
            // 처음 맞았을 경우 
            if (!monsterFSM.currentState.Equals(MonsterStateMachine.State.Engage) 
                && !monsterFSM.currentState.Equals(MonsterStateMachine.State.Die)) 
            {
                // 타겟을 설정
                monsterComponent.target = playerRigid;
                // 교전상태로 전환
                monsterFSM.ChangeState(MonsterStateMachine.State.Engage);
            }


            // 부딪힌 collider 를 캐싱한다
            skillAction = other_.GetComponent<SkillAction>();

            // 데미지만 가하는 스킬인 경우
            if (skillAction.isDamage == true && skillAction.isStatusEff == false) 
            {
                GetMonsterHit((int)skillAction.damage);
            }
            // 데미지 + 상태이상 스킬인 경우
            else if (skillAction.isDamage == true && skillAction.isStatusEff == true) 
            {
                GetMonsterHit((int)skillAction.damage);
                
                MonsterDebuff.Instance.DoDebuff(monsterComponent, (int)skillAction.statusEffId);
            }
            // 상태이상만 가하는 스킬인 경우
            else if (skillAction.isDamage == false && skillAction.isStatusEff == true) 
            {
                MonsterDebuff.Instance.DoDebuff(monsterComponent, (int)skillAction.statusEffId);
            }
        }
    }


    #region 데미지 받는 메서드
    private void GetMonsterHit(int damage_)
    {
        // 데미지 감소 적용
        monsterComponent.monsterHP -= damage_;

        // 체력이 0 이하일 경우 Die 상태로 전환
        if (monsterComponent.monsterHP <= 0f)
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Die);
        }
    }
    #endregion
}

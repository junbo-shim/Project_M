using UnityEngine;

public class BossRigid : MonsterRigid
{
    // 몬스터 FSM
    public BossStateMachine bossFSM;


    protected override void OnTriggerEnter(Collider other_)
    {
        // 만약 충돌한 물체가 PlayerATK 레이어를 가지고 있을 경우 + 몬스터의 체력이 0 이상일 경우에만 체크
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerATK"))
            && monsterComponent.monsterHP > 0)
        {
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

        // 보스가 Wall 낙사 시 데미지 받는 로직
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("FallDie")))
        {
            GetMonsterHit(monsterComponent.monsterHP / 6);

            monsterComponent.transform.position = new Vector3(4660, 2020, 2730);
        }
    }


    #region 데미지 받는 메서드
    protected override void GetMonsterHit(int damage_)
    {
        // 데미지 감소 적용
        monsterComponent.monsterHP -= damage_;

        // 체력이 0 이하일 경우 Die 상태로 전환
        if (monsterComponent.monsterHP <= 0f)
        {
            bossFSM.ChangeState(BossStateMachine.BossState.BossDie);
        }
    }
    #endregion
}

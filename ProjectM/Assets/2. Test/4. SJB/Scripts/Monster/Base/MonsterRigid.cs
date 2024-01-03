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
    private SkillAction action;


    private void OnEnable()
    {
        monsterComponent = monster.GetComponent<Monster>();
    }

    private void OnTriggerEnter(Collider other_)
    {
        // 만약 충돌한 물체가 PlayerATK 레이어를 가지고 있을 경우
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerATK")) && monsterComponent.monsterHP > 0) 
        {
            // 부딪힌 collider 를 캐싱한다
            action = other_.GetComponent<SkillAction>();

            // 데미지만 가하는 스킬인 경우
            if (action.isDamage == true && action.isStatusEff == false) 
            {
                GetMonsterHit(monsterComponent.monsterHP, (int)action.damage);
                Debug.LogWarning("1");
            }
            // 데미지 + 상태이상 스킬인 경우
            else if (action.isDamage == true && action.isStatusEff == true) 
            {
                GetMonsterHit(monsterComponent.monsterHP, (int)action.damage);
                Debug.LogWarning("2");
            }
            // 상태이상만 가하는 스킬인 경우
            else if (action.isDamage == false && action.isStatusEff == true) 
            {
                Debug.LogWarning("3");
            }
        }
    }

    #region 내부 Item Dictionary 에서 랜덤한 아이템 얻어오는 메서드
    private void GetItemFromClientDB() 
    {
        GameObject testItem = Instantiate(ItemDataBase.Instance.fieldItemPrefab, 
            transform.parent.position, Quaternion.identity);

        testItem.GetComponent<FieldItem>().SetItem(ItemDataBase.Instance.itemDB[Random.Range(0, 13)]);
    }
    #endregion

    #region 데미지 받는 메서드
    public virtual void GetMonsterHit(int hp_, int damage_)
    {
        hp_ -= damage_;

        if (hp_ <= 0f)
        {
            monsterFSM.ChangeState(MonsterStateMachine.State.Die);
        }
    }
    #endregion
}

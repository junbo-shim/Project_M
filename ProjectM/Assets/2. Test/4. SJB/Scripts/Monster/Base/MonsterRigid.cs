using UnityEngine;

public class MonsterRigid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other_)
    {
        // 만약 충돌한 물체가 PlayerATK 레이어를 가지고 있을 경우
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerATK"))) 
        {
            // DEMO
            //transform.parent.GetComponent<TestMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Die);
            //GetItemFromClientDB();
            //Destroy(transform.parent.gameObject);

            // 데미지만 가하는 스킬인 경우
            if (other_.GetComponent<SkillAction>().isDamage == true
                && other_.GetComponent<SkillAction>().isStatusEff == false) 
            {
                Debug.LogWarning("1");
            }
            // 데미지 + 상태이상 스킬인 경우
            else if (other_.GetComponent<SkillAction>().isDamage == true
                        && other_.GetComponent<SkillAction>().isStatusEff == true) 
            {
                Debug.LogWarning("2");
            }
            // 상태이상만 가하는 스킬인 경우
            else if (other_.GetComponent<SkillAction>().isDamage == false
                        && other_.GetComponent<SkillAction>().isStatusEff == true) 
            {
                Debug.LogWarning("3");
            }
        }
    }

    private void GetItemFromClientDB()
    {
        GameObject testItem = Instantiate(ItemDataBase.Instance.fieldItemPrefab,
            transform.parent.position, Quaternion.identity);

        testItem.GetComponent<FieldItem>().SetItem(ItemDataBase.Instance.itemDB[Random.Range(0, 13)]);
    }
}

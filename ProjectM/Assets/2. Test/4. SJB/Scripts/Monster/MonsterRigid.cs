using UnityEngine;

public class MonsterRigid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other_)
    {
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerATK"))) 
        {
            // DEMO
            transform.parent.GetComponent<TestMonster>().monsterFSM.ChangeState(MonsterStateMachine.State.Die);
            GetItemFromClientDB();
            Destroy(transform.parent.gameObject);
        }
    }

    private void GetItemFromClientDB() 
    {
        GameObject testItem = Instantiate(ItemDataBase.Instance.fieldItemPrefab, 
            transform.parent.position, Quaternion.identity);

        testItem.GetComponent<FieldItem>().SetItem(ItemDataBase.Instance.itemDB[Random.Range(0, 13)]);
    }
}

using UnityEngine;

public class Test2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("MonsterATK"))) 
        {
            Debug.Log(other.GetComponent<MonsterATK>().damage);
        }
    }
}

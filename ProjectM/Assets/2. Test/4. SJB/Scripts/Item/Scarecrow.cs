using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    private ObjectPool scarecrowPool;

    private void Awake()
    {
        scarecrowPool = GameObject.Find("Pool_Scarecrow").GetComponent<ObjectPool>();
    }

    private void OnTriggerEnter(Collider other_)
    {
        if (other_.gameObject.layer.Equals(LayerMask.NameToLayer("MonsterATK"))) 
        {
            scarecrowPool.ReturnObjToPool(gameObject);
        }
    }
}

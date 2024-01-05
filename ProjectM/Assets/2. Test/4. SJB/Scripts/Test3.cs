using UnityEngine;

public class Test3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Monster"))) 
        {
            gameObject.SetActive(false);
        }
    }
}

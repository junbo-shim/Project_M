using Unity.VisualScripting;
using UnityEngine;

public class TestP : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I)) 
        {
            gameObject.layer = LayerMask.NameToLayer("Invisible");
        }
        else if (Input.GetKey(KeyCode.U)) 
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }   
}

using UnityEngine;

public class DetectUI : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);       
    }
}

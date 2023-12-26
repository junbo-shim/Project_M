using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 5f; // 플레이어 이동 속도

    void Update()
    {
        // 플레이어 이동 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
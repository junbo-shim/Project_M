using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 5f; // �÷��̾� �̵� �ӵ�

    void Update()
    {
        // �÷��̾� �̵� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
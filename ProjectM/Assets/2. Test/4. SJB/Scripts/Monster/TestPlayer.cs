using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public CharacterController control;
    public GameObject footStep;
    public Vector3 beforePos;

    private float horizontal;
    private float vertical;
    private float speed;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        speed = 5f;
        gravity = -9.81f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        beforePos = transform.position;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(speed * horizontal, gravity, speed * vertical);

        control.Move(Time.deltaTime * moveVector);

        ChangeFootStep(beforePos);
    }

    private void ChangeFootStep(Vector3 beforePos_) 
    {
        if (beforePos_ != transform.position) 
        {
            footStep.SetActive(true);
        }
        else
        {
            footStep.SetActive(false);
        }
    }
}

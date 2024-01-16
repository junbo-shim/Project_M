using UnityEngine;
using System.Collections;

public class Hanger : MonoBehaviour
{
    private GameObject doorPoint;
    private float doorTime;
    private WaitForSeconds doorDelay;


    private void Start()
    {
        // 게임 오브젝트 하위에 있는 문 할당
        doorPoint = transform.GetChild(1).gameObject;
        doorTime = 0.02f;
        doorDelay = new WaitForSeconds(doorTime);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R)) 
        {
            OpenDoor();
        }
        else if (Input.GetKey(KeyCode.T))
        {
            CloseDoor();
        }
    }


    // 문 여는 함수
    public void OpenDoor() 
    {
        StartCoroutine(Open());
    }

    // 문 여는 코루틴
    private IEnumerator Open() 
    {
        float angle = default;

        while (0.7f >= Quaternion.Euler(new Vector3(angle, 0, 0)).x)
        {
            yield return doorDelay;

            angle += 1f;

            doorPoint.transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
        }
    }

    // 문 닫는 함수
    public void CloseDoor() 
    {
        StartCoroutine(Close());
    }

    // 문 닫는 코루틴
    private IEnumerator Close() 
    {
        float angle = doorPoint.transform.rotation.eulerAngles.x;

        while (0f <= Quaternion.Euler(new Vector3(angle, 0, 0)).x)
        {
            yield return doorDelay;

            angle -= 1f;

            doorPoint.transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
        }
    }
}

using UnityEngine;
using System.Collections;

public class Hanger_1_Door : MonoBehaviour
{
    private float doorTime;
    private WaitForSeconds doorDelay;


    private void Start()
    {
        doorTime = 0.02f;
        doorDelay = new WaitForSeconds(doorTime);

        MapGameManager.instance.dayStart += CloseDoor;
        MapGameManager.instance.nightStart += OpenDoor;
    }

    // 문 여는 함수
    public void OpenDoor()
    {
        StartCoroutine(Open());
    }

    // 문 여는 코루틴
    private IEnumerator Open()
    {
        float height = transform.localPosition.y;

        while (-22 < height)
        {
            yield return doorDelay;

            height -= 0.5f;

            transform.localPosition = new Vector3(0, height, 0);
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
        float height = transform.localPosition.y;

        while (0 > height)
        {
            yield return doorDelay;

            height += 0.5f;

            transform.localPosition = new Vector3(0, height, 0);
        }
    }
}

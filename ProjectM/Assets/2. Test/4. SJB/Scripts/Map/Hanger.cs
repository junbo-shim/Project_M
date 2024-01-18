using UnityEngine;
using System.Collections;

public class Hanger : MonoBehaviour
{
    private GameObject doorPoint;
    private float doorTime;
    private WaitForSeconds doorDelay;
    public GameObject nightMonster;
    private ObjectPool nightMonsterPool;

    private void Start()
    {
        // 게임 오브젝트 하위에 있는 문 할당
        doorPoint = transform.GetChild(1).gameObject;
        doorTime = 0.02f;
        doorDelay = new WaitForSeconds(doorTime);
        nightMonsterPool = GameObject.Find("Pool_Mech_Large").GetComponent<ObjectPool>();
        nightMonster = nightMonsterPool.ActiveObjFromPool(transform.position + (Vector3.forward * 10));

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
        float angle = default;

        while (0.7f >= Quaternion.Euler(new Vector3(angle, 0, 0)).x)
        {
            yield return doorDelay;

            angle += 1f;

            doorPoint.transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
        }

        if (nightMonster.GetComponent<Monster>().enabled.Equals(false)) 
        {
            nightMonster = nightMonsterPool.ActiveObjFromPool(transform.position + (Vector3.forward * 10));
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

        if (nightMonster.GetComponent<Monster>().enabled.Equals(true)) 
        {
            nightMonsterPool.ReturnObjToPool(nightMonster);
        }
    }
}

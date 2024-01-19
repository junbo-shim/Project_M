using UnityEngine;
using System.Collections;

public class Hanger : MonoBehaviour
{
    private GameObject doorPoint;
    private float doorTime;
    private WaitForSeconds doorDelay;
    public GameObject nightMonsterPool;
    public GameObject nightMonster;


    private void Start()
    {
        // 게임 오브젝트 하위에 있는 문 할당
        doorPoint = transform.GetChild(1).gameObject;
        doorTime = 0.02f;
        doorDelay = new WaitForSeconds(doorTime);

        nightMonster = nightMonsterPool.GetComponent<ObjectPool>().ActiveObjFromPool(transform.position);
        nightMonsterPool.GetComponent<ObjectPool>().ReturnObjToPool(nightMonster);

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
        float angle = doorPoint.transform.rotation.eulerAngles.x;

        while (90 >= angle)
        {
            yield return doorDelay;

            angle += 1f;

            doorPoint.transform.rotation = Quaternion.Euler(new Vector3(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        }

        nightMonster = nightMonsterPool.GetComponent<ObjectPool>().ActiveObjFromPool(transform.position);
        nightMonster.transform.position = transform.position + (Vector3.forward * 10f);
        nightMonster.GetComponent<Monster>().monsterFSM.ChangeState(MonsterStateMachine.State.Patrol);
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

        while (0 <= angle)
        {
            yield return doorDelay;

            angle -= 1f;

            doorPoint.transform.rotation = Quaternion.Euler(new Vector3(angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        }

        nightMonsterPool.GetComponent<ObjectPool>().ReturnObjToPool(nightMonster);
        nightMonster.GetComponent<Monster>().monsterFSM.ChangeState(MonsterStateMachine.State.Ready);
    }
}

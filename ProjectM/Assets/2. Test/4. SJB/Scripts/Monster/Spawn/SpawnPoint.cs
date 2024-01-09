using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public ObjectPool monsterPool;
    public GameObject detectArea;


    private void Start()
    {
        MapGameManager.instance.dayStart += TurnOnDetection;
        MapGameManager.instance.nightStart += TurnOffDetection;
        MapGameManager.instance.nightStart += ReturnAllObject;
    }


    // 밤에는 스폰 시키지 않도록 감지를 해제하는 메서드
    private void TurnOffDetection()
    {
        if (MapGameManager.instance.currentState.Equals(DayState.NIGHT)) 
        {
            detectArea.SetActive(false);
        }
    }

    // 낮에 다시 감지범위를 켜주는 메서드
    private void TurnOnDetection() 
    {
        if (!MapGameManager.instance.currentState.Equals(DayState.NIGHT)) 
        {
            detectArea.SetActive(true);
        }
    }

    // 모든 오브젝트 회수
    private void ReturnAllObject() 
    {
        if (MapGameManager.instance.currentState.Equals(DayState.NIGHT))
        {
            monsterPool.GetAllActivePoolObjects();
        }
    }
}

using BNG;
using System;
using UnityEngine;

public enum DayState
{
    MORNING,
    SUNSET,
    NIGHT
}

public class MapGameManager : MonoBehaviour
{
    private static MapGameManager Instance;
    public bool BedAct = false;
    public bool StopDayProgression = false;
    public GameObject itemSpObj;
    public RidingSnapZone ridingSnapZone;
    public GameObject playerGameObject;
    public Vector3 playerSavePos;


    //23.01.05 LJY
    public event Action dayStart;
    public event Action nightStart;



    public static MapGameManager instance
    {
        get 
        { 
            if (Instance == null)
            {
                GameObject singletonObject = new GameObject("MapGameManager");
                Instance = singletonObject.AddComponent<MapGameManager>();
            }
            return Instance; 
        }
    }

    public DayState currentState;

    public void Awake()
    {
        ridingSnapZone = playerGameObject.GetComponent<RidingSnapZone>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    void Start()
    {
        if(playerSavePos == null)
        {
            playerSavePos =new Vector3(-13.1899414f + 2, -2.75f, -17.8699951f);
        }
        currentState = DayState.MORNING;
    }

   
    // 상태 변화 
    public void DayStateChange(DayState state) 
    {
        currentState = state;
       
        if (currentState == DayState.NIGHT )
        {
            // 23.01.05 LJY
            nightStart?.Invoke();   
            itemSpObj.SetActive(true);
        }
        else if (currentState == DayState.SUNSET)
        {
            itemSpObj.SetActive(false);
        }
        // 23.01.05 LJY
        else if (currentState == DayState.MORNING)
        {
            dayStart?.Invoke();
        }
    }

    public Quaternion LightRotate(Vector3 vector)
    {
   
        float xRotation = vector.x;
        
        if (currentState == DayState.MORNING)
        {
            xRotation = 160f;
       
        }
        else if (currentState == DayState.NIGHT)
        {
        
            xRotation = 180f;
           
        }
   
        Quaternion quaternion = Quaternion.Euler(xRotation, vector.y, vector.z);
        return quaternion;
    }

    public void BedChange() //침대 사용여부 체크
    {
      
       BedAct = !BedAct;

    }

    public void ChangeDayState()
    {
        StopDayProgression = !StopDayProgression;
      
    }

    public void OnObj(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void SetplayerSavePos(Transform transform_)
    {
        playerSavePos = transform_.position;
    }
}

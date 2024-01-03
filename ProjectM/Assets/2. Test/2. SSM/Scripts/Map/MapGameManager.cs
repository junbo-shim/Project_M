using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public GameObject itemSpObj;
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
        currentState = DayState.MORNING;
    }

   
    // 상태 변화 
    public void DayStateChange(DayState state) 
    {
        currentState = state;
        Debug.Log(currentState);
        if (currentState == DayState.NIGHT )
        {
            itemSpObj.SetActive(true);
       
        }else if (currentState == DayState.SUNSET)
        {
            itemSpObj.SetActive(false);
       
        }
      
    }

    public Quaternion LightRotate(Vector3 vector)
    {
   
        float xRotation = vector.x;
        Debug.Log(xRotation);
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
    public void OnObj(GameObject obj)
    {
        obj.SetActive(true);
    }
}

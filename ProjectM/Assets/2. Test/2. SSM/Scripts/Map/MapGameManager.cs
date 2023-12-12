using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
      
    }
    // 상태 변화 
    public void DayStateChange(DayState state) 
    {
        currentState = state;
     
        if(currentState == DayState.NIGHT )
        {
            itemSpObj.SetActive(true);
       
        }else if (currentState == DayState.SUNSET)
        {
            itemSpObj.SetActive(false);
       
        }
      
    }

    public void OnObj(GameObject obj)
    {
        obj.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItem : MonoBehaviour
{
    public GameObject scarecrowObj;     // 허수아비 오브젝트
    public GameObject trapObj;          // 덫 오브젝트

    private BookScript book;    // 책 레이 힛포인트 받아오기위한 컴포넌트

    private GameObject settingObj;  // 설치중인 오브젝트를 담을 변수
    private bool isSetting;         // 설치중인지 확인할 bool변수

    private Inventory playerInven;  // 플레이어 인벤토리

    void Start()
    {
        book = GetComponent<BookScript>();
        playerInven = FindAnyObjectByType<Inventory>();
    }

    void Update()
    {
        
    }

    public void SetScarecrow()
    {
        scarecrowObj.SetActive(true);
        settingObj = scarecrowObj;
        settingObj.transform.GetChild(0).gameObject.SetActive(true);
        settingObj.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine(SettingItemCT());
    }

    public void SetTrap()
    {
        trapObj.SetActive(true);
        settingObj = trapObj;
        settingObj.transform.GetChild(0).gameObject.SetActive(true);
        settingObj.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine(SettingItemCT());
    }

    private IEnumerator SettingItemCT()
    {
        while(isSetting)
        {
            settingObj.transform.position = book.target;
            yield return null;
        }

        settingObj.transform.GetChild(0).gameObject.SetActive(false);
        settingObj.transform.GetChild(1).gameObject.SetActive(true);

        if(settingObj == scarecrowObj)
        {
            for(int i = 0; i < playerInven.items.Count; i++)
            {
                if (playerInven.items[i].itemName == "ScareCrow")
                {
                    playerInven.items[i].itemCount = playerInven.items[i].itemCount - 1;
                }
            }
        }
        else if( settingObj == trapObj)
        {
            for (int i = 0; i < playerInven.items.Count; i++)
            {
                if (playerInven.items[i].itemName == "Trap")
                {
                    playerInven.items[i].itemCount = playerInven.items[i].itemCount - 1;
                }
            }
        }
    }
}

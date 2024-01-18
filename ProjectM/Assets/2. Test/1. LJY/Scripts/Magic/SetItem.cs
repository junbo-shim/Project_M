using System.Collections;
using TMPro;
using UnityEngine;

public class SetItem : MonoBehaviour
{
    public CoolTimeManager coolManager; // 쿨타임 관리 스크립트

    public GameObject scarecrowObj;     // 허수아비 오브젝트
    public GameObject trapObj;          // 덫 오브젝트

    public GameObject scarecrowTrigger; // 허수아비 트리거
    public GameObject trapTrigger;      // 덫 트리거

    private BookScript book;    // 책 레이 힛포인트 받아오기위한 컴포넌트

    private GameObject settingObj;  // 설치중인 오브젝트를 담을 변수
    public bool isSetting;         // 설치중인지 확인할 bool변수
    public GameObject buttons;     // 설치 버튼 모음 오브젝트

    private Inventory playerInven;  // 플레이어 인벤토리

    public TextMeshProUGUI noticeText;     // 아이템 전용 알림 텍스트
    private float fadeDuration = 1.5f;     // 페이드 아웃에 걸리는 시간
    private Coroutine fadeCoroutine;       // 텍스트 페이드 코루틴

    void Start()
    {
        book = transform.parent.parent.GetComponent<BookScript>();
        playerInven = FindAnyObjectByType<Inventory>();
        coolManager = book.GetComponent<CoolTimeManager>();
        SetStarting(); // 시작하면 미리 생성하고 꺼놓기
    }

    private void OnEnable()
    {
        if(isSetting)
        {
            settingObj.SetActive(false);
            settingObj = null;
            isSetting = false;
        }   // if : 설치중에 트리거 지속시간이 종료되어 UI가 다시 나타났을 경우
    }

    private void OnDisable()
    {
        buttons.SetActive(true);    // 버튼 다시 켜놓기
        if(isSetting)   
        {
            settingObj.SetActive(false);
            settingObj = null;
        }   // if : 설치하다 Ui가 꺼졌을 경우 설치중이던 오브젝트 끄기
    }

    // 초기 세팅 함수
    private void SetStarting()
    {
        scarecrowObj = Instantiate(scarecrowObj, transform.position, Quaternion.identity);
        trapObj = Instantiate(trapObj, transform.position, Quaternion.identity);
        scarecrowObj.SetActive(false);
        trapObj.SetActive(false);
    }

    // 허수아비 설치 함수
    public void SetScarecrow()
    {
        //for (int i = 0; i < playerInven.items.Count; i++)
        //{
        //    if (playerInven.items[i].itemName == "ScareCrow")
        //    {
        //        if (playerInven.items[i].itemCount == 0) // 허수아비가 0개 일 때 
        //        {
        //            noticeText.text = string.Format("허수아비가 부족합니다.");
        //            FadeText();
        //            return;
        //        }
        //    }
        //}
        if (coolManager.isScarecrowCool)
        {
            // 쿨타임이라고 알리기
            noticeText.text = string.Format("허수아비의 쿨타임이 {0:0}초 남았습니다.", coolManager.currentSCCool);
            FadeText();
            return;
        }
        scarecrowObj.SetActive(true);
        scarecrowTrigger.SetActive(true);
        settingObj = scarecrowObj;
        settingObj.transform.GetChild(0).gameObject.SetActive(true);
        settingObj.transform.GetChild(1).gameObject.SetActive(false);
        isSetting = true;
        StartCoroutine(SettingItemCT());
    }

    // 덫 설치 함수
    public void SetTrap()
    {
        //for (int i = 0; i < playerInven.items.Count; i++)
        //{
        //    if (playerInven.items[i].itemName == "Trab")
        //    {
        //        if (playerInven.items[i].itemCount == 0) // 트랩이 0개 일 때 
        //        {
        //            noticeText.text = string.Format("덫이 부족합니다.");
        //            FadeText();
        //            return;
        //        }
        //    }
        //}


        if (coolManager.isTrapCool)
        {
            // 쿨타임이라고 알리기
            noticeText.text = string.Format("덫의 쿨타임이 {0:0}초 남았습니다.", coolManager.currentTrCool);
            FadeText();
            return;
        }
        trapObj.SetActive(true);
        trapTrigger.SetActive(true);
        settingObj = trapObj;
        settingObj.transform.GetChild(0).gameObject.SetActive(true);
        settingObj.transform.GetChild(1).gameObject.SetActive(false);
        isSetting = true;
        StartCoroutine(SettingItemCT());
    }

    private IEnumerator SettingItemCT()
    {
        // 설치 시작시 버튼 끄기
        buttons.SetActive(false);

        while(isSetting)
        {
            settingObj.transform.position = book.target;
            yield return null;
        }   // 설치중이면 세팅오브젝트가 레이 끝을 따라간다.

        buttons.SetActive(true); // 설치 종료시 버튼 켜기

        settingObj.transform.GetChild(0).gameObject.SetActive(false);
        settingObj.transform.GetChild(1).gameObject.SetActive(true);

        if (settingObj == scarecrowObj)
        {
            StartCoroutine(coolManager.ScarecrowCoolTime());
            //for (int i = 0; i < playerInven.items.Count; i++)
            //{
            //    if (playerInven.items[i].itemName == "ScareCrow")
            //    {
            //        playerInven.items[i].itemCount = playerInven.items[i].itemCount - 1;
            //    }
            //}
        }
        else if (settingObj == trapObj)
        {
            StartCoroutine(coolManager.TrapCoolTime());
            //for (int i = 0; i < playerInven.items.Count; i++)
            //{
            //    if (playerInven.items[i].itemName == "Trap")
            //    {
            //        playerInven.items[i].itemCount = playerInven.items[i].itemCount - 1;
            //    }
            //}
        }
    }

    private void FadeText()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeOutText());
    }

    // 아이템 알림 텍스트 페이드 코루틴
    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        noticeText.color = Color.white;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            SetTextAlpha(alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetTextAlpha(0f);
    }

    // 알파값 변경하는 함수
    private void SetTextAlpha(float alpha_)
    {
        Color textColor = Color.white;
        textColor.a = alpha_;
        noticeText.color = textColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BedOBJScripts : NpcActionBase
{
    [SerializeField] private InputActionReference playerUpAction; // 플레이어 키입력
    [SerializeField] private GameObject msgObj; // 메세지 박스 오브젝트
    [SerializeField] private GameObject imgObj; // blackBak 이미지 오브젝트
    private Image image; // 이미지 컴퍼넌트 저장용
    private WaitForSeconds colorTime; // 색 변경 시간 캐싱
    private WaitForSeconds readyTime; // 대기 시간 캐싱
    private Color color;// 현재 컬러 저장용
    private Color orgColor;// 원래컬러
    public void Start()
    {
        image = imgObj.transform.GetChild(0).GetComponent<Image>();
        colorTime = new WaitForSeconds(0.1f);
        readyTime = new WaitForSeconds(0.3f);
        color = image.color; // 이미지 컬러 컬러에 넣기
        orgColor = color; // 원래 컬러 저장
      
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            msgObj.transform.LookAt(other.transform);
            if (playerUpAction.action.ReadValue<float>() == 1)
            {
                if (ClickBool == false)
                {
                    BoolChange();
                    if (!msgObj.activeSelf)
                    {
                        msgObj.SetActive(true);
                    }
                    else
                    {
                        msgObj.SetActive(false);
                    }

                }
            }
        }
  
    }
     
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            msgObj.SetActive(false);
        }
    }
    public void OnButtonYes()
    {
      
       
        imgObj.SetActive(true);
        StartCoroutine(AlphaValueChange());
  

    }

    public void OnButtonNo()
    {
        msgObj.SetActive(false);
    }

    private IEnumerator AlphaValueChange() // 이미지 알파값 변화
    {
       
        while (color.a < 1)
        {
            color.a += 0.1f;
            image.color = color;
            yield return colorTime;
        }
        MapGameManager.instance.BedChange();
        yield return readyTime;

        while (color.a >= 0)
        {
            color.a -= 0.1f;
            image.color = color;
            yield return colorTime;
        }

        imgObj.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowTrigger : MagicBase
{
    private GameObject itemUiObj;   // 아이템 버튼 Ui
    private SetItem setItem;        // 아이템 설치 스크립트

    // Start is called before the first frame update
    void Start()
    {
        itemUiObj = transform.parent.gameObject;
        setItem = itemUiObj.GetComponent<SetItem>();
    }
   
    protected override void GetPageChangeBtn()
    {
        pageChangeBtn = transform.parent.parent.parent.GetChild(0).gameObject;
    }

    // 허수아비와 덫은 트리거이지만 스킬이 아니므로 base를 사용하지 않는다.
    protected override void CastSkill()
    {
        setItem.isSetting = false;  // 설치중 bool값 해제
        gameObject.SetActive(false);
        itemUiObj.SetActive(true);
    }

    protected override IEnumerator TriggerTimer()
    {
        yield return new WaitForSeconds(triggerDuration);

        transform.parent.GetChild(0).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }


}

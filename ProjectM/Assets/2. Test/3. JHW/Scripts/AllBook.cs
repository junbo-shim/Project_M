using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBook : MonoBehaviour
{
    //인벤토리 Onoff
    public GameObject inventoryPanel;
    public CanvasGroup canvasGroup;
    private bool openIvUI = true;

    public GameObject[] uiControl;

    ////스킬창 OnOff
    //public GameObject skillPanel;
    //bool activeSkill = false;


    ////제작창 OnOff
    //public GameObject creaftingPanel;
    //bool activeCreafting = false;


    public void QuestUI()
    {
        uiControl[0].SetActive(false);
        uiControl[1].SetActive(true);
        inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
        openIvUI = true;

    }


    public void InventoryUIOnOff()
    {
        inventoryPanel.GetComponent<CanvasGroup>().alpha = 1;
        openIvUI = false;
        uiControl[0].SetActive(false);
        uiControl[1].SetActive(false);
    }

    public void SkillUIOnOff()
    {
        inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
        openIvUI = true;
        uiControl[0].SetActive(true);
        uiControl[1].SetActive(false);

    }


}

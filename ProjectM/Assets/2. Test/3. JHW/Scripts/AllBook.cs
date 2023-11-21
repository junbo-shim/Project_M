using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBook : MonoBehaviour
{
    //인벤토리 Onoff
    public GameObject inventoryPanel;
    public CanvasGroup canvasGroup;
    private bool openIvUI = true;

    //스킬창 OnOff
    public GameObject skillPanel;
    bool activeSkill = false;


    //제작창 OnOff
    public GameObject creaftingPanel;
    bool activeCreafting = false;


    public void InventoryUIOnOff()
    {
        if (openIvUI == true)
        {
            inventoryPanel.GetComponent<CanvasGroup>().alpha = 1;
            openIvUI = false;
        }
        else
        {
            inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
            openIvUI = true;
        }
    }

    public void SkillUIOnOff()
    {
        activeSkill = !activeSkill;
        skillPanel.SetActive(activeSkill);
    }


    public void CreaftingUIOnOff()
    {
        activeCreafting = !activeCreafting;
        creaftingPanel.SetActive(activeCreafting);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class AllBook : MonoBehaviour
{

    //public GameObject[] onOffCanvas;

    public InputActionReference inputActionReference;
    public InputActionReference inputActionReferenceSystemSetting;
    ShowDrawPattern showDrawPattern;

    //인벤토리 Onoff
    public GameObject inventoryPanel;
    public CanvasGroup canvasGroup;
    private bool openIvUI = true;

    public GameObject[] uiControl;



    public GameObject onOffCanvas;
    bool activeCanvas = false;

    public GameObject onOffSystemSetting;
    bool activeSystemSetting = false;


    public GameObject helperOnOff;

    public void Start()
    {
        showDrawPattern = GetComponent<ShowDrawPattern>();
    }



    public void QuestUI()
    {
        uiControl[0].SetActive(false);
        uiControl[1].SetActive(true);
        uiControl[2].SetActive(false);
        inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
        openIvUI = true;

    }


    public void InventoryUIOnOff()
    {
        inventoryPanel.GetComponent<CanvasGroup>().alpha = 1;
        openIvUI = false;
        uiControl[0].SetActive(false);
        uiControl[1].SetActive(false);
        uiControl[2].SetActive(false);

    }

    public void SkillUIOnOff()
    {
        inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
        openIvUI = true;
        uiControl[0].SetActive(true);
        uiControl[1].SetActive(false);
        uiControl[2].SetActive(false);

    }

    public void CreaftingItem()
    {
        inventoryPanel.GetComponent<CanvasGroup>().alpha = 0;
        openIvUI = true;
        uiControl[0].SetActive(false);
        uiControl[1].SetActive(false);
        uiControl[2].SetActive(true);
    }


    private void Update()
    {
        if (activeSystemSetting == false)
        {

            if (inputActionReference.action.triggered)
            {
                activeCanvas = !activeCanvas;
                onOffCanvas.SetActive(activeCanvas);
                Debug.Log(activeCanvas);
                if(!activeCanvas)
                {
                    showDrawPattern.CloseFireballPattern();
                    showDrawPattern.CloseProtectPattern();
                    showDrawPattern.CloseIceBallPattern();
                    showDrawPattern.ClosePoisonPattern();
                    showDrawPattern.CloseJumpPattern();
                    showDrawPattern.CloseHealPattern();
                }
                
            }
        }

        if (activeCanvas == false)
        {
            if (inputActionReferenceSystemSetting.action.triggered)
            {
                activeSystemSetting = !activeSystemSetting;
                onOffSystemSetting.SetActive(activeSystemSetting);
                Debug.Log(activeSystemSetting);

            }
        }
    }



    public void HelperOn()
    {
        helperOnOff.SetActive(true);
        activeSystemSetting = false;
    }

    public void HelperOff()
    {
        helperOnOff.SetActive(false);
    }


}
